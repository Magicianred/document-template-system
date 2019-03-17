import { Component, OnInit, Inject} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Template } from '../_models/template'
import { TemplateContent } from '../_models/templateContent';
import queries from '../../assets/queries.json';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as jspdf from 'jspdf';
import html2canvas from 'html2canvas';  
import { document } from 'ngx-bootstrap';

@Component({
  selector: 'app-form-picker',
  templateUrl: './form-picker.component.html',
  styleUrls: ['./form-picker.component.css']
})
export class FormPickerComponent implements OnInit {
  baseUrl: string;
  templates: Template[];
  formBase: object;
  chosenFormId: number;
  gotForm: boolean;
  tempElem: HTMLElement;


  constructor(
    private apiClient: HttpClient,
    private modalService: NgbModal
  ) {
  }

  ngOnInit() {
    this.getTemplates();
    this.gotForm = false;
  }

  getTemplates() {
    this.apiClient.get<Template[]>(queries.templatesPath).subscribe(result => {
      this.templates = result;
    }, error => console.error(error));
  }

  showVersion(id: number) {
    this.chosenFormId = id;
    this.apiClient.get<object>(queries.formPath + id).subscribe(result => {
      this.formBase = result;
      this.gotForm = true;
    }, error => console.error(error));
  }

  postData(formValues: any, content) { 

    this.apiClient.post<TemplateContent>(queries.formPath + this.chosenFormId, formValues).subscribe(result => {
      let templateContent = result;
      this.tempElem = document.createElement("DIV");
      
      this.tempElem.innerHTML = templateContent.content;
      
      this.open(content, this.tempElem)

    }, error => console.error(error));
  }

  open(content, tempCont: HTMLElement) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title', size: 'lg'});
    document.getElementById("version-container").appendChild(tempCont);
  }

  print() {
    var data = document.getElementById('version-container');
    html2canvas(data).then(canvas => {
      // Few necessary setting options  
      var imgWidth = 208;
      var pageHeight = 295;
      var imgHeight = canvas.height * imgWidth / canvas.width;
      var heightLeft = imgHeight;

      const contentDataURL = canvas.toDataURL('image/png')
      let pdf = new jspdf('p', 'mm', 'a4'); // A4 size page of PDF  
      var position = 0;
      pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight)
      pdf.save('MYPdf.pdf');
   }); 
 }

}
