import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit, Optional, Inject } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { NhomTaiSanServiceProxy, NhomTaiSanInput, API_BASE_URL } from '@shared/service-proxies/service-proxies';
import { HttpClient } from '@angular/common/http';
import { SearchTaiSanComponent } from './search-taisan.component';
import { Paginator } from 'primeng/components/paginator/paginator';


@Component({
    selector: 'createOrEditNhomTaiSanModal',
    templateUrl: './create-or-edit-nhomtaisan-modal.component.html'
})
export class CreateOrEditNhomTaiSanModalComponent extends AppComponentBase implements OnInit {

    ngOnInit() {
        //this.getListId();
    }

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('nhomTaiSanCombobox') nhomTaiSanCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;
    @ViewChild('createOrEditSearchModal') searchTaiSanModel: SearchTaiSanComponent;
    @ViewChild('paginator') paginator: Paginator;


    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    nhomTaiSan: NhomTaiSanInput = new NhomTaiSanInput();
    private baseUrl: string;
    listId: string[];

    constructor(
        injector: Injector,
        private _nhomTaiSanService: NhomTaiSanServiceProxy,
        private httpService: HttpClient,
        @Optional() @Inject(API_BASE_URL) baseUrl?: string
    ) {
        super(injector);
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    show(nhomTaiSanId?: number | null | undefined): void {
        this.saving = false;


        this._nhomTaiSanService.getNhomTaiSanForEdit(nhomTaiSanId).subscribe(result => {
            this.nhomTaiSan = result;
            this.modal.show();

        })
    }

    // 30-5 lấy id
    // getListId(): void {
    //     //this.listId = "ohh";
    //     //this.httpService.get(this.baseUrl + "/api/NhomTaiSan/GetIdNhomTaiSan?"/*, {responseType: 'text'}*/).subscribe(
    //     //    result => {
    //     //        this.listId = result.toString();
    //     //})

    //     this._nhomTaiSanService.getListId().subscribe(
    //         result => {
    //             this.listId = result['result'];
    //         })
    // }

    // 3-6 searchTaiSan
    searchTaiSan() {
        this.searchTaiSanModel.show();
    }

    reloadPage(): void {
        //this.paginator.changePage(this.paginator.getPage());
    }

    save(): void {
        let input = this.nhomTaiSan;
        this.saving = true;
        this._nhomTaiSanService.createOrEditNhomTaiSan(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
