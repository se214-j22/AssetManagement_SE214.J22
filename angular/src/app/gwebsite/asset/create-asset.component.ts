import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { AssetServiceProxy, AssetInput, AssetGroupDto, AssetGroupServiceProxy, AssetGroupForViewDto } from '@shared/service-proxies/service-proxies';
import { moment } from 'ngx-bootstrap/chronos/test/chain';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';

@Component({
    selector: 'createAssetComponent',
    templateUrl: './create-asset.component.html'
})
export class CreateAssetComponent extends AppComponentBase {


    // @ViewChild('createOrEditModal') modal: ModalDirective;
    // @ViewChild('assetCombobox') assetCombobox: ElementRef;
    // @ViewChild('iconCombobox') iconCombobox: ElementRef;
    // @ViewChild('dateInput') dateInput: ElementRef;


    /**
    * @Output dùng để public event cho component khác xử lý
    */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    asset: AssetInput = new AssetInput();
    assetGroups: AssetGroupDto[];
    assetGroup: AssetGroupForViewDto = new AssetGroupForViewDto();
    assetId: string;
    total: number;

    constructor(
        injector: Injector,
        private _assetService: AssetServiceProxy,
        private _assetGroupService: AssetGroupServiceProxy,
        private _router: Router
    ) {
        super(injector);
    }

    ngOnInit(): void {
        //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
        //Add 'implements OnInit' to the class.
        this.assetId = '';
        this.saving = false;
        this.getTotalAsset();

        this.asset.dateAdded = moment().format('YYYY-MM-DD');
    }

    // show(assetId?: number | null | undefined): void {
    //     this.saving = false;
    //     this.getTotalAsset();

    //     this._assetService.getAssetForEdit(assetId).subscribe(result => {
    //         this.asset = result;
    //         this.modal.show();
    //         if (!this.asset.id) {
    //             this.asset.dateAdded = moment().format('YYYY-MM-DD');
    //         }
    //         else {
    //             this.getAssetGroupByID(this.asset.assetGrouptId);
    //         }
    //     });
    // }

    save(): void {
        let input = this.asset;
        this.saving = true;
        this._assetService.createOrEditAsset(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            // this.close();
            this.goBack();
        })
    }

    // close(): void {
    //     // this.modal.hide();
    //     this.modalSave.emit(null);
    // }

    clear(): void {
      this.asset = new AssetInput();
      this.assetGroups = [];
      this.assetGroup = new AssetGroupForViewDto();
      this.assetId = '';
      this.asset.dateAdded = moment().format('YYYY-MM-DD');
    }

    goBack(): void {
      this._router.navigateByUrl('app/gwebsite/asset');
    }

    getListAssetGroupsByAssetType(assetType: number): void {
        this._assetGroupService.getListAssetGroupsByAssetType(assetType).subscribe(result => {
            this.assetGroups = result;
            if (this.assetGroup.assetGrouptId != null) {
                this.getAssetGroupByID(this.assetGroups[0].assetGrouptId);
            }
        });
    }

    getTotalAsset(): void {
        this._assetService.getTotalAsset().subscribe(result => {
            if (result == null) {
                this.total = 1;
            }
            else {
                this.total = result + 1;
            }
        });
    }

    getAssetGroupByID(assetGroupId: string): void {
        this._assetGroupService.getAssetGroupByAssetID(assetGroupId).subscribe(result => {
            this.assetGroup = result;

            if (this.asset.assetType == 0) {
                this.assetId = 'C' + this.assetGroup.assetGrouptId + this.formatAssetID(this.total);
            }
            else {
                this.assetId = 'T' + this.assetGroup.assetGrouptId + this.formatAssetID(this.total);
            }
            this.asset.assetId = this.assetId.toUpperCase();
            if (!this.asset.id) {
                this.asset.monthOfDepreciation = this.assetGroup.monthOfDepreciation;
            }
        });
    }

    formatAssetID(total: number): string {
        let result: string = total + '';
        let len: number = 6 - result.length;
        for (let index = 0; index < len; index++) {
            result = '0' + result;
        }
        return result;
    }

    onChange(event: any) {
        // console.log(this.asset.dateAdded);
        // console.log(event.target.value);
    }
}