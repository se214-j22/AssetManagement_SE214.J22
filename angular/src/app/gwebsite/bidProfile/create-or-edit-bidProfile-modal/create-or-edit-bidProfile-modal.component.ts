import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { ComboboxItemDto, BidProfileServiceProxy, BidProfileSaveForCreate } from '@shared/service-proxies/service-proxies';
import { BidProfileDto, ApprovalStatusEnum, NewPJDto, BidProfileTypeInfo, BidTypeEnum } from '../dto/bidProfile.dto';
import * as moment from 'moment';
import { IMyDpOptions, IMyDateModel, IMyDate } from 'mydatepicker';

@Component({
    selector: 'createOrEditBidProfileModal',
    templateUrl: './create-or-edit-bidProfile-modal.component.html',
    styleUrls: ['./create-or-edit-bidProfile-modal.component.css']
})
export class CreateOrEditBidProfileModalComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    // @ViewChild('bidProfileCombobox') bidProfileCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    bidProfile: BidProfileDto = new BidProfileDto();
    bidProfiles: ComboboxItemDto[] = [];

    public pjCode = '';
    public pjName = '';
    public pjCreateDate = '';
    public pjActiveDate = '';
    public pjUnitPrice = '';
    public pjCalUnit = '';
    public pjDescription = '';

    public bidProfileTypeId: number;
    public bidCatalogProductId: number;
    public projectId: number;

    public bidProfileTypes = [
        {
            id: 1,
            code: 'F001',
            name: 'Computer Screen'
        },
        {
            id: 2,
            code: 'F002',
            name: 'Computer CPU'
        },
        {
            id: 3,
            code: 'G001',
            name: 'Fridge'
        }
    ];


    public bidTypeEnum = BidTypeEnum;
    public bidType = 1;
    public bidTypes = [
        {
            id: BidTypeEnum.Bidding,
            name: 'Bidding'
        },
        {
            id: BidTypeEnum.AppointContractors,
            name: 'Appoint Contractors'
        }
    ];


    public supplierId: number;
    public suppliers = [
        {
            id: 1,
            code: 'S001',
            name: 'DMX'
        },
        {
            id: 2,
            code: 'S002',
            name: 'FPT'
        },
        {
            id: 3,
            code: 'S001',
            name: 'Fridge'
        }
    ];

    public bidProfileTypeInfoList = [];
    public supplierInfoList = [];

    public projectInfos = [];
    public allProjectFakes = [
        {
            id: 1,
            code: 'Pd01',
            name: 'Product1'
        },
        {
            id: 2,
            code: 'Pd02',
            name: 'Product1'
        },
        {
            id: 3,
            code: 'Pd03',
            name: 'Product1'
        },
        {
            id: 4,
            code: 'Pd04',
            name: 'Product1'
        },
        {
            id: 5,
            code: 'Pd05',
            name: 'Product1'
        },
        {
            id: 6,
            code: 'Pd06',
            name: 'Product1'
        }
    ];

    //api 8.7, get all products có status=1(active hay open)
    public productInfos = [];
    public productFakes = [
        {
            id: 1,
            code: 'Pd01',
            name: 'Product1'
        },
        {
            id: 2,
            code: 'Pd02',
            name: 'Product1'
        },
        {
            id: 3,
            code: 'Pd03',
            name: 'Product1'
        },
        {
            id: 4,
            code: 'Pd04',
            name: 'Product1'
        },
        {
            id: 5,
            code: 'Pd05',
            name: 'Product1'
        },
        {
            id: 6,
            code: 'Pd06',
            name: 'Product1'
        }
    ];

    public isCheckActive = false;
    public statusEnum = ApprovalStatusEnum;
    public newBidProfile: NewPJDto;

    public startDateString = '';
    public endDateString = '';
    public openDateString = '';

    public openDatePickerOptions: IMyDpOptions = {
        selectorWidth: '240px',
        dateFormat: 'dd/mm/yyyy',
        showTodayBtn: true,
        todayBtnTxt: 'Now',
        showClearDateBtn: true,
        alignSelectorRight: true,
        openSelectorOnInputClick: true,
        inline: false,
        editableDateField: false,
        selectionTxtFontSize: '13px',
        height: '37px',
        firstDayOfWeek: 'su',
        sunHighlight: true,
        disableUntil: {
            year: new Date().getFullYear(),
            month: new Date().getMonth() + 1,
            day: new Date().getDate() - 1
        }
    };

    constructor(
        injector: Injector,
        private _apiService: BidProfileServiceProxy
    ) {
        super(injector);
    }

    ngOnInit(): void {
        // this.isPermissionEditCloseActive = true;
        // call hàm này khi subcribe api 8.6 get all project success
        this.handelSelectsProject();

        // call hàm này khi subcribe api 8.7 get all product success
        this.handelSelectsProduct();
    }

    public handelSelectsProduct(): void {
        // filter products
        this.productInfos = [];
        this.productFakes.forEach((item, i) => {
            this.productInfos.push(
                new BidProfileTypeInfo(item.id, `${item.code} - ${item.name}`));
        });
    }
    public onDateChangedByStart(event: IMyDateModel): void {
        const date = Object.assign({}, event);
        this.startDateString = date.jsdate ? moment(date.jsdate).format('YYYY-MM-DDTHH:mm:ss') : '';
    }
    public onDateChangedByEnd(event: IMyDateModel): void {
        const date = Object.assign({}, event);
        this.endDateString = date.jsdate ? moment(date.jsdate).format('YYYY-MM-DDTHH:mm:ss') : '';
    }
    public onDateChangedByOpen(event: IMyDateModel): void {
        const date = Object.assign({}, event);
        this.openDateString = date.jsdate ? moment(date.jsdate).format('YYYY-MM-DDTHH:mm:ss') : '';
    }

    show(bidProfileId?: number | null | undefined): void {
        this.active = true;
        this.saving = false;

        this.pjCode = '';
        this.pjName = '';
        this.isCheckActive = false;
        this.pjUnitPrice = '';
        this.pjCalUnit = '';
        this.pjDescription = '';

        let now = new Date();
        this.pjCreateDate = moment(now).format('DD/MM/YYYY');

        this.bidProfileTypeId = this.bidProfileTypes[0].id;
        this.bidProfileTypeInfoList = [];

        this.bidProfileTypes.forEach((item, i) => {
            this.bidProfileTypeInfoList.push(
                new BidProfileTypeInfo(item.id, `${item.code} - ${item.name}`));
        });

        this.supplierId = this.suppliers[0].id;
        this.supplierInfoList = [];

        this.suppliers.forEach((item, i) => {
            this.supplierInfoList.push(
                new BidProfileTypeInfo(item.id, `${item.code} - ${item.name}`));
        });
        this.modal.show();
    }

    public handelSelectsProject(): void {
        // filter products
        this.projectInfos = [];
        this.allProjectFakes.forEach((item, i) => {
            this.projectInfos.push(
                new BidProfileTypeInfo(item.id, `${item.code} - ${item.name}`));
        });
    }

    save(): void {
        if (this.pjCode && this.pjCode !== '' && this.pjName && this.pjName !== '') {
            this.saving = true;

            // let status = this.isCheckActive ? this.statusEnum.Active : this.statusEnum.Inactive;

            //createDate: BE lấy giờ hệ thống
            // this.newBidProfile = new NewPJDto(this.pjCode, this.pjName, this.bidProfileTypeId, this.supplierId, this.pjUnitPrice,
            //     this.pjCalUnit, this.pjDescription, status);


            console.log(this.pjCode + '--' + this.pjName + '--' + this.bidProfileTypeId + '--' + this.supplierId + '--' + this.pjUnitPrice
                + '--' + this.pjCalUnit + '--' + this.pjDescription + '--' + status);
// this._apiService.createBidProfileAsync(new BidProfileSaveForCreate({id:0,projectId:this.projectId,bidType:this.bidTypes[this.bidType].name,bidCatalog:this.bidCatalogProductId}))
            // this.insertBidProfile();

            // call api create bidProfile category theo code,nam,status
            // add xuống, id tự tạo

            //trước khi add nhớ check duplicat code.


            this.close();
        }
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }

    activeNewPrj(event: Event): void {
        if (this.isCheckActive) {
            this.pjActiveDate = this.pjCreateDate;
        }
    }
}
