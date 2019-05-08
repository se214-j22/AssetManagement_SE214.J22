
import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild, Output, EventEmitter } from "@angular/core";
import { ModalDirective } from 'ngx-bootstrap';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { ModelForViewDto, ModelServiceProxy, ModelDto } from "@shared/service-proxies/service-proxies";

@Component({
    selector: 'model',
    templateUrl: './model.component.html',
    animations: [appModuleAnimation()]
})

export class ModelComponent extends AppComponentBase {

    model: ModelForViewDto = new ModelForViewDto();
    maModel: string;
    //@ViewChild('viewModal') modal: ModalDirective;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('viewModal') modal: ModalDirective;
    @Output() dlTraVe: EventEmitter<ModelDto> = new EventEmitter<ModelDto>();
    item: ModelDto = new ModelDto();


    constructor(
        injector: Injector,
        private _modelService: ModelServiceProxy

    ) {
        super(injector);
    }

    show(): void {

        this.modal.show();
    }

    getModels(event?: LazyLoadEvent) {
        //alert("aaa")
        if (!this.paginator || !this.dataTable) {
            return;
        }
        this.primengTableHelper.showLoadingIndicator();
        this.reloadList(null, event);

    }
    applyFilters(): void {
        //truyền params lên url thông qua router
        this.reloadList(this.maModel, null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }
    truncateString(text): string {
        return abp.utils.truncateStringWithPostfix(text, 32, '...');
    }
    reloadList(maTaiSan, event?: LazyLoadEvent) {
        this._modelService.getModelByFilter(maTaiSan, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            // alert("aaa")
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();

        });
    }

    close(): void {
        this.modal.hide();
        this.dlTraVe.emit(this.item);

    }

}