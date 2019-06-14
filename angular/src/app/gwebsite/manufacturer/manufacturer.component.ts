
import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { WebApiServiceProxy, IFilter } from '@shared/service-proxies/webapi.service';
import { DemoModelServiceProxy, ManufacturerServiceProxy, ManufacturerDto } from '@shared/service-proxies/service-proxies';
import { ViewManufacturerModalComponent } from './view-manufacturer-modal.component';
import { CreateOrEditManufacturerModalComponent } from './create-or-edit-manufacturer-modal.component';


@Component({
    templateUrl: './manufacturer.component.html',
    animations: [appModuleAnimation()]
})
export class ManufacturerComponent extends AppComponentBase implements AfterViewInit {
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditManufacturerModalComponent;
    @ViewChild('viewModal') viewModal: ViewManufacturerModalComponent;
    filterTerm: string;

    constructor(
        injector: Injector,
        private _manufacturerService: ManufacturerServiceProxy,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _apiService: WebApiServiceProxy
    ) {
        super(injector);
    }

    ngAfterViewInit(): void {
        setTimeout(() => {
            this.init();
        });
    }
    init(): void {
        this._activatedRoute.params.subscribe((params: Params) => {
            this.filterTerm = params['term'] || '';
            this.reloadList(this.filterTerm, null);
        });
    }
    getManufacturers(event?: LazyLoadEvent) {
        if (!this.paginator || !this.dataTable) {
            return;
        }

        //show loading trong gridview
        this.primengTableHelper.showLoadingIndicator();

        /**
         * mặc định ban đầu lấy hết dữ liệu nên dữ liệu filter = null
         */

        this.reloadList(null, event);

    }


    reloadList(filterTerm, event?: LazyLoadEvent) {
        this._manufacturerService.getByFilter(filterTerm, this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event),
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }
    createManufacturer() {
        this.createOrEditModal.show();
    }

    deleteManufacturer(id): void {
        this._manufacturerService.delete(id).subscribe(() => {
            this.reloadPage();
        })
    }


    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    applyFilters(): void {
        this.reloadList(this.filterTerm, null);

        if (this.paginator.getPage() !== 0) {
            this.paginator.changePage(0);
            return;
        }
    }

    /**
    * @param text
    */
    truncateString(text): string {
        return abp.utils.truncateStringWithPostfix(text, 32, '...');
    }
}