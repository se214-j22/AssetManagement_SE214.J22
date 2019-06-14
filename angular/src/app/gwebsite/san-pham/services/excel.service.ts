import { Injectable, ViewChild } from '@angular/core';
import { Workbook } from 'exceljs';
import * as fs from 'file-saver';
import * as moment from 'moment/moment.js';

const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
const EXCEL_EXTENSION = '.xlsx';
@Injectable({
  providedIn: 'root'
})
export class ExcelService {
  constructor() { }

  public exportAsExcelFile(json: any[]): void {
    
    
    //Excel Title, Header, Data
    const title = 'Danh sách tài sản';
    const header = ["STT","Mã tài sản", "Tên tài sản", "Ngày tạo", "Ngày cập nhật", "Trạng thái"]

    const data=[];

    if(json!=null){
     for(var i=0;i<json.length;i++){
      data.push([(i+1).toString(),json[i].maSP,json[i].tenSP,json[i].ngayTao,json[i].ngayCapNhat,json[i].trangThai]);
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

  
    let subTitleRow = worksheet.addRow(['Ngày : ' +moment().lang('Vi').format("DD/MM/YYYY, h:mm:ss A")])
    subTitleRow.alignment = { vertical: 'top', horizontal: 'left' };


  
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
    //Merge Cells
    worksheet.mergeCells(`A${footerRow.number}:F${footerRow.number}`);

    //Generate Excel File with given name
    workbook.xlsx.writeBuffer().then((data) => {
      let blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      fs.saveAs(blob, "ds_taisan"+"_"+new Date().getTime()+'.xlsx');
    })

  }
  }
}
