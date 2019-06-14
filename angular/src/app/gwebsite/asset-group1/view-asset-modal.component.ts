import { Component, OnInit, Input, ViewChild, Injector } from '@angular/core';
import { AssetDto, AssetServiceProxy, OrganizationUnitServiceProxy, ListResultDtoOfOrganizationUnitDto } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewAssetModalGroup1',
    templateUrl: './view-asset-modal.component.html',
    // styleUrls: ['./view-asset-modal.component.css']
})
export class ViewAssetModalComponentGroup1 extends AppComponentBase implements OnInit {
    @Input() asset: AssetDto = new AssetDto();
    @ViewChild('viewModal') modal: ModalDirective;
    OU: ListResultDtoOfOrganizationUnitDto;

    constructor(
        injector: Injector,
        private _assetService: AssetServiceProxy,
        private _organizationUnitService: OrganizationUnitServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this._organizationUnitService.getOrganizationUnits().subscribe(ou =>
            this.OU = ou);
    }

    show(assetId?: number | null | undefined): void {
        this._assetService.getById(assetId).subscribe(result => {
            this.asset = result;
            this.modal.show();
        })
    }

    close(): void {
        this.modal.hide();
    }

    onPrint() {
        window.print();
    }

}
