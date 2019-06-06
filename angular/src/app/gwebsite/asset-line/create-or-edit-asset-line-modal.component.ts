import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit, OnChanges } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CustomerServiceProxy, CustomerInput, AssetInput, AssetServiceProxy, AssetLine, AssetLineServiceProxy, AssetLineDto, ComboboxItemDto, AssetLineInput, AssetTypeDto, Manufacturer, ManufacturerDto, ManufacturerServiceProxy, AssetTypeServiceProxy } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditAssetLineModal',
    templateUrl: './create-or-edit-asset-line-modal.component.html'
})
export class CreateOrEditAssetLineModalComponent extends AppComponentBase implements OnInit {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('statusSelect') statusSelect;
    @ViewChild('assetTypeCombobox') assetTypeCombobox: ElementRef;
    @ViewChild('manufacturerCombobox') manufacturerCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;
    assetTypeComboboxs: ComboboxItemDto[] = [];
    manufacturerComboboxs: ComboboxItemDto[] = [
        new ComboboxItemDto({ value: false.toString(), isSelected: false, displayText: 'Normal' }),
        new ComboboxItemDto({ value: true.toString(), isSelected: true, displayText: 'Is Damaged' })];
    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    assetLine: AssetLineInput = new AssetLineInput();
    assetTypes: AssetTypeDto[] = new Array<AssetTypeDto>();
    manufacturers: ManufacturerDto[] = new Array<ManufacturerDto>()
    beingCreated: boolean;
    constructor(
        injector: Injector,
        private _assetLineService: AssetLineServiceProxy,
        private _assetTypeService: AssetTypeServiceProxy,
        private _manufacturerService: ManufacturerServiceProxy,
    ) {
        super(injector);
    }
    ngOnInit() {
        this._assetTypeService.getByFilter(undefined, undefined, 999, undefined).subscribe(result => {
            if (result) {
                this.assetTypes = result.items;
                this.assetTypeComboboxs = result.items.map(al => {
                    return new ComboboxItemDto({ value: al.id.toString(), displayText: al.name, isSelected: false })
                })
            }
        });
        this._manufacturerService.getByFilter(undefined, undefined, 999, undefined).subscribe(result => {
            if (result) {
                this.manufacturers = result.items;
                this.manufacturerComboboxs = result.items.map(al => {
                    return new ComboboxItemDto({ value: al.id.toString(), displayText: al.name, isSelected: false })
                })
            }
        });
    }

    show(assetLineId?: number | null | undefined): void {
        this.saving = false;

        this._assetLineService.getForEdit(assetLineId).subscribe(result2 => {
            if (result2) {
                this.assetLine = result2;
                this.beingCreated = (this.assetLine.id == null);
                this.modal.show();
                setTimeout(() => {
                    $(this.assetTypeCombobox.nativeElement).selectpicker('refresh');
                    $(this.manufacturerCombobox.nativeElement).selectpicker('refresh');
                }, 0);
            }
        })
    }

    save(): void {
        let input = this.assetLine;
        this.saving = true;
        this._assetLineService.createOrEdit(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
