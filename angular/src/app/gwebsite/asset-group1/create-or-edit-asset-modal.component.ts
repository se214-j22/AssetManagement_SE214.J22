import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit, OnChanges, SimpleChange, SimpleChanges } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CustomerServiceProxy, CustomerInput, AssetInput, AssetTypeServiceProxy, AssetType, AssetTypeDto, ManufacturerServiceProxy, Manufacturer, ManufacturerDto, AssetServiceProxy, AssetLine, AssetLineServiceProxy, AssetLineDto, ComboboxItemDto, OrganizationUnitServiceProxy, ListResultDtoOfOrganizationUnitDto, OrganizationUnitDto, SoftAssetInput } from '@shared/service-proxies/service-proxies';
import jsQR from "jsqr";

@Component({
    selector: 'createOrEditAssetModalGroup1',
    templateUrl: './create-or-edit-asset-modal.component.html'
})
export class CreateOrEditAssetModalComponentGroup1 extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('assetTypeCombobox') assetTypeCombobox: ElementRef;
    @ViewChild('manufacturerCombobox') manufacturerCombobox: ElementRef;
    @ViewChild('assetLineCombobox') assetLineCombobox: ElementRef;
    @ViewChild('statusCombobox') statusCombobox: ElementRef;
    assetTypeComboboxs: ComboboxItemDto[] = [];
    assetTypes: AssetTypeDto[] = [];
    manufacturerComboboxs: ComboboxItemDto[] = [];
    assetLineComboboxs: ComboboxItemDto[] = [];
    assetLines: AssetLineDto[] = new Array<AssetLineDto>();
    manufacturers: ManufacturerDto[] = new Array<ManufacturerDto>();
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
    assetTypeID: string;
    manufacturerID: string;
    depreciationRate: number;
    processingAssetCodes: string[] = [];
    completedAssetCodes: string[] = [];
    errorAssetCodes: string[] = [];
    beingCreated: boolean;
    status: 'IS_DAMAGED' | 'RESTING' | 'USING';
    OUs: ListResultDtoOfOrganizationUnitDto;
    mainOU: OrganizationUnitDto;

    constructor(
        injector: Injector,
        private _assetService: AssetServiceProxy,
        private _assetTypeService: AssetTypeServiceProxy,
        private _manufacturerService: ManufacturerServiceProxy,
        private _assetLineService: AssetLineServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy
    ) {
        super(injector);
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes.assetTypeID && changes.assetTypeID.currentValue && changes.assetTypeID.currentValue != changes.assetTypeID.previousValue
            || changes.manufacturerID && changes.manufacturerID.currentValue && changes.manufacturerID.currentValue != changes.manufacturerID.previousValue) {

        }
        if (changes.asset && changes.asset.currentValue.assetLineID != changes.assetTypeID.previousValue.assetLineID) {
            this._assetLineService.getById(changes.asset.currentValue.assetLineID).subscribe(al => {
                this.assetTypeID = al.assetType.id.toString();
                this.manufacturerID = al.manufacturer.id.toString();
                setTimeout(() => {
                    $(this.assetTypeCombobox.nativeElement).selectpicker('refresh');
                    $(this.manufacturerCombobox.nativeElement).selectpicker('refresh');
                }, 0);
            })
        }


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

        this._assetTypeService.getByFilter(undefined, undefined, 999, undefined).subscribe(result => {
            if (result) {
                this.assetTypes = result.items;
                this.assetTypeComboboxs = result.items.map(at => {
                    return new ComboboxItemDto({ value: at.id.toString(), displayText: at.name, isSelected: false })
                })
            }
        });


        this._manufacturerService.getByFilter(undefined, undefined, 999, undefined).subscribe(result => {
            if (result) {
                this.manufacturers = result.items;
                this.manufacturerComboboxs = result.items.map(m => {
                    return new ComboboxItemDto({ value: m.id.toString(), displayText: m.name, isSelected: false })
                })
            }
        });

        this._assetLineService.getByFilter(undefined, undefined, undefined, undefined, 999, undefined).subscribe(result => {
            if (result) {
                this.assetLines = result.items;
                this.assetLineComboboxs = result.items.map(m => {
                    return new ComboboxItemDto({ value: m.id.toString(), displayText: m.name, isSelected: false })
                })
            }
        });
    }

    refreshAssetLine() {
        this._assetLineService.getByFilter(undefined, Number(this.assetTypeID), Number(this.manufacturerID), undefined, 999, undefined).subscribe(result => {
            if (result) {
                this.assetLines = result.items;
                this.assetLineComboboxs = result.items.map(m => {
                    return new ComboboxItemDto({ value: m.id.toString(), displayText: m.name, isSelected: false })
                })
                setTimeout(() => {
                    $(this.assetLineCombobox.nativeElement).selectpicker('refresh');
                }, 0);
            }
        });
    }
    onChangeAssetType(e) {
        this.refreshAssetLine();
        this.depreciationRate = this.assetTypes.find(at => at.id == e).depreciationRate;
    }
    onChangeManufacturer(e) {
        this.refreshAssetLine();
    }
    onChangeAssetLine(e) {
        if (!e) {
            this.assetTypeID = String(0);
            this.manufacturerID = String(0);
        };
        this._assetLineService.getById(this.asset.assetLineID).subscribe(al => {
            this.assetTypeID = al.assetType.id.toString();
            this.manufacturerID = al.manufacturer.id.toString();
            setTimeout(() => {
                $(this.assetTypeCombobox.nativeElement).selectpicker('refresh');
                $(this.manufacturerCombobox.nativeElement).selectpicker('refresh');
            }, 0);
        })
        if (this.asset.fullDepreciationPrice == null)
            this.asset.fullDepreciationPrice = Number(this.assetLines.find(al => al.id == e).price);
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
        this.assetTypeID = String(0);
        this.manufacturerID = String(0);
        this.asset = new AssetInput();
        this.modifyMultipleAssetsMode = true;
        this.beingCreated = false;
        this.getStatus();
        this.modal.show();
        setTimeout(() => {
            this.startup()
            $(this.assetTypeCombobox.nativeElement).selectpicker('refresh');
            $(this.manufacturerCombobox.nativeElement).selectpicker('refresh');
            $(this.assetLineCombobox.nativeElement).selectpicker('refresh');
            $(this.statusCombobox.nativeElement).selectpicker('refresh');
        }, 0);
    }

    show(assetId?: number | null | undefined): void {
        this.assetTypeID = String(0);
        this.manufacturerID = String(0);
        this.modifyMultipleAssetsMode = false;
        this.depreciationRate = undefined;
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
                    this._assetLineService.getById(this.asset.assetLineID).subscribe(al => {
                        this.assetTypeID = al.assetType.id.toString();
                        this.manufacturerID = al.manufacturer.id.toString();
                        this.depreciationRate = this.assetTypes.find(at => at.id == Number(this.assetTypeID)).depreciationRate;
                        setTimeout(() => {
                            $(this.assetTypeCombobox.nativeElement).selectpicker('refresh');
                            $(this.manufacturerCombobox.nativeElement).selectpicker('refresh');
                            $(this.assetLineCombobox.nativeElement).selectpicker('refresh');
                        }, 0);
                    })
                }
                console.log(this);
                this.getStatus();
                this.modal.show();
                setTimeout(() => {
                    $(this.assetTypeCombobox.nativeElement).selectpicker('refresh');
                    $(this.manufacturerCombobox.nativeElement).selectpicker('refresh');
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
            if (!multiple)
                this.close();
        })
    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
