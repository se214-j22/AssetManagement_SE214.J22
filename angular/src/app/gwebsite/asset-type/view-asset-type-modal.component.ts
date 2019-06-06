import { Component, OnInit, Input, ViewChild, Injector } from '@angular/core';
import { AssetDto, AssetServiceProxy, AssetTypeDto, AssetTypeServiceProxy } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'viewAssetTypeModal',
  templateUrl: './view-asset-type-modal.component.html'
})
export class ViewAssetTypeModalComponent extends AppComponentBase {
  @Input() assetType: AssetTypeDto = new AssetTypeDto();
  @ViewChild('viewModal') modal: ModalDirective;

  constructor(
      injector: Injector,
      private _assetTypeService: AssetTypeServiceProxy
  ) {
      super(injector);
  }

  show(assetId?: number | null | undefined): void {
    this._assetTypeService.getById(assetId).subscribe(result => {
        this.assetType = result;
        this.modal.show();
    })
}

close() : void{
    this.modal.hide();
}

  onPrint() {
    window.print();
  }

}
