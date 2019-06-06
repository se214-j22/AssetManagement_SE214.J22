import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { ComboboxItemDto, SupplierServiceProxy, ProductsServiceProxy } from '@shared/service-proxies/service-proxies';
import { BidProfileDto } from '../dto/bidProfile.dto';
import { SelectItem } from 'primeng/primeng';
import * as moment from 'moment';
@Component({
  selector: 'app-view-bid-profile',
  templateUrl: './view-bid-profile.component.html',
  styleUrls: ['./view-bid-profile.component.css']
})
export class ViewBidProfileComponent extends AppComponentBase {

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
  bidProfiles: ComboboxItemDto[] = [];
  selectItems: SelectItem[] = [];
  suppliers: SelectItem[] = [];
  rangeDates: Date[];

  constructor(injector: Injector, private _supplierServiceProxy: SupplierServiceProxy, private _productsServiceProxy: ProductsServiceProxy) {
    super(injector);

  }



  show(bidProfile?: any | null | undefined): void {
    this.active = true;
    this.edit = bidProfile !== undefined;
    this.modal.show();

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
