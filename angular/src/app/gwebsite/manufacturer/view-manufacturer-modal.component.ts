import { Component, OnInit, Input, ViewChild, Injector } from '@angular/core';
import { AssetDto, AssetServiceProxy, ManufacturerDto, ManufacturerServiceProxy } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'viewManufacturerModal',
  templateUrl: './view-manufacturer-modal.component.html'
})
export class ViewManufacturerModalComponent extends AppComponentBase {
  @Input() manufacturer: ManufacturerDto = new ManufacturerDto();
  @ViewChild('viewModal') modal: ModalDirective;

  constructor(
      injector: Injector,
      private _manufacturerService: ManufacturerServiceProxy
  ) {
      super(injector);
  }

  show(assetId?: number | null | undefined): void {
    this._manufacturerService.getById(assetId).subscribe(result => {
        this.manufacturer = result;
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
