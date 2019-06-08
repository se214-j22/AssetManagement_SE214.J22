import { AfterViewInit, Component, ElementRef, Injector, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as _ from 'lodash';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { Paginator } from 'primeng/components/paginator/paginator';
import { Table } from 'primeng/components/table/table';
import { ModelForViewDto, ThongTinBaoHiemServiceProxy, QuanLyVanHanhServiceProxy, QuanLyVanHanhDto, ThongTinBaoHiemDto, PhiDuongBoDTO, PhiDuongBoServiceProxy, ThongTinSuaChuaDTO, ThongTinSuaChuaServiceProxy, ThongTinBaoDuongDto, ThongTinBaoDuongServiceProxy, ThongTinDangKiemDto, ThongTinDangKiemInput, ThongTinDangKiemServiceProxy, ThietBiKemTheoServiceProxy, ThietBiKemTheoDto } from '@shared/service-proxies/service-proxies';
import { ThongTinXeModalComponent } from '../thongtinxe/thongtinxe-modal.component';
import { ThongTinXeViewDTO } from '../thongtinxe/dto/ThongTinXeViewDTO';
import { ViewVanHanhXeModalComponent } from '../vanhanhxe/view-vanhanhxe-modal.component';



@Component({
    selector: 'chitietxeComponent',
    templateUrl: './chitietxe.component.html',
    styleUrls: ['./chitietxe.css'],
    animations: [appModuleAnimation()]
})
export class ChiTietXeComponent extends AppComponentBase implements AfterViewInit, OnInit {

    /**
     * @ViewChild là dùng get control và call thuộc tính, functions của control đó
     */
    @ViewChild('dataTable') dataTable: Table;
    @ViewChild('dataTable1') dataTable1: Table;
    @ViewChild('paginator') paginator: Paginator;
    @ViewChild('paginatorVanHanhXe') paginatorVanHanhXe: Paginator;
    @ViewChild('viewThongTinXe') viewThongTinXe: ThongTinXeModalComponent;
    @ViewChild('viewVanHanhXeModal') viewVanHanhXeModal: ViewVanHanhXeModalComponent;


    soXe: string;
    model: ModelForViewDto = new ModelForViewDto();
    thongtinxeDto: ThongTinXeViewDTO = new ThongTinXeViewDTO();
    vanhanhxes: QuanLyVanHanhDto[] = [];
    baohiemxes: ThongTinBaoHiemDto[] = [];
    phiduongbos: PhiDuongBoDTO[] = [];
    suachuaxes: ThongTinSuaChuaDTO[] = [];
    baoduongxes: ThongTinBaoDuongDto[] = [];
    dangkiemxes: ThongTinDangKiemDto[] = [];
    tbkts: ThietBiKemTheoDto[] = [];


    constructor(
        injector: Injector,
        private _baohiemxeService: ThongTinBaoHiemServiceProxy,
        private _vanhanhxeService: QuanLyVanHanhServiceProxy,
        private _phiduongboService: PhiDuongBoServiceProxy,
        private _suachuaxeService: ThongTinSuaChuaServiceProxy,
        private _baoduongxeService: ThongTinBaoDuongServiceProxy,
        private _thongtindangkiemService: ThongTinDangKiemServiceProxy,
        private _tbktService: ThietBiKemTheoServiceProxy

    ) {
        super(injector);

    }

    /**
     * Hàm xử lý trước khi View được init
     */
    ngOnInit(): void {
    }

    /**
     * Hàm xử lý sau khi View được init
     */
    ngAfterViewInit(): void {
        setTimeout(() => {


        });
    }

    getThietBiKemTheo() {

        this.soXe = this.thongtinxeDto.soXe;
        if (this.soXe != undefined) {
            this._tbktService.getThietBiKemTheosByFilter(this.soXe, undefined, undefined, undefined).subscribe(kq3 => {
                this.tbkts = kq3.items;
                console.log("kq3", kq3.items);
            })

        }

    }

    getThongTinBaoHiems() {
        this.soXe = this.thongtinxeDto.soXe;
        if (this.soXe != undefined) {
            this._baohiemxeService.getThongTinBaoHiemsByFilter(this.soXe, undefined, undefined, undefined
            ).subscribe(result => {
                this.baohiemxes = result.items;
            });
        }
    }

    getThongTinPhiDuongBos() {
        this.soXe = this.thongtinxeDto.soXe;
        if (this.soXe != undefined) {
            this._phiduongboService.getPhiDuongBosByFilter(this.soXe, undefined, undefined, undefined, undefined, undefined, undefined
            ).subscribe(result => {
                this.phiduongbos = result.items;
            });
        }
    }
    getThongTinVanHanhs() {
        this.soXe = this.thongtinxeDto.soXe;
        if (this.soXe != undefined) {
            this._vanhanhxeService.getQuanLyVanHanhsByFilter(this.soXe, undefined, undefined,
                undefined,
            ).subscribe(result => {
                this.vanhanhxes = result.items;
            });
        }
    }
    getThongTnSuaChuaXes() {
        this.soXe = this.thongtinxeDto.soXe;
        if (this.soXe != undefined) {
            this._suachuaxeService.getThongTinSuaChuasByFilter(this.soXe, undefined, undefined, undefined, undefined, undefined, undefined,
            ).subscribe(result => {
                this.suachuaxes = result.items;
            });
        }
    }
    getThongTinBaoDuongXes() {
        this.soXe = this.thongtinxeDto.soXe;
        if (this.soXe != undefined) {
            this._baoduongxeService.getThongTinBaoDuongsByFilter(this.soXe, undefined, undefined,
                undefined,
            ).subscribe(result => {
                this.baoduongxes = result.items;
            });
        }
    }
    getThongTinDangKiemXes() {
        this.soXe = this.thongtinxeDto.soXe;
        if (this.soXe != undefined) {
            this._thongtindangkiemService.getThongTinDangKiemsByFilter(this.soXe, undefined, undefined,
                undefined,
            ).subscribe(result => {
                this.dangkiemxes = result.items;
            });
        }
    }





    getThongTinXe(item: ThongTinXeViewDTO) {
        this.thongtinxeDto = item;
        this.soXe = item.soXe;
        this.getThietBiKemTheo();
        this.getThongTinVanHanhs();
        this.getThongTinBaoHiems();
        this.getThongTinPhiDuongBos();
        this.getThongTnSuaChuaXes();
        this.getThongTinDangKiemXes();
        this.getThongTinBaoDuongXes();


    }


    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }



}
