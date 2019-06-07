import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { PhongBanServiceProxy, PhongBanDto, CapPhatServiceProxy} from '@shared/service-proxies/service-proxies';
import { SanPhamServiceProxy,SanPhamDto } from '@shared/service-proxies/service-proxies';
import { CreateOrEditCapPhatModalComponent } from './create-or-edit-cap-phat-modal.component';
import { ViewCapPhatModalComponent } from './view-cap-phat-modal.component';

@Component({
  templateUrl: './cap-phat.component.html',
  animations: [appModuleAnimation()]
})
export class CapPhatComponent extends AppComponentBase implements OnInit {

  /**
   * @ViewChild là dùng get control và call thuộc tính, functions của control đó
   */
  @ViewChild('dataTable') dataTable: Table;
  @ViewChild('paginator') paginator: Paginator;
  @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditCapPhatModalComponent;
  @ViewChild('viewCapPhatModal') viewCapPhatModal: ViewCapPhatModalComponent;

  /**
   * tạo các biến dể filters
   */
  phongBanId: number;
  selectedPhongBan: PhongBanDto;
  phongBanList: PhongBanDto[];

  selectedSanPham: SanPhamDto;
  sanPhamList: SanPhamDto[];

  constructor(
      injector: Injector,
      private _capPhatService: CapPhatServiceProxy,
      private _phongBanService: PhongBanServiceProxy,
      private _sanPhamService: SanPhamServiceProxy,
      private _activatedRoute: ActivatedRoute,
  ) {
      super(injector);
  }

  ngOnInit(): void {
  }

  ngAfterViewInit(): void {
      setTimeout(() => {
          this.init();
      });
  }

  init(): void {
      //get params từ url để thực hiện filter
      this._activatedRoute.params.subscribe((params: Params) => {
          this.phongBanId = params['phongBanId'] || '';
          //this.reloadList(this.phongBanId, null);
      });
      this.loadPhongBanList();
      this.loadSanPhamList();
  }

  getCapPhats(event?: LazyLoadEvent) {
      if (!this.paginator || !this.dataTable || !event) {
          return;
      }

      //show loading trong gridview
      this.primengTableHelper.showLoadingIndicator();

      /**
       * mặc định ban đầu lấy hết dữ liệu nên dữ liệu filter = null
       */

      this.reloadList(event);

  }

  reloadList(event?: LazyLoadEvent) {
      this._capPhatService.getCapPhatsByFilter(
          this.phongBanId,
          this.primengTableHelper.getSorting(this.dataTable),
          this.primengTableHelper.getMaxResultCount(this.paginator, event),
          this.primengTableHelper.getSkipCount(this.paginator, event),
      ).subscribe(result => {
          this.primengTableHelper.totalRecordsCount = result.totalCount;
          console.log(result);
          this.primengTableHelper.records = result.items;
          this.primengTableHelper.hideLoadingIndicator();
      });
  }

  deleteCapPhat(id): void {
      this._capPhatService.deleteCapPhat(id).subscribe(() => {
          this.reloadPage();
      })
  }

  applyFilters(): void {
      //truyền params lên url thông qua router
      this.reloadList(null);

      if (this.paginator.getPage() !== 0) {
          this.paginator.changePage(0);
          return;
      }
  }

  reloadPage(): void {
      this.paginator.changePage(this.paginator.getPage());
  }

  loadPhongBanList() {
      this._phongBanService.getPhongBansByFilter(undefined, undefined, undefined, undefined, undefined
      ).subscribe(result => {
          console.log(result);
          this.phongBanList = result.items;
      });
  }3

  loadSanPhamList() {
    this._sanPhamService.getSanPhamsByFilter(undefined, undefined, undefined, undefined, undefined,undefined, undefined, undefined
    ).subscribe(result => {
        console.log(result);
        this.sanPhamList = result.items;
    });
}

  SelectPhongBan(phongBan: PhongBanDto) {
      this.selectedPhongBan = phongBan;
  }
  SelectSanPham(sanPham: SanPhamDto) {
    this.selectedSanPham = sanPham;
  }

  createCapPhat() {
      this.createOrEditModal.show();
  }

  /**
   * Tạo pipe thay vì tạo từng hàm truncate như thế này
   * @param text
   */
  truncateString(text): string {
      return abp.utils.truncateStringWithPostfix(text, 32, '...');
  }

}
