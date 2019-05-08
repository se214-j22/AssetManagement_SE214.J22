import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { ModalDirective } from 'ngx-bootstrap';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { NhaCungCapForViewDto, NhaCungCapServiceProxy } from "@shared/service-proxies/service-proxies";

@Component({
    selector: 'nhacungcap',
    templateUrl: './nhacungcap.component.html',
    animations: [appModuleAnimation()]
})

export class NhaCungCapComponent extends AppComponentBase  {

    nhacungcap : NhaCungCapForViewDto = new NhaCungCapForViewDto();
    maTaiSan: string;
    //@ViewChild('viewModal') modal: ModalDirective;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
 

    constructor(
        injector: Injector,
        private _nhacungcapService: NhaCungCapServiceProxy

    ) {
        super(injector);
    }

    show(Id?: number | null | undefined): void {
        this._nhacungcapService.getNhaCungCapForView(Id).subscribe(result => {
            this.nhacungcap = result;
          //  this.modal.show();
        })
    }

    getNhaCungCaps(event ?: LazyLoadEvent)
    {
        //alert("aaa")
        if(!this.paginator|| !this.dataTable)
        {
            return ;
        }
        this.primengTableHelper.showLoadingIndicator();
        this.reloadList(null,event);
        
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
        this._nhacungcapService.getNhaCungCapsByFilter(maTaiSan, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
           // alert("aaa")
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
            console.log("Vô không: "+result.totalCount);
        });
    }

    close() : void{
      //  this.modal.hide();
    }
}