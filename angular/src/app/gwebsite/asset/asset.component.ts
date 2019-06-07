
import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { WebApiServiceProxy, IFilter } from '@shared/service-proxies/webapi.service';
import { DemoModelServiceProxy, AssetServiceProxy, AssetDto, OrganizationUnitServiceProxy, OrganizationUnitDto } from '@shared/service-proxies/service-proxies';
import jsQR from "jsqr";
import { ViewAssetModalComponent } from './view-asset-modal.component';
import { CreateOrEditAssetModalComponent } from './create-or-edit-asset-modal.component';

@Component({
  templateUrl: './asset.component.html',
  animations: [appModuleAnimation()]
})
export class AssetComponent extends AppComponentBase implements OnInit, AfterViewInit {
  // @ViewChild('textsTable') textsTable: ElementRef;
  @ViewChild('dataTable') dataTable: Table;
  @ViewChild('paginator') paginator: Paginator;
  @ViewChild('createOrEditModal') createOrEditModal: CreateOrEditAssetModalComponent;
  @ViewChild('viewModal') viewModal: ViewAssetModalComponent;
  filterTerm: string;
  asset: AssetDto;
  assetId: number;
  assetCode: string;
  chooseBy: 'id' | 'detecting';
  mainOU: OrganizationUnitDto;

  constructor(
    injector: Injector,
    private _assetService: AssetServiceProxy,
    private _router: Router,
    private _activatedRoute: ActivatedRoute,
    private _organizationUnitService: OrganizationUnitServiceProxy
  ) {
    super(injector);
  }
  ngOnInit(): void {
    this._organizationUnitService.getOrganizationUnit().subscribe(ou =>
      this.mainOU = ou);
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
  getAssets(event?: LazyLoadEvent) {
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

  createAsset() {
    this.createOrEditModal.show();
  }
  reloadList(filterTerm, event?: LazyLoadEvent) {
    this._assetService.getByFilter(filterTerm, 0, this.primengTableHelper.getSorting(this.dataTable),
      this.primengTableHelper.getMaxResultCount(this.paginator, event),
      this.primengTableHelper.getSkipCount(this.paginator, event),
    ).subscribe(result => {
      this.primengTableHelper.totalRecordsCount = result.totalCount;
      this.primengTableHelper.records = result.items;
      this.primengTableHelper.hideLoadingIndicator();
    });
  }

  deleteAsset(id): void {
    this._assetService.delete(id).subscribe(() => {
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

  // createCustomer() {
  //   this.createOrEditModal.show();
  // }

  /**
  * @param text
  */
  truncateString(text): string {
    return abp.utils.truncateStringWithPostfix(text, 32, '...');
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
