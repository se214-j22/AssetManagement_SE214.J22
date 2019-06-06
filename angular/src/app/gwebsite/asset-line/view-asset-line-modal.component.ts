import { Component, OnInit, Input, ViewChild, Injector } from '@angular/core';
import { AssetDto, AssetServiceProxy, AssetLineDto, AssetLineServiceProxy } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'viewAssetLineModal',
  templateUrl: './view-asset-line-modal.component.html'
})
export class ViewAssetLineModalComponent extends AppComponentBase {
  @Input() assetLine: AssetLineDto = new AssetLineDto();
  @ViewChild('viewModal') modal: ModalDirective;

  constructor(
      injector: Injector,
      private _assetLineService: AssetLineServiceProxy
  ) {
      super(injector);
  }

  show(assetId?: number | null | undefined): void {
    this._assetLineService.getById(assetId).subscribe(result => {
        this.assetLine = result;
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
