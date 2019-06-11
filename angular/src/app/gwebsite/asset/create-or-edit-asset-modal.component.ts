import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit, OnChanges } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CustomerServiceProxy, CustomerInput, AssetInput, AssetServiceProxy, AssetLine, AssetLineServiceProxy, AssetLineDto, ComboboxItemDto, OrganizationUnitServiceProxy, ListResultDtoOfOrganizationUnitDto, OrganizationUnitDto, SoftAssetInput } from '@shared/service-proxies/service-proxies';
import jsQR from "jsqr";

@Component({
    selector: 'createOrEditAssetModal',
    styles: ['./create-or-edit-asset-modal.component.css'],
    templateUrl: './create-or-edit-asset-modal.component.html'
})
export class CreateOrEditAssetModalComponent extends AppComponentBase implements OnInit {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('assetLineCombobox') assetLineCombobox: ElementRef;
    @ViewChild('statusCombobox') statusCombobox: ElementRef;
    assetLineComboboxs: ComboboxItemDto[] = [];
    organizationUnitComboboxs: ComboboxItemDto[] = [];
    statusComboboxs: ComboboxItemDto[] = [
        new ComboboxItemDto({ value: 'IS_DAMAGED', isSelected: false, displayText: 'Damaged' }),
        new ComboboxItemDto({ value: 'RESTING', isSelected: true, displayText: 'Resting' }),
        new ComboboxItemDto({ value: 'USING', isSelected: false, displayText: 'Using' })];
    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    modifyMultipleAssetsMode = false;
    asset: AssetInput = new AssetInput();
    processingAssetCodes: string[] = [];
    completedAssetCodes: string[] = [];
    errorAssetCodes: string[] = [];
    assetLines: AssetLineDto[] = new Array<AssetLineDto>();
    beingCreated: boolean;
    status: 'IS_DAMAGED' | 'RESTING' | 'USING';
    OUs: ListResultDtoOfOrganizationUnitDto;
    mainOU: OrganizationUnitDto;
    // assetCode: string;

    constructor(
        injector: Injector,
        private _assetService: AssetServiceProxy,
        private _assetLineService: AssetLineServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy
    ) {
        super(injector);
    }

    ngOnInit() {
        this._organizationUnitService.getOrganizationUnit().subscribe(ou =>
            this.mainOU = ou);
        this._organizationUnitService.getOrganizationUnits().subscribe(ous => {
            if (ous) {
                this.OUs = ous;
                this.organizationUnitComboboxs = ous.items.filter(ou => ou.id != this.mainOU.id).map(ou => {
                    return new ComboboxItemDto({ value: ou.id.toString(), displayText: ou.displayName, isSelected: false })
                })
            }
        });
        this._assetLineService.getByFilter(undefined, undefined, undefined, undefined, 999, undefined).subscribe(result => {
            if (result) {
                this.assetLines = result.items;
                this.assetLineComboboxs = result.items.map(al => {
                    return new ComboboxItemDto({ value: al.id.toString(), displayText: al.name, isSelected: false })
                })
            }
        });
    }

    startup() {
        this.video = document.getElementById('video');
        this.canvas = document.getElementById('canvas');
        this.context = this.canvas.getContext('2d');
        navigator.mediaDevices.getUserMedia({ video: { facingMode: "environment" }, audio: false })
            .then((stream) => {
                this.video.srcObject = stream;
                this.video.setAttribute("playsinline", true); // required to tell iOS safari we don't want fullscreen
                this.video.play();
                requestAnimationFrame(this.decodeQRCode);
            });

    }
    video: any;
    canvas: any;
    context: any;
    decodeQRCode = () => {
        // console.log(this.video.readyState);
        if (this.video.readyState === this.video.HAVE_ENOUGH_DATA) {
            this.canvas.height = this.video.videoHeight;
            this.canvas.width = this.video.videoWidth;
            this.context.drawImage(this.video, 0, 0, this.canvas.width, this.canvas.height);
            var imageData = this.context.getImageData(0, 0, this.canvas.width, this.canvas.height);
            var code = jsQR(imageData.data, imageData.width, imageData.height, {
                inversionAttempts: "dontInvert",
            });
            if (code && code.data) {
                console.log("Found QR code", code.data);
                if (this.processingAssetCodes.some(c => c == code.data)
                    || this.completedAssetCodes.some(c => c == code.data)) {
                    this.notify.info('This asset have aready edited!');
                }
                else {
                    this.processingAssetCodes.push(code.data);
                    this.editAsset(code.data);
                }
                setTimeout(() => requestAnimationFrame(this.decodeQRCode), 2000);
            }
            else {
                requestAnimationFrame(this.decodeQRCode);
            }
        }
        else
            requestAnimationFrame(this.decodeQRCode);
    }

    getStatus() {
        if (this.asset.isDamaged)
            this.status = 'IS_DAMAGED';
        else {
            if (this.asset.organizationUnitId && this.asset.organizationUnitId != this.mainOU.id) {
                this.status = 'USING';
            }
            else {
                this.status = 'RESTING';
            }
        }
    }

    showForModify(): void {
        this.processingAssetCodes = [];
        this.completedAssetCodes = [];
        this.errorAssetCodes = [];
        this.saving = false;
        this.modifyMultipleAssetsMode = true;
        this.asset.isDamaged = false;
        this.beingCreated = false;
        this.getStatus();
        this.modal.show();
        setTimeout(() => {
            this.startup()
            $(this.assetLineCombobox.nativeElement).selectpicker('refresh');
            $(this.statusCombobox.nativeElement).selectpicker('refresh');
        }, 0);
    }

    show(assetId?: number | null | undefined): void {
        console.log(assetId);
        this.saving = false;
        console.log(this);
        this._assetService.getForEdit(assetId).subscribe(result2 => {
            console.log(this);
            if (result2) {
                console.log(this);
                this.asset = result2;
                if (!this.asset.assetLineID) {
                    this.asset.isDamaged = false;

                    this.beingCreated = true;
                    this.asset.number = 1;
                }
                else {
                    this.beingCreated = false;
                }
                console.log(this);
                this.getStatus();
                this.modal.show();
                setTimeout(() => {
                    $(this.assetLineCombobox.nativeElement).selectpicker('refresh');
                    $(this.statusCombobox.nativeElement).selectpicker('refresh');
                }, 0);
            }
        })
    }
    editAsset(code: string) {
        this.asset.isDamaged = this.status == 'IS_DAMAGED';
        if (this.status == 'IS_DAMAGED' || this.status == 'RESTING')
            this.asset.organizationUnitId = this.mainOU.id;
        let softUpdateAsset = new SoftAssetInput(Object.assign(this.asset, { code: code }));
        this.saving = true;
        this._assetService.softUpdate(softUpdateAsset).subscribe(result => {
            this.processingAssetCodes = this.processingAssetCodes.filter(c => c != code);
            this.completedAssetCodes.push(code);
            this.notify.info(this.l('SavedSuccessfully'));
        },
            error => {
                this.processingAssetCodes = this.processingAssetCodes.filter(c => c != code);
                this.errorAssetCodes.push(code);
            })
    }

    save(multiple = false): void {
        this.asset.isDamaged = this.status == 'IS_DAMAGED';
        if (this.status == 'IS_DAMAGED' || this.status == 'RESTING')
            this.asset.organizationUnitId = this.mainOU.id;
        let input = this.asset;
        this.saving = true;
        this._assetService.createOrEdit(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            if (multiple)
                this.close();
        })
    }

    close(): void {

        this.modal.hide();
        this.modalSave.emit(null);
    }
}
