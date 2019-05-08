import { TaiSanForViewDto, TaiSanDto } from './../../../shared/service-proxies/service-proxies';
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild, Output, EventEmitter } from "@angular/core";
import { TaiSanServiceProxy } from "@shared/service-proxies/service-proxies";
import { ModalDirective } from 'ngx-bootstrap';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';


@Component({
    selector: 'taisan',
    templateUrl: './taisan.component.html',
    animations: [appModuleAnimation()]
})

export class TaiSanComponent extends AppComponentBase implements AfterViewInit {
    taisan: TaiSanForViewDto = new TaiSanForViewDto();
    maTaiSan: string;
    item: TaiSanDto = new TaiSanDto();
    @Output() dlTraVe: EventEmitter<TaiSanDto> = new EventEmitter<TaiSanDto>();

    @ViewChild('viewModal') modal: ModalDirective;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;


    constructor(
        injector: Injector,
        private _taisanService: TaiSanServiceProxy

    ) {
        super(injector);

    }

    show(): void {
        // this.reloadList(null, null);
        this.modal.show();

    }


    ngAfterViewInit() {


    }


    getTaiSans(event?: LazyLoadEvent) {
        //alert("aaa")
        if (!this.paginator || !this.dataTable) {
            return;
        }
        this.primengTableHelper.showLoadingIndicator();
        this.reloadList(null, event);


    }
    applyFilters(): void {
        //truyền params lên url thông qua router
        this.reloadList(this.maTaiSan, null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }
    truncateString(text): string {
        return abp.utils.truncateStringWithPostfix(text, 32, '...');
    }
    reloadList(maTaiSan, event?: LazyLoadEvent) {
        this._taisanService.getTaiSanByFilter(maTaiSan, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            // alert("aaa")
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
            console.log("ahihi", result.items);
        });
    }
    close(): void {
        console.log("ahihi", this.item);
        this.modal.hide();
        this.dlTraVe.emit(this.item);
    }
}