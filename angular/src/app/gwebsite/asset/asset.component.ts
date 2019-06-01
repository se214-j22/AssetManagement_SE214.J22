
import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { WebApiServiceProxy, IFilter } from '@shared/service-proxies/webapi.service';
import { DemoModelServiceProxy, AssetServiceProxy, AssetDto } from '@shared/service-proxies/service-proxies';
import jsQR from "jsqr";

@Component({
  templateUrl: './asset.component.html',
  animations: [appModuleAnimation()]
})
export class AssetComponent extends AppComponentBase
  implements AfterViewInit {
  // @ViewChild('textsTable') textsTable: ElementRef;
  // @ViewChild('dataTable') dataTable: Table;
  // @ViewChild('paginator') paginator: Paginator;
  // @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditAssetModalComponent;
  // @ViewChild('viewModal') viewDemoModelModal: ViewAssetModalComponent;
  filterText: string;
  asset: AssetDto;
  assetId: number;
  assetCode: string;
  chooseBy: 'id' | 'detecting';
  constructor(
    injector: Injector,
    private _assetService: AssetServiceProxy,
    private _router: Router,
    private _activatedRoute: ActivatedRoute,
    private _apiService: WebApiServiceProxy
  ) {
    super(injector);
  }
  choose() {
    this.assetCode = null;
    this.assetId = null;
    this.asset = null;
    if (!this.chooseBy) {
      this.chooseBy = 'id';
      document.getElementById('detectingByCamera').style.display = 'none';
    }
    else {
      if (this.chooseBy == 'id') {
        this.chooseBy = 'detecting';
        // this.startup();
        document.getElementById('detectingByCamera').style.display = 'block';
        setTimeout(() => 
          this.startup()
        )
      } else {
        this.chooseBy = 'id';
        document.getElementById('detectingByCamera').style.display = 'none';
      }

    }
  }
  getAssetByID() {
    this._assetService.getById(this.assetId).subscribe(asset => {
      this.asset = asset;
    });
  }
  getAssetByCode() {
    this._assetService.getByCode(this.assetCode).subscribe(asset => {
      this.asset = asset;
    });
  }
  ngAfterViewInit(): void {
    setTimeout(() => {
      // this.startup();
      // this.init();

    });
  }
  // getDemoModels(event?: LazyLoadEvent) {
  //   if (!this.paginator || !this.dataTable) {
  //       return;
  //   }

  //   //show loading trong gridview
  //   this.primengTableHelper.showLoadingIndicator();

  //   /**
  //    * Sử dụng _apiService để call các api của backend
  //    */

  //   // this._assetService.getDemoModelsByFilter(null, null, this.primengTableHelper.getSorting(this.dataTable),
  //   //     this.primengTableHelper.getMaxResultCount(this.paginator, event),
  //   //     this.primengTableHelper.getSkipCount(this.paginator, event),
  //   // ).subscribe(result => {
  //   //     this.primengTableHelper.totalRecordsCount = result.totalCount;
  //   //     this.primengTableHelper.records = result.items;
  //   //     this.primengTableHelper.hideLoadingIndicator();
  //   // });

  // }

  // deleteDemoModel(id): void {
  //   // this._assetService.deleteDemoModel(id).subscribe(() => {
  //   //     this.reloadPage();
  //   // })
  // }

  // init(): void {
  //   //get params từ url để thực hiện filter
  //   this._activatedRoute.params.subscribe((params: Params) => {
  //       this.filterText = params['filterText'] || '';
  //       //reload lại gridview
  //       this.reloadPage();
  //   });
  // }

  // reloadPage(): void {
  //   this.paginator.changePage(this.paginator.getPage());
  // }

  // applyFilters(): void {
  //   //truyền params lên url thông qua router
  //   this._router.navigate(['app/gwebsite/menu-client', {
  //       filterText: this.filterText
  //   }]);

  //   if (this.paginator.getPage() !== 0) {
  //       this.paginator.changePage(0);
  //       return;
  //   }
  // }

  // //hàm show view create MenuClient
  // createDemoModel() {
  //   // this.createOrEditModal.show();
  // }

  // /**
  // * Tạo pipe thay vì tạo từng hàm truncate như thế này
  // * @param text
  // */
  // truncateString(text): string {
  //   return abp.utils.truncateStringWithPostfix(text, 32, '...');
  // }

  startup() {
    // this.video = document.createElement("video");
    this.video = document.getElementById('video');
    this.canvas = document.getElementById('canvas');
    this.context = this.canvas.getContext('2d');
    // const nav = <any>navigator;
    // nav.getUserMedia = nav.getUserMedia || nav.mozGetUserMedia || nav.webkitGetUserMedia;

    navigator.mediaDevices.getUserMedia({ video: { facingMode: "environment" }, audio: false })
      .then((stream) => {
        this.video.srcObject = stream;
        this.video.setAttribute("playsinline", true); // required to tell iOS safari we don't want fullscreen
        this.video.play();
        requestAnimationFrame(this.decodeQRCode);
      });

  }
  video: any;
  canvas: any;
  context: any;
  decodeQRCode = () => {
    // console.log(this.video.readyState);
    if (this.video.readyState === this.video.HAVE_ENOUGH_DATA) {
      this.canvas.height = this.video.videoHeight;
      this.canvas.width = this.video.videoWidth;
      this.context.drawImage(this.video, 0, 0, this.canvas.width, this.canvas.height);
      var imageData = this.context.getImageData(0, 0, this.canvas.width, this.canvas.height);
      var code = jsQR(imageData.data, imageData.width, imageData.height, {
        inversionAttempts: "dontInvert",
      });
      if (code && code.data) {
        console.log("Found QR code", code.data);
        this.assetCode = code.data;
        this.getAssetByCode();
      }
      // else { console.log("Not found QR code", code); }
      requestAnimationFrame(this.decodeQRCode)
    }
    else
      requestAnimationFrame(this.decodeQRCode);
  }
}
