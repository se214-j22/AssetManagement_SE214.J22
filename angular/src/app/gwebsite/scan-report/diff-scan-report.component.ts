import {ScanReportForViewDto} from './../../../shared/service-proxies/service-proxies';
import {AppComponentBase} from '@shared/common/app-component-base';
import {AfterViewInit, Injector, Component, ViewChild} from '@angular/core';
import {ScanReportServiceProxy} from '@shared/service-proxies/service-proxies';
import {Router} from '@angular/router';
import {ModalDirective} from 'ngx-bootstrap';
import * as JsonDiffPatch from 'jsondiffpatch';
import {of as _observableOf} from '@node_modules/rxjs';

@Component({
    selector: 'diffScanReport',
    templateUrl: './diff-scan-report.component.html',
})
export class DiffScanReportComponent extends AppComponentBase {
    objectKeys = Object.keys;
    leftSelected: number;
    rightSelected: number;
    leftScanReport: object;
    rightScanReport: object;
    diffSelectOtions: Array<object>;
    visualDiff: string;
    diffHandler: JsonDiffPatch.DiffPatcher;
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _scanReportService: ScanReportServiceProxy
    ) {
        super(injector);

        this.diffHandler = new JsonDiffPatch.DiffPatcher({
            objectHash: function(obj, index) {
                if (typeof obj._id !== 'undefined') {
                    return obj._id;
                }
                if (typeof obj.id !== 'undefined') {
                    return obj.id;
                }
                return '$$index:' + index;
            },
        });
    }

    close() {
        this.modal.hide();
    }

    computeDateString(dateString: string) {
        let date = new Date(dateString);
        return date.toLocaleTimeString('vi') + ' ' + date.toLocaleDateString('vi');
    }

    onOpenModal(): void {
        this._scanReportService.getScanReportsByFilter(undefined, undefined, undefined, undefined).subscribe(result => {
            let records = result.items;

            this.diffSelectOtions = records.map(elm => {
                return {
                    'label': `Bản quét #${elm.id} - lúc ${this.computeDateString(elm.createdDate)}`,
                    'value': elm.id
                };
            });

            this.modal.show();
        });
    }
    submitGetDiff(): void {
        // trigger fetch left
        // alert(`${this.rightSelected} ${this.leftSelected}`);



        this.fetchLeftScanReport(this.leftSelected);

    }

    getDiff(): void {
        let diff = this.diffHandler.diff(this.leftScanReport, this.rightScanReport);

        JsonDiffPatch.formatters.html.hideUnchanged();

        this.visualDiff = JsonDiffPatch.formatters.html.format(diff, this.leftScanReport);

        if (this.visualDiff) {
            document.getElementById('visual-diff').innerHTML = this.visualDiff;
        } else {
            document.getElementById('visual-diff').innerHTML = 'Chưa có dữ liệu';
        }
    }

    fetchLeftScanReport(scanReportId?: number | null | undefined): void {
        this._scanReportService.getScanReportForView(scanReportId).subscribe(result => {
            this.leftScanReport = JSON.parse(result.scannedData);
            this.fetchRightScanReport(this.rightSelected);
        });
    }

    fetchRightScanReport(scanReportId?: number | null | undefined): void {
        this._scanReportService.getScanReportForView(scanReportId).subscribe(result => {
            this.rightScanReport = JSON.parse(result.scannedData);
            this.getDiff();

            // alert(this.visualDiff);
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
