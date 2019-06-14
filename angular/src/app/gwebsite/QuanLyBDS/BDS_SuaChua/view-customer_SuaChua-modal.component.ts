import { CustomerForViewDto, CustomerForViewDto_SuaChua } from '../../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { CustomerServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewCustomer_SuaChuaModal',
    templateUrl: './view-customer_SuaChua-modal.component.html'
})

export class ViewCustomer_SuaChuaModalComponent extends AppComponentBase {

    customer : CustomerForViewDto_SuaChua = new CustomerForViewDto_SuaChua();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _customerService: CustomerServiceProxy
    ) {
        super(injector);
    }

    show(customerId?: number | null | undefined): void {
        this._customerService.getCustomerForView_SuaChua(customerId).subscribe(result => {
            this.customer = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}