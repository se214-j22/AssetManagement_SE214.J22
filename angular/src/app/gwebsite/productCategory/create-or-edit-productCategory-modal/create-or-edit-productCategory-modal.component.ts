import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { ComboboxItemDto } from '@shared/service-proxies/service-proxies';
import { NewPCDto, StatusEnum } from '../dto/productCategory.dto';

@Component({
    selector: 'createOrEditProductCategoryModal',
    templateUrl: './create-or-edit-productCategory-modal.component.html',
    styleUrls: ['./create-or-edit-productCategory-modal.component.css']
})
export class CreateOrEditProductCategoryModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('productCategoryCombobox') productCategoryCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    public isCreated = false;

    public newProductCategory: NewPCDto;
    public pcCode = '';
    public pcName = '';
    public pcNote = '';
    public status = StatusEnum.Open;
    public statusEnum = StatusEnum;
    public isCheckStatus = false;

    constructor(
        injector: Injector,
        private _apiService: WebApiServiceProxy
    ) {
        super(injector);
    }

    show(): void {
        this.active = true;
        this.saving = false;

        this.isCheckStatus = false;
        this.pcCode = '';
        this.pcName = '';
        this.pcNote = '';

        // this._apiService.getForEdit('api/MenuClient/GetMenuClientForEdit', productCategoryId).subscribe(result => {
        //     this.productCategory = result.menuClient;
        //     this.productCategorys = result.menuClients;
        //     this.modal.show();
        //     setTimeout(() => {
        //             $(this.productCategoryCombobox.nativeElement).selectpicker('refresh');
        //     }, 0);
        // });

        this.modal.show();

    }

    save(): void {
        this.primengTableHelper.showLoadingIndicator();
        if (this.pcCode && this.pcCode !== '' && this.pcName && this.pcName !== '') {
            this.saving = true;
            let status = this.isCheckStatus ? StatusEnum.Open : StatusEnum.Close;

            this.newProductCategory = new NewPCDto(this.pcCode, this.pcName, status, this.pcNote);

            this.insertProductCategory();

            //trước khi add nhớ check duplicat code.
        }
        this.primengTableHelper.hideLoadingIndicator();
    }

    insertProductCategory() {
        this._apiService.post('api/ProductType/CreateProductCatalogAsync', this.newProductCategory)
            .pipe(finalize(() => this.saving = false))
            .subscribe(result => {
                if (result) {
                    this.notify.info(this.l('CreatedSuccessfully'));
                    this.isCreated = true;
                    this.close();
                    this.modalSave.emit(null);
                }
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
