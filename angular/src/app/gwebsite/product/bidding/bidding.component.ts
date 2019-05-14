import { Component, OnInit, ViewChild, Output, EventEmitter, Injector } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { AppComponentBase } from '@shared/common/app-component-base';
import { SupplierServiceProxy, SupplierDto, BiddingSaved } from '@shared/service-proxies/service-proxies';
import { SelectItem } from 'primeng/primeng';
import * as moment from 'moment';
@Component({
  selector: 'app-bidding',
  templateUrl: './bidding.component.html',
  styleUrls: ['./bidding.component.css']
})
export class BiddingComponent extends AppComponentBase {
  val1 = 0;
  @ViewChild('biddingModel') modal: ModalDirective;
  @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
  suppliers: SelectItem[] = [];
  active = false;
  selectedSupplierId = 0;
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
  rangeDates: Date[];
  constructor(injector: Injector, private _supplierServiceProxy: SupplierServiceProxy) {
    super(injector);
  }


  show(supplierId?: number | null | undefined): void {
    this.selectedSupplierId = supplierId;
    this.active = true;
    this.modal.show();
    this.getSupplierByProduct();
  }
  onChangeRadioButton() {
    console.log(this.val1);
  }
  save() {
    this._supplierServiceProxy.changeOwnerBiddingProduct(new BiddingSaved({
      startDate: this.rangeDates ? moment(this.rangeDates[0]) : moment(new Date()),
      endDate: this.rangeDates && this.rangeDates.length > 1 ? moment(this.rangeDates[1]) : moment(new Date()),
      status: 1,
      productId: this.selectedSupplierId, supplierId: this.val1, biddingType: this.biddingType, price: this.price
    })).subscribe(result => {
      console.log(result);
      this.modalSave.emit(null);
      this.close();
    });
  }
  getSupplierByProduct() {
    this._supplierServiceProxy.getSupplierByProduct('', '', 1000, 0, +this.selectedSupplierId).subscribe(suppliers => {
      this.suppliers = [];
      suppliers.items.map(i => {
        i.biddings.map(p => this.val1 = p.status === 1 ? i.id : 0);
        this.suppliers.push({ value: i.id, label: i.name });
      });

    });
  }
  close(): void {
    this.active = false;
    this.modal.hide();
  }
}
