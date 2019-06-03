import { Component, OnInit, Input, ViewChild, Injector } from '@angular/core';
import { AssetDto, AssetServiceProxy } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'viewAssetModal',
  templateUrl: './view-asset-modal.component.html',
  // styleUrls: ['./view-asset-modal.component.css']
})
export class ViewAssetModalComponent extends AppComponentBase {
  @Input() asset: AssetDto = new AssetDto();
  @ViewChild('viewModal') modal: ModalDirective;

  constructor(
      injector: Injector,
      private _assetService: AssetServiceProxy
  ) {
      super(injector);
  }

  show(assetId?: number | null | undefined): void {
    this._assetService.getById(assetId).subscribe(result => {
        this.asset = result;
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
