import { CustomerForViewDto, CustomerForViewDto_QuanLyCongTrinhXayDung } from '../../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { CustomerServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewCustomer_QuanLyCongTrinhXayDungModal',
    templateUrl: './view-customer_QuanLyCongTrinhXayDung-modal.component.html'
})

export class ViewCustomer_QuanLyCongTrinhXayDungModalComponent extends AppComponentBase {

    customer : CustomerForViewDto_QuanLyCongTrinhXayDung = new CustomerForViewDto_QuanLyCongTrinhXayDung();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _customerService: CustomerServiceProxy
    ) {
        super(injector);
    }

    show(customerId?: number | null | undefined): void {
        this._customerService.getCustomerForView_QuanLyCongTrinhXayDung(customerId).subscribe(result => {
            this.customer = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}