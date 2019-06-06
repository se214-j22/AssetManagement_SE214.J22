import { Component, ElementRef, EventEmitter, Injector, Output, ViewChild } from '@angular/core';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { WebApiServiceProxy } from '@shared/service-proxies/webapi.service';
import { ComboboxItemDto, ProjectServiceProxy, ProjectSavedDto } from '@shared/service-proxies/service-proxies';
import { ProjectDto, ApprovalStatusEnum, NewPJDto } from '../dto/project.dto';
import * as moment from 'moment';

@Component({
    selector: 'createOrEditProjectModal',
    templateUrl: './create-or-edit-project-modal.component.html',
    styleUrls: ['./create-or-edit-project-modal.component.css']
})
export class CreateOrEditProjectModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal') modal: ModalDirective;
    // @ViewChild('projectCombobox') projectCombobox: ElementRef;
    @ViewChild('iconCombobox') iconCombobox: ElementRef;

    /**
     * @Output dùng để public event cho component khác xử lý
     */
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    checked: boolean;
    project: ProjectDto = new ProjectDto();
    projects: ComboboxItemDto[] = [];

    public pjCode = '';
    public pjName = '';
    public pjCreateDate = '';
    public pjActiveDate = '';
    public isCheckActive = false;
    public statusEnum = ApprovalStatusEnum;
    public newProject: NewPJDto;

    constructor(
        injector: Injector,
        private _apiService: ProjectServiceProxy
    ) {
        super(injector);
    }

    show(projectId?: number | null | undefined): void {
        this.active = true;
        this.saving = false;

        this.pjCode = '';
        this.pjName = '';
        this.isCheckActive = false;

        let now = new Date();
        this.pjCreateDate = moment(now).format('DD/MM/YYYY');

        this.modal.show();
    }

    save(): void {
        if (this.pjCode && this.pjCode !== '' && this.pjName && this.pjName !== '') {
            this.saving = true;

            let status = this.isCheckActive ? this.statusEnum.Active : this.statusEnum.Inactive;

            this.newProject = new NewPJDto(this.pjCode, this.pjName, status);

            console.log(this.newProject.code + '--' + this.newProject.name
                + '--' + this.newProject.status);

            // this.insertProject();

            // call api create product category theo code,nam,status
            // add xuống, id tự tạo

            //trước khi add nhớ check duplicat code.
            this._apiService.createProjectAsync(new ProjectSavedDto({ code: this.pjCode, name: this.pjName, status: this.checked ? 1 : 2 })).subscribe(item => console.log(item));

            this.close();
        }
    }


    close(): void {
        this.active = false;
        this.modal.hide();
    }

    handleChange() {
        this.isCheckActive = true;
        this.pjActiveDate = this.pjCreateDate;
    }
    activeNewPrj(event: Event): void {
        if (this.isCheckActive) {
            this.pjActiveDate = this.pjCreateDate;
        }
    }
}
