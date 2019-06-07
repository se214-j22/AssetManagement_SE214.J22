import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { ComboboxItemDto } from '@shared/service-proxies/service-proxies';
import { BidProfileDto, ApprovalStatusEnum, NewPJDto, BidProfileTypeInfo } from '../dto/bidProfile.dto';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditBidProfileModal',
    templateUrl: './create-or-edit-bidProfile-modal.component.html',
    styleUrls: ['./create-or-edit-bidProfile-modal.component.css']
})
export class CreateOrEditBidProfileModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('bidProfileCombobox') bidProfileCombobox: ElementRef;
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

    public isCheckActive = false;
    public statusEnum = ApprovalStatusEnum;
    public newBidProfile: NewPJDto;

    constructor(
        injector: Injector,
        private _apiService: WebApiServiceProxy
    ) {
        super(injector);
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

        this._apiService.getForEdit('api/MenuClient/GetMenuClientForEdit', bidProfileId).subscribe(result => {
            this.bidProfile = result.menuClient;
            this.bidProfiles = result.menuClients;
            this.modal.show();
            setTimeout(() => {
                $(this.bidProfileCombobox.nativeElement).selectpicker('refresh');
            }, 0);
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

            // this.insertBidProfile();

            // call api create bidProfile category theo code,nam,status
            // add xuống, id tự tạo

            //trước khi add nhớ check duplicat code.


            this.close();
        }
    }

    insertBidProfile() {
        // tiennnnnnnnnnnnnnnnnnnnnnnnnnnnn
        this._apiService.post('api/MenuClient/CreateMenuClient', this.bidProfile)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
    }

    updateBidProfile() {
        // tiennnnnnnnnnnnnnnnnnnnnnnnnnnnn
        this._apiService.put('api/MenuClient/UpdateMenuClient', this.bidProfile)
            .pipe(finalize(() => this.saving = false))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
            });
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
