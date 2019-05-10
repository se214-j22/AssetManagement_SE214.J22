import { CategoryForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { CategoryServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewCategoryModal',
    templateUrl: './view-category-modal.component.html'
})

export class ViewCategoryModalComponent extends AppComponentBase {

    category : CategoryForViewDto = new CategoryForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _categoryService: CategoryServiceProxy
    ) {
        super(injector);
    }

    show(categoryId?: number | null | undefined): void {
        this._categoryService.getCategoryForView(categoryId).subscribe(result => {
            this.category = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}