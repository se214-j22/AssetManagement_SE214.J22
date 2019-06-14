import { Injectable, ViewChild } from '@angular/core';
import { Workbook } from 'exceljs';
import * as fs from 'file-saver';
import * as logoFile from './software_engineer.js';
import * as moment from 'moment/moment.js';

@Injectable({
  providedIn: 'root'
})
export class ExcelService {
  constructor(

  ){}


  generateExcel(json:any[]) {
    
    //Excel Title, Header, Data
    const title = 'Báo cáo kiểm kê';
    const header = ["STT","Mã sản phẩm", "Tên sản phẩm", "Ngày tạo", "Ngày cập nhật", "Trạng thái"]

    const data=[];

    var n1=0,n2=0,n3=0,n4=0;
    if(json!=null){
     for(var i=0;i<json.length;i++){
      data.push([(i+1).toString(),json[i].maSP,json[i].tenSP,json[i].ngayTao,json[i].ngayCapNhat,json[i].trangThai]);
    
      if(json[i].ngayCapNhat!=moment().format("DD/MM/YYYY")){
        n1++;
      }
      if(json[i].trangThai=="Bình thường"){
          n2++;
      }else if(json[i].trangThai=="Hư hỏng"){
          n3++;
      }else{
          n4++;
      }   
    }
  }

    //Create workbook and worksheet
    let workbook = new Workbook();
    let worksheet = workbook.addWorksheet('Report Data');


    //Add Row and formatting
    let titleRow = worksheet.addRow([title]);
    titleRow.alignment = { vertical: 'middle', horizontal: 'center' };
    titleRow.font = { name: 'Times New Roman',family: 4, size: 16, underline: 'none', bold: true }
    worksheet.addRow([]);
    worksheet.addRow([]);

    let nameInventory=worksheet.addRow(['Đơn vị được kiểm kê: Khoa Công nghệ phần mềm']);
    nameInventory.font = { name: 'Times New Roman',family: 3, size: 13, underline: 'none', bold: true }
    let inventoryPeriod=worksheet.addRow(['Kì kiểm kê: 1']);
    let subTitleRow = worksheet.addRow(['Ngày : ' +moment().lang('Vi').format("DD/MM/YYYY, h:mm:ss A")])
    subTitleRow.alignment = { vertical: 'top', horizontal: 'left' };

    //Add Image
    let logo = workbook.addImage({
      base64: logoFile.logoBase64,
      extension: 'png',
    });

    worksheet.addImage(logo, 'E1:F3');
    worksheet.mergeCells('A1:D2');


    //Blank Row 
    worksheet.addRow([]);

    //Add Header Row
    let headerRow = worksheet.addRow(header);
    headerRow.font = { name: 'Times New Roman',family: 3, size: 13, underline: 'none', bold: true }
    headerRow.alignment = { vertical: 'middle', horizontal: 'center' };
    
    // Cell Style : Fill and Border
    headerRow.eachCell((cell, number) => {
      cell.fill = {
        type: 'pattern',
        pattern: 'solid',
        fgColor: { argb: 'FFFFFF00' },
        bgColor: { argb: 'FF0000FF' }
      }
      cell.border = { top: { style: 'thin' }, left: { style: 'thin' }, bottom: { style: 'thin' }, right: { style: 'thin' } }
    })
    // worksheet.addRows(data);


    // Add Data and Conditional Formatting
    data.forEach(d => {
      let row = worksheet.addRow(d);
      let qty = row.getCell(5);
      let color = 'FF99FF99';
      if (+qty.value < 500) {
        color = 'FF9999'
      }

      qty.fill = {
        type: 'pattern',
        pattern: 'solid',
        fgColor: { argb: color }
      }
    }

    );

    worksheet.getColumn(1).width = 5;
    worksheet.getColumn(2).width = 15;
    worksheet.getColumn(3).width = 30;
    worksheet.getColumn(4).width = 20;
    worksheet.getColumn(5).width = 20;
    worksheet.getColumn(6).width = 15;

    //Footer Row
    let footerRow = worksheet.addRow(['']);
    footerRow.getCell(1).fill = {
      type: 'pattern',
      pattern: 'solid',
      fgColor: { argb: 'FFCCFFE5' }
    };
    footerRow.getCell(1).border = { top: { style: 'thin' }, left: { style: 'thin' }, bottom: { style: 'thin' }, right: { style: 'thin' } }

    worksheet.addRow([]);
    worksheet.addRow([]);
    let info1=worksheet.addRow(['* Số sản phẩm chưa cập nhật là: '+n1]);
    let info2=worksheet.addRow(['* Số sản phẩm trạng thái "Bình thường" là: '+n2]);
    let info3=worksheet.addRow(['* Số sản phẩm trạng thái "Hư hỏng" là: '+n3]);
    let info4=worksheet.addRow(['* Số sản phẩm trạng thái "Thất lạc" là: '+n4]);

    worksheet.addRow([]);
    let personCreate=worksheet.addRow(['Người tạo']);
    personCreate.getCell(2);
    personCreate.font = { name: 'Times New Roman',family: 3, size: 13, underline: 'none', bold: true }
    //Merge Cells
    worksheet.mergeCells(`A${footerRow.number}:F${footerRow.number}`);

    //Generate Excel File with given name
    workbook.xlsx.writeBuffer().then((data) => {
      let blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      fs.saveAs(blob, "report"+"_"+new Date().getTime()+'.xlsx');
    })

  }
}
