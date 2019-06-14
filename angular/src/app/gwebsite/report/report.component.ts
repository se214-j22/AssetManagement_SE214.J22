import { Component, OnInit, AfterViewInit, Injector, ElementRef, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { SanPhamServiceProxy } from '@shared/service-proxies/service-proxies';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { LazyLoadEvent, Paginator } from 'primeng/primeng';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent extends AppComponentBase implements AfterViewInit, OnInit {
  
    constructor(
        injector: Injector,
        public _sanPhamService: SanPhamServiceProxy,
        private _router: Router,
        private _activatedRoute: ActivatedRoute,
        private _apiService: WebApiServiceProxy,
    ) {
        super(injector);
    }

    public data: any[] = [];
    ngOnInit(): void {
        
    }
    
    getData():any{
        this._sanPhamService.getAllSanPhams().subscribe((result)=>{    
            this.data  =  result;
        });
        return this.data;
    }

    ngAfterViewInit(): void {

    }
}
