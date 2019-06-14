import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { RevokeServiceProxy, RevokeInput, AssetForViewDto, AssetGroupForViewDto, AssetServiceProxy, AssetGroupServiceProxy, RevokeDto, AssetDto } from '@shared/service-proxies/service-proxies';
import { moment } from 'ngx-bootstrap/chronos/test/chain';
import { AssetLookupModalComponent } from '@app/shared/common/lookup/asset-lookup-modal.component';


@Component({
    selector: 'createOrEditRevokeModal',
    templateUrl: './create-or-edit-revoke-modal.component.html'
})
export class CreateOrEditRevokeModalComponent extends AppComponentBase {
    @ViewChild('assetLookup') assetLookupModal: AssetLookupModalComponent;

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('revokeCombobox') revokeCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    revoke: RevokeInput = new RevokeInput();
    listAssetInUse: AssetForViewDto[];
    assetSelect: AssetForViewDto = new AssetForViewDto();
    assetType: string = "";
    assetGroup: AssetGroupForViewDto = new AssetGroupForViewDto();
    dateEndDepreciation: string;
    listRevoke: RevokeDto[];
    remainingOfLiquidation: number;

    constructor(
        injector: Injector,
        private _revokeService: RevokeServiceProxy,
        private _assetService: AssetServiceProxy,
        private _assetgroupService: AssetGroupServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
        //Add 'implements OnInit' to the class.
        this.getListAssetsInUse();
    }

    show(revokeId?: number | null | undefined): void {
        this.saving = false;
        this.getListAssetsInUse();
        this._revokeService.getRevokeForEdit(revokeId).subscribe(result => {
            this.revoke = result;
            if (!this.revoke.id) {
                this.revoke.revokeDate = moment().format('YYYY-MM-DD');
            }
            else {
                this.getAssetByID(this.revoke.assetId);
            }
            console.log(this);
            this.modal.show();
        })
    }

    save(): void {
        let input = this.revoke;
        this.saving = true;
        this._revokeService.createOrEditRevoke(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }

    getListAssetsInUse(): void {
        this._assetService.getListAssetsInUse().subscribe(result => {
            this.listAssetInUse = result;
            this.getListRevokeNotApproved();
        });
    }

    getAssetByID(assetID: string): void {
        this._assetService.getAssetByAssetID(assetID).subscribe(result => {
            this.assetSelect = result;
            if (this.assetSelect.assetType == 0) {
                this.assetType = "Công cụ lao động";
            }
            else {
                this.assetType = "Tài sản cố định";
            }
            let date = new Date(this.assetSelect.dateAdded);
            date.setMonth(date.getMonth() + this.assetSelect.monthOfDepreciation);
            this.dateEndDepreciation = moment(date).format('YYYY-MM-DD');
            this.getAssetGroup();
            this.calculateRemaining();
        });
    }

    getAssetGroup(): void {
        this._assetgroupService.getAssetGroupByAssetID(this.assetSelect.assetGrouptId).subscribe(result => {
            if (result != null)
                this.assetGroup = result;
        });
    }

    filterListAssetInUse(): void {
        this.listRevoke.forEach(revoke => {
            this.listAssetInUse = this.listAssetInUse.filter(item => {
                if(item.assetId.toLowerCase() != revoke.assetId.toLowerCase()){
                    return item;
                }
            }
            );
        });

        if (this.listAssetInUse.length > 0) {
            this.getAssetByID(this.listAssetInUse[0].assetId);
            this.revoke.assetId = this.listAssetInUse[0].assetId;
        }
    }

    getListRevokeNotApproved(): void {
        this._revokeService.getListRevokeNotApproved().subscribe(
            result => {
                this.listRevoke = result;
                this.filterListAssetInUse();
            }
        );
    }

    calculateRemaining() : number {
        let dateNow = moment();
        let dateAdd = moment(this.assetSelect.dateAdded);
        console.log(dateNow);
        let diffDate : number = dateNow.diff(dateAdd, "months");
        this.remainingOfLiquidation = (this.assetSelect.depreciationValue/this.assetSelect.monthOfDepreciation)*(this.assetSelect.monthOfDepreciation - diffDate);
        console.log(this.remainingOfLiquidation);
        console.log(diffDate);
        console.log(this.assetSelect);
        return this.remainingOfLiquidation;
    }

    openSelectAssetModal() {
        this.assetLookupModal.show();
    }

    modalSaved(assetSaved: AssetDto) {
        this.assetSelect.assetId = assetSaved.assetId;
        this.assetSelect.assetName = assetSaved.assetName;
        this.getAssetByID(this.assetSelect.assetId);
        this.revoke.assetId = assetSaved.assetId;
    }
}
