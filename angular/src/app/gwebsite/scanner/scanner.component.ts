import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { SanPhamServiceProxy } from '@shared/service-proxies/service-proxies';
import { ScanModalComponent } from './scan-modal.component';
import { ZXingScannerComponent } from '@zxing/ngx-scanner';
import { Result } from '@zxing/library';
import * as moment from 'moment/moment.js';

@Component({
  templateUrl: './scanner.component.html',
  animations: [appModuleAnimation()],
  styleUrls: ['./scanner.component.css']
})
export class ScannerComponent extends AppComponentBase implements AfterViewInit, OnInit {

  /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('scanner') scanner: ZXingScannerComponent;
    @ViewChild('ScanModal') ScanModal: ScanModalComponent;

    hasDevices: boolean;
    hasPermission: boolean;
    qrResultString: string;
    qrResult: Result;
    QR_CODE:string;
    
    availableDevices: MediaDeviceInfo[];
    currentDevice: MediaDeviceInfo;
    /**
     * tạo các biến dể filters
     */
    sanPhamName: string;
    maSP: string;
    filterDate: moment.Moment;

    constructor(
      injector: Injector,
      private _sanPhamService: SanPhamServiceProxy,
      private _activatedRoute: ActivatedRoute,
     
    ) {
        super(injector);
    }

    /**
     * Hàm xử lý trước khi View được init
     */
    ngOnInit(): void {
        this.scanner.camerasFound.subscribe((devices: MediaDeviceInfo[]) => {
            this.hasDevices = true;
            this.availableDevices = devices;
            this.currentDevice = this.availableDevices[0];
      
            // selects the devices's back camera by default
            // for (const device of devices) {
            //     if (/back|rear|environment/gi.test(device.label)) {
            //         this.scanner.changeDevice(device);
            //         this.currentDevice = device;
            //         break;
            //     }
            // }
          });
      
        this.scanner.camerasNotFound.subscribe(() => this.hasDevices = false);
       // this.scanner.scanComplete.subscribe((result: Result) => {this.qrResult = result; this.ScanModal.showInfo(this.qrResultString)});
        this.scanner.scanComplete.subscribe((result: Result) => {this.qrResult = result});
        this.scanner.permissionResponse.subscribe((perm: boolean) => this.hasPermission = perm);
    }

    /**
     * Hàm xử lý sau khi View được init
     */
    ngAfterViewInit(): void {
        setTimeout(() => {
            this.init();
        });
    }
    
    /**
     * Hàm get danh sách LoaiTaiSan
     * @param event
     */
    getSanPhams(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) {
            return;
        }

        //show loading trong gridview
        this.primengTableHelper.showLoadingIndicator();

        /**
         * mặc định ban đầu lấy hết dữ liệu nên dữ liệu filter = null
         */

        this.reloadList(this.filterDate, event);

    }

    reloadList(filterText, event?: LazyLoadEvent) {
        this._sanPhamService.getSanPhamsByFilter(null,null,null,filterText,null, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    deleteSanPham(id): void {
        this._sanPhamService.deleteSanPham(id).subscribe(() => {
            this.reloadPage();
        })
    }

    init(): void {
        //get params từ url để thực hiện filter
        this._activatedRoute.params.subscribe((params: Params) => {
            //this.sanPhamName = params['name'] || '';
            this.filterDate=(moment)(new Date());
            this.reloadList(this.filterDate, null);
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    
    applyFilters(): void {
        //truyền params lên url thông qua router
        this.reloadList(this.filterDate, null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
        
    }
    /**
     * Tạo pipe thay vì tạo từng hàm truncate như thế này
     * @param text
     */
    truncateString(text): string {
        return abp.utils.truncateStringWithPostfix(text, 32, '...');
    }

    /* Scan QR Code from Video Camera */
    displayCameras(cameras: MediaDeviceInfo[]) {
        console.debug('Devices: ', cameras);
        this.availableDevices = cameras;
      }
    
      handleQrCodeResult(resultString: string) {
        console.debug('Result: ', resultString);
        this.qrResultString = resultString;
        this.ScanModal.showInfo(this.qrResultString);
      }
    
      onDeviceSelectChange(selectedValue: string) {
        console.debug('Selection changed: ', selectedValue);
        this.currentDevice = this.scanner.getDeviceById(selectedValue);
      }
    
      stateToEmoji(state: boolean): string {
    
        const states = {
          // not checked
          undefined: '❔',
          // failed to check
          null: '⭕',
          // success
          true: '✔',
          // can't touch that
          false: '❌'
        };
    
        return states['' + state];
      }
}
