import { ChangeDetectorRef, Component, EventEmitter, Injector, OnInit, Output, ViewChild } from '@angular/core';

import { AppComponentBase } from '@shared/common/app-component-base';
import { OrganizationUnitServiceProxy, OrganizationUnitAssetListDto } from '@shared/service-proxies/service-proxies';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { IAssetWithOrganizationUnit } from './asset-with-organization-unit';
import { IAssetsWithOrganizationUnit } from './assets-with-organization-unit';
import { IBasicOrganizationUnitInfo } from '../basic-organization-unit-info';


@Component({
    selector: 'organization-unit-assets',
    templateUrl: './organization-unit-assets.component.html'
})
export class OrganizationUnitAssetsComponent extends AppComponentBase implements OnInit {

    @Output() assetRemoved = new EventEmitter<IAssetWithOrganizationUnit>();
    @Output() assetsAdded = new EventEmitter<IAssetsWithOrganizationUnit>();

    // @ViewChild('placeAssetModal') placeAssetModal: PlaceAssetModalComponent;
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('paginator') paginator: Paginator;

    private _organizationUnit: IBasicOrganizationUnitInfo = null;

    constructor(
        injector: Injector,
        private _changeDetector: ChangeDetectorRef,
        private _organizationUnitService: OrganizationUnitServiceProxy
    ) {
        super(injector);
    }

    get organizationUnit(): IBasicOrganizationUnitInfo {
        return this._organizationUnit;
    }

    set organizationUnit(ou: IBasicOrganizationUnitInfo) {
        if (this._organizationUnit === ou) {
            return;
        }

        this._organizationUnit = ou;
        // this.addAssetModal.organizationUnitId = ou.id;
        if (ou) {
            this.refreshAssets();
        }
    }

    ngOnInit(): void {

    }

    getOrganizationUnitAssets(event?: LazyLoadEvent) {
        if (!this._organizationUnit) {
            return;
        }

        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);

            return;
        }

        this.primengTableHelper.showLoadingIndicator();
        this._organizationUnitService.getAssets(
            this._organizationUnit.id,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getMaxResultCount(this.paginator, event),
            this.primengTableHelper.getSkipCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    refreshAssets(): void {
        this.reloadPage();
    }

    openAddModal(): void {
        // this.addAssetModal.show();
    }

    // removeAsset(asset: OrganizationUnitAssetListDto): void {
    //     this.message.confirm(
    //         this.l('RemoveAssetFromOuWarningMessage', asset.code, this.organizationUnit.displayName),
    //         this.l('AreYouSure'),
    //         isConfirmed => {
    //             if (isConfirmed) {
    //                 this._organizationUnitService
    //                     .removeAssetFromOrganizationUnit(user.id, this.organizationUnit.id)
    //                     .subscribe(() => {
    //                         this.notify.success(this.l('SuccessfullyRemoved'));
    //                         this.assetRemoved.emit({
    //                             userId: user.id,
    //                             ouId: this.organizationUnit.id
    //                         });

    //                         this.refreshAssets();
    //                     });
    //             }
    //         }
    //     );
    // }

    placeAssets(data: any): void {
        this.assetsAdded.emit({
            assetIds: data.assetIds,
            ouId: data.ouId
        });

        this.refreshAssets();
    }
}
