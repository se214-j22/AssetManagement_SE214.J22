import { Component, ViewChild, Injector, Output, EventEmitter, ViewEncapsulation, Input } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { AssetServiceProxy, AssetDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';

@Component({
    selector: 'assetLookupModal',
    // styleUrls: ['./emCampaign-lookup-table-modal.component.less'],
    encapsulation: ViewEncapsulation.None,
    templateUrl: './asset-lookup-modal.component.html'
})
export class AssetLookupModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    filterText = '';
    id = '';
    displayName: string;

    @Input() assetStatusFilter: number;
    @Output() modalSave: EventEmitter<AssetDto> = new EventEmitter<AssetDto>();
    active = false;
    saving = false;

    constructor(
        injector: Injector,
        private assetService: AssetServiceProxy
    ) {
        super(injector);
    }

    show(): void {
        this.active = true;
        this.paginator.rows = 5;
        this.getAll();
        this.modal.show();
    }

    getAll(event?: LazyLoadEvent) {
        if (!this.active) {
            return;
        }

        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this.assetService.getAssetsByFilter(
          this.filterText,
          this.primengTableHelper.getSorting(this.dataTable),
          this.primengTableHelper.getMaxResultCount(this.paginator, event),
          this.primengTableHelper.getSkipCount(this.paginator, event)
        ).subscribe(result => {
            result.items = result.items.filter(asset => {
                return (asset.statusApproved === true && asset.status === this.assetStatusFilter)
            })

            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });

        // this.assetService.getAssetsByFilter(
        //   this.filterText,
        //   '',
        //   0,
        //   5
        // ).subscribe(result => {
        //   this.primengTableHelper.totalRecordsCount = result.totalCount;
        //   this.primengTableHelper.records = result.items;
        //   this.primengTableHelper.hideLoadingIndicator();
        // });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    setAndSave(asset: AssetDto) {
        // this.id = asset.assetId;
        // this.displayName = asset.assetName;
        const assetDto = new AssetDto();
        assetDto.assetId = asset.assetId;
        assetDto.assetId = asset.assetName;

        this.active = false;
        this.modal.hide();
        this.modalSave.emit(asset);
    }

    close(): void {
        this.active = false;
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
