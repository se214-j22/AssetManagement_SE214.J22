import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { UseAssetServiceProxy, UseAssetInput, AssetForViewDto, AssetGroupForViewDto, AssetServiceProxy, AssetGroupServiceProxy, OrganizationUnitDto, OrganizationUnitServiceProxy, OrganizationUnitUserListDto, UseAssetDto, AssetDto, CustomerServiceProxy, CustomerDto } from '@shared/service-proxies/service-proxies';
import { moment } from 'ngx-bootstrap/chronos/test/chain';

import { AssetLookupModalComponent } from '../../shared/common/lookup/asset-lookup-modal.component';


@Component({
    selector: 'createOrEditUseAssetModal',
    templateUrl: './create-or-edit-useasset-modal.component.html'
})
export class CreateOrEditUseAssetModalComponent extends AppComponentBase {
    @ViewChild('assetLookup') assetLookupModal: AssetLookupModalComponent;

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('useassetCombobox') useassetCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
    * @Output dùng để public event cho component khác xử lý
    */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    testName = '';

    useasset: UseAssetInput = new UseAssetInput();

    listAssetInStock: AssetForViewDto[];
    assetSelect: AssetForViewDto = new AssetForViewDto();
    assetType: string = "";
    assetGroup: AssetGroupForViewDto = new AssetGroupForViewDto();
    listUseAsset: UseAssetDto[];
    listOrganizationUnit: OrganizationUnitDto[];
    listOrganizationUnitUser: OrganizationUnitUserListDto[];
    endOfLiquidation: string;

    userName: string = "";
    unitName: string = "";

    customers: CustomerDto[] = [];
    customersByOrganization: CustomerDto[] = [];

    constructor(
        injector: Injector,
        private _useassetService: UseAssetServiceProxy,
        private _assetService: AssetServiceProxy,
        private _assetgroupService: AssetGroupServiceProxy,
        private _organizationunitService: OrganizationUnitServiceProxy,
        private _organizationunituserService: OrganizationUnitServiceProxy,
        private _customerService: CustomerServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
        //Add 'implements OnInit' to the class.
        this.getListAssetsInStock();
        this.getOrganizationUnit();
        if(this.listAssetInStock[0].assetId) this._assetService.getAssetByAssetID(this.listAssetInStock[0].assetId).subscribe(result => {
            this.assetSelect = result;
        });
    }

    show(useassetId?: number | null | undefined): void {
        this.saving = false;
        this.getListAssetsInStock();
        this._useassetService.getUseAssetForEdit(useassetId).subscribe(result => {
            this.useasset = result;
            // this.modal.show();
            if (!this.useasset.id) {
                this.useasset.dateExport = moment().format('YYYY-MM-DD');
            }
            else {
                this.getAssetByID(this.useasset.assetId);
            }
            this._customerService.getCustomersByFilter('', '', 1000, 0).subscribe(result => {
                this.customers = result.items;
            });
            this.modal.show();
        })
    }

    save(): void {
        let input = this.useasset;
        this.saving = true;
        this._useassetService.createOrEditUseAsset(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this._assetService.updateAssetStatusUsing(this.assetSelect.assetId);
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
            this.getListUseAssetNotApproved();
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
        this.listUseAsset.forEach(useAss => {
            this.listAssetInStock = this.listAssetInStock.filter(item => {
                if (item.assetId.toLowerCase() != useAss.assetId.toLowerCase()) {
                    return item;
                }
            }
            );
        });

        if (this.listAssetInStock.length > 1) {
            this.getAssetByID(this.listAssetInStock[0].assetId);
            this.useasset.assetId = this.listAssetInStock[0].assetId;
        }
    }

    getListUseAssetNotApproved(): void {
        this._useassetService.getListUsseAssetNoteApproved().subscribe(
            result => {
                this.listUseAsset = result;
                this.filterListAssetInStock();
            }
        );
    }

    getOrganizationUnit(): void {
        this._organizationunitService.getOrganizationUnits().subscribe(
            result => {
                if (result != null)
                    this.listOrganizationUnit = result.items;
            }
        );
    }

    getOrgannizationUnitUser(id: number): void {
        // this._organizationunituserService.getListUsersOrganizationUnit(id).subscribe(
        //     result => {
        //         if (result != null)
        //             this.listOrganizationUnitUser = result.items;
        //     }
        // )
        this.useasset.userId = undefined;
        this.customersByOrganization = this.customers.filter(customer => {
            console.log(`${customer.organizationId} == ${id}`);
            if (customer.organizationId == id) {
                return true;
            }

            return false;
        });
        console.error(id);
        console.log(this.customersByOrganization);
        console.error(this.customers);
    }

    calculateEndOfLiqidation(): void {
        let dateAdd = moment(this.assetSelect.dateAdded);
        this.endOfLiquidation = dateAdd.add(this.assetSelect.monthOfDepreciation, "months").format('YYYY-MM-DD');
    }

    openSelectAssetModal() {
        this.assetLookupModal.show();
    }

    modalSaved(assetSaved: AssetDto) {
        this.assetSelect.assetId = assetSaved.assetId;
        this.assetSelect.assetName = assetSaved.assetName;
        this.getAssetByID(this.assetSelect.assetId);
        this.useasset.assetId = assetSaved.assetId;
    }

    setAssetIdNull() {
        this.assetSelect = new AssetForViewDto();
        this.getAssetByID('');
    }
}