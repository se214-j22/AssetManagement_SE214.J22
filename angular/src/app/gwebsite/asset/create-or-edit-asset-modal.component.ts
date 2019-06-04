import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild, OnInit, OnChanges } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { CustomerServiceProxy, CustomerInput, AssetInput, AssetServiceProxy, AssetLine, AssetLineServiceProxy, AssetLineDto, ComboboxItemDto } from '@shared/service-proxies/service-proxies';


@Component({
    selector: 'createOrEditAssetModal',
    templateUrl: './create-or-edit-asset-modal.component.html'
})
export class CreateOrEditAssetModalComponent extends AppComponentBase implements OnInit {


    @ViewChild('createOrEditModal') modal: ModalDirective;
    @ViewChild('statusSelect') statusSelect;
    @ViewChild('assetLineCombobox') assetLineCombobox: ElementRef;
    @ViewChild('dateInput') dateInput: ElementRef;
    assetLineComboboxs: ComboboxItemDto[] = [];
    statusComboboxs: ComboboxItemDto[] = [
        new ComboboxItemDto({ value: false.toString(), isSelected: false, displayText: 'Normal' }),
        new ComboboxItemDto({ value: true.toString(), isSelected: true, displayText: 'Is Damaged' })];
    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    saving = false;

    asset: AssetInput = new AssetInput();
    assetLines: AssetLineDto[] = new Array<AssetLineDto>();
    beingCreated: boolean;
    constructor(
        injector: Injector,
        private _assetService: AssetServiceProxy,
        private _assetLineService: AssetLineServiceProxy
    ) {
        super(injector);
    }
    ngOnInit() {
        this._assetLineService.getByFilter(undefined, undefined, undefined, undefined, 999, undefined).subscribe(result => {
            if (result) {
                this.assetLines = result.items;
                this.assetLineComboboxs = result.items.map(al => {
                    return new ComboboxItemDto({ value: al.id.toString(), displayText: al.name, isSelected: false })
                })
            }
        });
    }

    show(assetId?: number | null | undefined): void {
        this.saving = false;

        this._assetService.getForEdit(assetId).subscribe(result2 => {
            if (result2) {
                this.asset = result2;
                if (!this.asset.assetLineID) 
                {
                    this.asset.isDamaged = false;
                    this.beingCreated = true;
                    this.asset.number= 1;
                }
                else
                {
                    this.beingCreated = false;
                }

                this.modal.show();
                setTimeout(() => {
                    $(this.assetLineCombobox.nativeElement).selectpicker('refresh');
                }, 0);
            }
        })
    }

    save(): void {
        let input = this.asset;
        this.saving = true;
        this._assetService.createOrEdit(input).subscribe(result => {
            this.notify.info(this.l('SavedSuccessfully'));
            this.close();
        })

    }

    close(): void {
        this.modal.hide();
        this.modalSave.emit(null);
    }
}
