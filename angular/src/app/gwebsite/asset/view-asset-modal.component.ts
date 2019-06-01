import { Component, OnInit, Input } from '@angular/core';
import { AssetDto } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-view-asset-modal',
  templateUrl: './view-asset-modal.component.html',
  // styleUrls: ['./view-asset-modal.component.css']
})
export class ViewAssetModalComponent implements OnInit {
  @Input() asset: AssetDto;
  constructor() { }

  ngOnInit() {
  }

  onPrint() {
    window.print();
  }

}
