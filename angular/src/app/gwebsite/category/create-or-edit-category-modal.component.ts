import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CategoryServiceProxy, CategoryInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditCategoryModal',
    templateUrl: './create-or-edit-category-modal.component.html'
})
export class CreateOrEditCategoryModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('categoryCombobox') categoryCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
    * @Output dùng để public event cho component khác xử lý
    */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    category: CategoryInput = new CategoryInput();

    constructor(
        injector: Injector,
        private _categoryService: CategoryServiceProxy
    ) {
        super(injector);
    }

    show(categoryId?: number | null | undefined): void {
        this.saving = false;


        this._categoryService.getCategoryForEdit(categoryId).subscribe(result => {
            this.category = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.category;
        this.saving = true;
        this._categoryService.createOrEditCategory(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}