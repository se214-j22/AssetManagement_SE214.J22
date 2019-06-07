import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, AfterViewInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { ScanReportServiceProxy, ScanReportInput } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditScanReportModal',
    templateUrl: './create-or-edit-scan-report-modal.component.html'
})
export class CreateOrEditScanReportModalComponent extends AppComponentBase implements AfterViewInit {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('scanReportCombobox') scanReportCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    scanReport: ScanReportInput = new ScanReportInput();

    constructor(
        injector: Injector,
        private _scanReportService: ScanReportServiceProxy,
        private _apiService: WebApiServiceProxy
    ) {
        super(injector);
    }

    ngAfterViewInit(): void {
        let t = this;
    }



    show(scanReportId?: number | null | undefined): void {
        this.saving = false;


        this._scanReportService.getScanReportForEdit(scanReportId).subscribe(result => {
            this.scanReport = result;
            // debugger
            this.modal.show();

        })
    }

    save(): void {
        let input = this.scanReport;
        this.saving = true;
        this._scanReportService.createOrEditScanReport(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
