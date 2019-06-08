import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit, OnChanges } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CustomerServiceProxy, CustomerInput, AssetInput, AssetServiceProxy, AssetLine, AssetLineServiceProxy, AssetLineDto, ComboboxItemDto, OrganizationUnitServiceProxy, ListResultDtoOfOrganizationUnitDto, OrganizationUnitDto } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditAssetModal',
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

    asset: AssetInput = new AssetInput();
    assetLines: AssetLineDto[] = new Array<AssetLineDto>();
    beingCreated: boolean;
    status: 'IS_DAMAGED' | 'RESTING' | 'USING';
    OUs: ListResultDtoOfOrganizationUnitDto;
    mainOU: OrganizationUnitDto;

    constructor(
        injector: Injector,
        private _assetService: AssetServiceProxy,
        private _assetLineService: AssetLineServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy
    ) {
        super(injector);
        console.log('cre_cons',this);
    }

    ngOnInit() {
        console.log('cre_on',this);
        this._organizationUnitService.getOrganizationUnit().subscribe(ou =>
            this.mainOU = ou);
        this._organizationUnitService.getOrganizationUnits().subscribe(ous => {
            this.OUs = ous;
            this.organizationUnitComboboxs = ous.items.filter(ou=> ou.id!=this.mainOU.id).map(ou => {
                return new ComboboxItemDto({ value: ou.id.toString(), displayText: ou.displayName, isSelected: false })
            })
        }
        );
        this._assetLineService.getByFilter(undefined, undefined, undefined, undefined, 999, undefined).subscribe(result => {
            if (result) {
                this.assetLines = result.items;
                this.assetLineComboboxs = result.items.map(al => {
                    return new ComboboxItemDto({ value: al.id.toString(), displayText: al.name, isSelected: false })
                })
            }
        });
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

    show(assetId?: number | null | undefined): void {
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

    save(): void {
        this.asset.isDamaged = this.status == 'IS_DAMAGED';
        if (this.status == 'IS_DAMAGED' || this.status == 'RESTING')
            this.asset.organizationUnitId = this.mainOU.id;
        let input = this.asset;
        this.saving = true;
        this._assetService.createOrEdit(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
