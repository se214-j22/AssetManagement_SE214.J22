import {AfterViewInit, Component, ElementRef, ViewChild, OnInit, Injector} from "@angular/core";
import { ModalDirective } from "ngx-bootstrap";


@Component({
  selector: 'generateQRModal',
  templateUrl: './generate-qr.component.html',
  styleUrls: ['./generate-qr.component.css']
})
export class GenerateQrComponent implements AfterViewInit {

  @ViewChild('generateQRModal') modal: ModalDirective;

  text = "https://github.com/werthdavid/ngx-kjua";
  render = "image";
  crisp = true;
  minVersion = 1;
  ecLevel = "H";
  size = 400;
  ratio = undefined;
  fill = "#333";
  back = "#fff";
  rounded = 0;
  quiet = 1;
  mode = "label";
  mSize = 30;
  mPosX = 50;
  mPosY = 50;
  label = "";
  fontname = "sans-serif";
  fontcolor = "#ff9818";
  image = undefined;
  stringURL=".png";
  codeURL="";
  IsmodelShow: boolean;

  @ViewChild("imgBuffer")
  imageElement: ElementRef;

  ngAfterViewInit(): void {
    setTimeout(() => this.image = this.imageElement.nativeElement, 500);
  }

  /**
   * Not perfect, I know
   * @param event
   */
  // getFiles(event) {
  //   if (event.target.files.length > 0) {
  //     const reader = new FileReader();
  //     reader.readAsDataURL(event.target.files[0]); // read file as data url
  //     reader.onload = (event2: any) => { // called once readAsDataURL is completed
  //       this.imageElement.nativeElement.src = event2.target.result;
  //       this.image = this.imageElement.nativeElement;
  //     }
  //   }
  // }
  
  downloadMyFile(){
    const link = document.createElement('a');
    link.setAttribute('target', '_blank');
    link.setAttribute('href',this.image.src);
    link.setAttribute('download', this.text+this.stringURL);
    document.body.appendChild(link);
    link.click();
    link.remove();
}
  close(): void {
    this.IsmodelShow=true;
    this.modal.hide();
  }
  transmissionData(maSP?: string | null | undefined): void{
        this.text="https://"+maSP+".com";
  }
}
