import { ScanReportForViewDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { ScanReportServiceProxy } from "@shared/service-proxies/service-proxies";
import { Router } from "@angular/router";
import { ModalDirective } from 'ngx-bootstrap';

@Component({
    selector: 'viewScanReportModal',
    templateUrl: './view-scan-report-modal.component.html'
})

export class ViewScanReportModalComponent extends AppComponentBase {

    scanReport : ScanReportForViewDto;
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
            this.modal.show();
        })

    }
}
