import { AppComponentBase } from "@shared/common/app-component-base";
import { AfterViewInit, Injector, Component, ViewChild } from "@angular/core";
import { ModalDirective } from 'ngx-bootstrap';
import { SanPhamForViewDto, SanPhamServiceProxy } from "@shared/service-proxies/service-proxies";

@Component({
    selector: 'viewSanPhamModal',
    templateUrl: './view-san-pham-modal.component.html'
})

export class ViewSanPhamModalComponent extends AppComponentBase {

    sanPham : SanPhamForViewDto = new SanPhamForViewDto();
    @ViewChild('viewModal') modal: ModalDirective;

    constructor(
        injector: Injector,
        private _sanPhamService: SanPhamServiceProxy
    ) {
        super(injector);
    }

    show(sanPhamId?: number | null | undefined): void {
        this._sanPhamService.getSanPhamForView(sanPhamId).subscribe(result => {
            this.sanPham = result;
            this.modal.show();
        })
    }

    close() : void{
        this.modal.hide();
    }
}