import { Component, OnInit, ViewChild, ElementRef, Injector } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Table } from 'primeng/table';
import { Paginator, LazyLoadEvent } from 'primeng/primeng';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { SupplierServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditSupplierComponent } from './create-or-edit-supplier/create-or-edit-supplier.component';

@Component({
  selector: 'app-supplier',
  templateUrl: './supplier.component.html',
  styleUrls: ['./supplier.component.css'],
  animations: [appModuleAnimation()]
})
export class SupplierComponent extends AppComponentBase implements OnInit {
  @ViewChild('textsTable') textsTable: ElementRef;
  @ViewChild('dataTable') dataTable: Table;
  @ViewChild('paginator') paginator: Paginator;
  @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditSupplierComponent;
  selectedCar1: any;
  /**
   * tạo các biến dể filters
   */
  filterText: string;

  constructor(
    injector: Injector,
    private _router: Router,
    private _activatedRoute: ActivatedRoute,
    private _supplierServiceProxy: SupplierServiceProxy
  ) {
    super(injector);
  }

  ngOnInit(): void {
  }




  getSupliers(event?: LazyLoadEvent) {
    if (!this.paginator || !this.dataTable) {
      return;
    }

    //show loading trong gridview
    this.primengTableHelper.showLoadingIndicator();

    /**
     * Sử dụng _apiService để call các api của backend
     */

    this._supplierServiceProxy.getAllBiddingPass(this.filterText, this.primengTableHelper.getSorting(this.dataTable),
      this.primengTableHelper.getMaxResultCount(this.paginator, event),
      this.primengTableHelper.getSkipCount(this.paginator, event)).subscribe(result => {
        this.primengTableHelper.totalRecordsCount = 20;
        this.primengTableHelper.records = result.items;
        this.primengTableHelper.hideLoadingIndicator();
      });
  }

  init(): void {
    //get params từ url để thực hiện filter
    this._activatedRoute.params.subscribe((params: Params) => {
      this.filterText = params['filterText'] || '';


    });
  }





  /**
   * Tạo pipe thay vì tạo từng hàm truncate như thế này
   * @param text
   */
  truncateString(text): string {
    return abp.utils.truncateStringWithPostfix(text, 32, '...');
  }

  //Refresh grid khi thực hiện create or edit thành công
  refreshValueFromModal(): void {
    this.getSupliers();
  }

  //hàm show view create Product
  createProduct() {
    this.createOrEditModal.show();
  }
}
