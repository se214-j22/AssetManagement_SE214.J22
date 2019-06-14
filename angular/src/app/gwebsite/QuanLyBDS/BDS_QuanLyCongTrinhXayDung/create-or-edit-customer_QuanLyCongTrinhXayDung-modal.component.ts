import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CustomerServiceProxy, CustomerInput_QuanLyCongTrinhXayDung } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditCustomer_QuanLyCongTrinhXayDungModal',
    templateUrl: './create-or-edit-customer_QuanLyCongTrinhXayDung-modal.component.html'
})
export class CreateOrEditCustomer_QuanLyCongTrinhXayDungModalComponent extends AppComponentBase {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('customerCombobox') customerCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    customer: CustomerInput_QuanLyCongTrinhXayDung = new CustomerInput_QuanLyCongTrinhXayDung();

    constructor(
        injector: Injector,
        private _customerService: CustomerServiceProxy
    ) {
        super(injector);
    }

    show(customerId?: number | null | undefined): void {
        this.saving = false;


        this._customerService.getCustomerForEdit_QuanLyCongTrinhXayDung(customerId).subscribe(result => {
            this.customer = result;
            this.modal.show();

        })
    }

    save(): void {
        let input = this.customer;
        this.saving = true;
        this._customerService.createOrEditCustomer_QuanLyCongTrinhXayDung(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
