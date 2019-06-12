import {ScanReportForViewDto} from './../../../shared/service-proxies/service-proxies';
import {AppComponentBase} from '@shared/common/app-component-base';
import {AfterViewInit, Injector, Component, ViewChild} from '@angular/core';
import {ScanReportServiceProxy} from '@shared/service-proxies/service-proxies';
import {Router} from '@angular/router';
import {ModalDirective} from 'ngx-bootstrap';

@Component({
    selector: 'viewScanReportModal',
    templateUrl: './view-scan-report-modal.component.html'
})

export class ViewScanReportModalComponent extends AppComponentBase {
    objectKeys = Object.keys;
    scannedDate: string;
    scanReportData: object;
    scanReport: ScanReportForViewDto;
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _scanReportService: ScanReportServiceProxy
    ) {
        super(injector);
    }

    close() {
        this.modal.hide();
    }

    show(scanReportId?: number | null | undefined): void {

        this._scanReportService.getScanReportForView(scanReportId).subscribe(result => {
            this.scanReport = result;
            this.scanReportData = JSON.parse(this.scanReport.scannedData);
            this.scannedDate = new Date(this.scanReport.createdDate).toLocaleTimeString('vi') + ' ' + new Date(this.scanReport.createdDate).toLocaleDateString('vi')
            this.modal.show();
        });

    }

    computeLabel(label: string): string {
        switch (label) {
            case 'cpuManufacture':
                return 'Nhà sản xuất CPU';
            case 'cpuCores':
                return 'Xung nhịp CPU';
            case 'biosMaker':
                return 'Nhà sản xuất Bios';
            case 'boardMaker':
                return 'Nhà sản xuất bảng mạch chủ';
            case 'ramSlots':
                return 'Số lượng khe RAM';
            case 'memory':
                return 'Bộ nhớ RAM';
            case 'computerName':
                return 'Tên máy tính';
            case 'osInformation':
                return 'Thông tin hệ điều hành';
            case 'macAddress':
                return 'Địa chỉ MAC';
            case 'accountName':
                return 'Tên tài khoản';
            case 'driveName':
                return 'Tên ổ cứng';
            case 'driveType':
                return 'Loại ổ cứng';
            case 'volumeLabel':
                return 'Phân vùng';
            case 'driveFormat':
                return 'Loại ổ cứng';
            case 'availableFreeSpace':
                return 'Bộ nhớ khả dụng';
            case 'totalFreeSpace':
                return 'Tổng bộ nhớ trống';
            case 'totalSize':
                return 'Tổng bộ nhớ';
            case 'drives':
                return 'Thông tin ổ cứng';
            case 'displayName':
                return 'Tên phần mềm';
            case 'size':
                return 'Kích thước (Bytes)';
            case 'version':
                return 'Phiên bản';
            case 'installDate':
                return 'Ngày cài đặt';
            case 'publisher':
                return 'Nhà cung cấp';
            case 'installLocation':
                return 'Đường dẫn cài đặt';
            case 'cdROM':
                return 'CD ROM';
            default:
                return label;
        }
    }
}
