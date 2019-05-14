import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { ComboboxItemDto } from '@shared/service-proxies/service-proxies';
import { PlanDto, PurchaseProducts, StatusEnum } from '../dto/plan.dto';

@Component({
    selector: 'createOrEditPlanModal',
    templateUrl: './create-or-edit-plan-modal.component.html',
    styleUrls: ['./create-or-edit-plan-modal.component.css']
})
export class CreateOrEditPlanModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('planCombobox') planCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    isEdit = false;

    plan: PlanDto = new PlanDto();
    purchaseProducts: PurchaseProducts = new PurchaseProducts();
    products: ComboboxItemDto[] = [];

    public statusData = [
        {
          id: StatusEnum.Draft,
          name: 'Draft'
        },
        {
          id: StatusEnum.Official,
          name: 'Official '
        }
      ];

    constructor(
        injector: Injector,
        private _apiService: WebApiServiceProxy
    ) {
        super(injector);
    }

    show(planId?: number | null | undefined): void {
        this._apiService.get('api/Products/GetProducts').subscribe(result => {
            this.products = result.items;
            this.modal.show();
            setTimeout(() => {
                    $(this.planCombobox.nativeElement).selectpicker('refresh');
            }, 0);
        });

        if (planId) {
            this._apiService.getForEdit('api/MenuClient/GetMenuClientForEdit', planId).subscribe(result => {
                // tiennnnnnnnnnnnnnnnnnnnnnnnnnnnn
                this.plan = result.menuClient;
                this.modal.show();
                setTimeout(() => {
                        $(this.planCombobox.nativeElement).selectpicker('refresh');
                }, 0);
            });

            this.isEdit = true;
        } else {
            this.plan.id = 0;
            this.plan.comment = '';
            this.plan.departmentId = 0;
            this.plan.status = 1;
            this.purchaseProducts.quantity = 0;
            this.purchaseProducts.productId = 0;
            this.plan.purchaseProducts = this.purchaseProducts;
        }

        this.active = true;
    }

    save(): void {
        let input = this.plan;
        this.saving = true;
        if (input.id) {
            this.updatePlan();
        } else {
            this.insertPlan();
        }
    }

    insertPlan() {
        // tiennnnnnnnnnnnnnnnnnnnnnnnnnnnn
        this._apiService.post('api/Purchase/CreatePurchase', this.plan)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    updatePlan() {
        // tiennnnnnnnnnnnnnnnnnnnnnnnnnnnn
        this._apiService.put('api/MenuClient/UpdateMenuClient', this.plan)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
