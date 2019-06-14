import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { LiquidationServiceProxy, LiquidationInput, AssetForViewDto, AssetServiceProxy, AssetGroupServiceProxy, AssetGroupForViewDto, LiquidationDto, AssetDto } from '@shared/service-proxies/service-proxies';
import { moment } from 'ngx-bootstrap/chronos/test/chain';
import { AssetLookupModalComponent } from '@app/shared/common/lookup/asset-lookup-modal.component';


@Component({
    selector: 'createOrEditLiquidationModal',
    templateUrl: './create-or-edit-liquidation-modal.component.html'
})
export class CreateOrEditLiquidationModalComponent extends AppComponentBase {
    @ViewChild('assetLookup') assetLookupModal: AssetLookupModalComponent;

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('liquidationCombobox') liquidationCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
    * @Output dùng để public event cho component khác xử lý
    */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    liquidation: LiquidationInput = new LiquidationInput();
    listAssetInStock: AssetForViewDto[];
    assetSelect: AssetForViewDto = new AssetForViewDto();
    assetType: string = "";
    assetGroup: AssetGroupForViewDto = new AssetGroupForViewDto();
    dateEndDepreciation: string = "";
    listLiquidation: LiquidationDto[];
    constructor(
        injector: Injector,
        private _liquidationService: LiquidationServiceProxy,
        private _assetService: AssetServiceProxy,
        private _assetgroupService: AssetGroupServiceProxy,
    ) {
        super(injector);
    }

    ngOnInit(): void {
        //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
        //Add 'implements OnInit' to the class.
        this.getListAssetsInStock();
    }

    show(liquidationId?: number | null | undefined): void {
        this.saving = false;
        this.getListAssetsInStock();
        this._liquidationService.getLiquidationForEdit(liquidationId).subscribe(result => {
            this.liquidation = result;
            if (!this.liquidation.id) {
                this.liquidation.liquidationDate = moment().format('YYYY-MM-DD');
            }
            else {
                this.getAssetByID(this.liquidation.assetID);
            }
            this.modal.show();
            console.log(this);
        })
    }

    save(): void {
        let input = this.liquidation;
        this.saving = true;
        this._liquidationService.createOrEditLiquidation(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }

    getListAssetsInStock(): void {
        this._assetService.getListAssetsInStock().subscribe(result => {
            this.listAssetInStock = result;
            this.getListLiquidationNoteApproved();
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
        });
    }

    getAssetGroup(): void {
        this._assetgroupService.getAssetGroupByAssetID(this.assetSelect.assetGrouptId).subscribe(result => {
            if (result != null)
                this.assetGroup = result;
        });
    }

    filterListAssetInStock(): void {
        this.listLiquidation.forEach(liqui => {
            this.listAssetInStock = this.listAssetInStock.filter(item => {
                if (item.assetId.toLowerCase() != liqui.assetID.toLowerCase()) {
                    return item;
                }
            }
            );
        });

        if (this.listAssetInStock.length > 0) {
            this.getAssetByID(this.listAssetInStock[0].assetId);
            this.liquidation.assetID = this.listAssetInStock[0].assetId;
        }
    }

    getListLiquidationNoteApproved(): void {
        this._liquidationService.getListLiquidationNoteApproved().subscribe(
            result => {
                this.listLiquidation = result;
                this.filterListAssetInStock();
            }
        );
    }

    openSelectAssetModal() {
        this.assetLookupModal.show();
    }

    modalSaved(assetSaved: AssetDto) {
        this.assetSelect.assetId = assetSaved.assetId;
        this.assetSelect.assetName = assetSaved.assetName;
        this.getAssetByID(this.assetSelect.assetId);
        this.liquidation.assetID = assetSaved.assetId;
    }
}