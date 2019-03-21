import { Component, OnInit, Inject} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Template } from '../_models/template'
import { TemplateContent } from '../_models/templateContent';
import queries from '../../assets/queries.json';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import * as jspdf from 'jspdf';
import html2canvas from 'html2canvas';  
import { document } from 'ngx-bootstrap';
import { Sort } from '@angular/material';

@Component({
  selector: 'app-form-picker',
  templateUrl: './form-picker.component.html',
  styleUrls: ['./form-picker.component.css']
})
export class FormPickerComponent implements OnInit {
  baseUrl: string;
  templates: Template[];
  sortedTemplates: Template[];
  formBase: object;
  chosenFormId: number;
  gotForm: boolean;
  tempElem: HTMLElement;
  headElements = ['Name', 'Creator'];


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
    this.apiClient.get<Template[]>(queries.activeTemplatesPath).subscribe(result => {
      this.templates = result;
      this.sortedTemplates = result;
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

  sortTemplates(sort: Sort) {
    const data = this.templates;

    if (!sort.active || sort.direction === '') {
      this.sortedTemplates = data;
      return;
    }
    this.sortedTemplates = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'Name': return this.compare(a.name, b.name, isAsc);
        case 'Owner': return this.compare(a.owner.surname, b.owner.surname, isAsc);
        default: return 0;
      }
    });
  }

  compare(a: number | string, b: number | string, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  filterTemplates(searchText: string) {
    this.sortedTemplates = this.templates.filter(template =>
      template.name.indexOf(searchText) !== -1
      || template.owner.name.indexOf(searchText) !== -1
      || template.owner.surname.indexOf(searchText) !== -1
      || template.owner.email.indexOf(searchText) !== -1);
  }
}
