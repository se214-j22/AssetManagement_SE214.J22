import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { AssetGroupServiceProxy, AssetGroupInput, AssetGroupDto } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditAssetGroupModal',
    templateUrl: './create-or-edit-assetgroup-modal.component.html'
})
export class CreateOrEditAssetGroupModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('assetgroupCombobox') assetgroupCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
    * @Output dùng để public event cho component khác xử lý
    */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    assetgroup: AssetGroupInput = new AssetGroupInput();
    assetGroupParents: AssetGroupDto[];

    constructor(
        injector: Injector,
        private _assetgroupService: AssetGroupServiceProxy
    ) {
        super(injector);
    }

    show(assetgroupId?: number | null | undefined): void {
        this.saving = false;


        this._assetgroupService.getAssetGroupForEdit(assetgroupId).subscribe(result => {
            this.assetgroup = result;
            this.modal.show();
            this.getListAssetGroupParent();
        })
    }

    ngOnInit(): void {
        //Called after the constructor, initializing input properties, and the first call to ngOnChanges.
        //Add 'implements OnInit' to the class.
        this.getListAssetGroupParent();
    }

    getListAssetGroupParent(): void {
        if (this.assetgroup.assetGrouptId == '') {
            this._assetgroupService.getListAssetGroups('').subscribe(result => {
                this.assetGroupParents = result;
            });
        }
        else {
            this._assetgroupService.getListAssetGroups(this.assetgroup.assetGrouptId).subscribe(result => {
                this.assetGroupParents = result;
            });
        }

    }

    save(): void {
        this.assetgroup.assetGrouptId = this.assetgroup.assetGrouptId.toUpperCase();
        let input = this.assetgroup;
        this.saving = true;
        this._assetgroupService.createOrEditAssetGroup(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}