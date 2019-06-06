import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { BidProfileDto } from '../dto/bidProfile.dto';
import { SelectItem } from 'primeng/primeng';
import * as moment from 'moment';
@Component({
    selector: 'createOrEditBidProfileModal',
    templateUrl: './create-or-edit-bidProfile-modal.component.html',
    styleUrls: ['./create-or-edit-bidProfile-modal.component.css']
})
export class CreateOrEditBidProfileModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    biddingType = 0;
    price = 0;
    biddingTypes = [
        { label: 'Select bidding type', value: null },
        { label: 'Đấu thầu', value: 1 },
        { label: 'Chuyển nhượng', value: 2 },
        { label: 'Gì đó', value: 3 }

    ];
    status = [
        { label: 'Select status', value: null },
        { label: 'Trúng thầu', value: 1 },
        { label: 'Dự thầu', value: 2 },
        { label: 'Hết hạn', value: 3 }

    ];
    active = false;
    saving = false;
    val1 = 0;
    edit = false;
    bidProfile: BidProfileDto = new BidProfileDto();

    selectItems: SelectItem[] = [];
    suppliers: SelectItem[] = [];
    rangeDates: Date[];

    constructor(injector: Injector) {
        super(injector);

    }
    getDataProduct() {

    }
    getSupplierByProduct() {

    }
    dropdownChange() {
        this.getSupplierByProduct();
    }


    show(bidProfile?: any | null | undefined): void {
        this.active = true;
        this.edit = bidProfile !== undefined;
        this.modal.show();

    }
    dropdownSupplierChange() {

    }

    save(): void {
        let input = this.bidProfile;
        this.saving = true;

    }



    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
