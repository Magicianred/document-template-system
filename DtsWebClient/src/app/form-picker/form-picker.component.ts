import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Template } from '../_models/template'
import { NgForm, FormGroup, FormBuilder } from '@angular/forms';
import { Key } from 'protractor';
import { TemplateContent } from '../_models/templateContent';

@Component({
  selector: 'app-form-picker',
  templateUrl: './form-picker.component.html',
  styleUrls: ['./form-picker.component.css']
})
export class FormPickerComponent implements OnInit {
  baseUrl: string;
  apiClient: HttpClient;
  templates: Template[];
  formBase: object;
  chosenFormId: number;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiClient = http;
    this.baseUrl = baseUrl;

  }
  ngOnInit() {
    this.getTemplates();
  }

  getTemplates() {
    this.apiClient.get<Template[]>("https://localhost:44346/api/templates/").subscribe(result => {
      this.templates = result;
    }, error => console.error(error));
  }

  showVersion(id: number) {
    this.chosenFormId = id;
    let query = `https://localhost:44346/api/templates/form/${id}`;
    this.apiClient.get<object>(query).subscribe(result => {
      this.formBase = result;
    }, error => console.error(error));
  }

  postData(form2: any) {
    let tempHolder = document.getElementById("filledTempHolder");

    if (tempHolder.firstChild) {
      tempHolder.removeChild(tempHolder.firstChild)
    }
    const tempCont = document.createElement("DIV");
    
    let query = `https://localhost:44346/api/templates/form/${this.chosenFormId}`;

    this.apiClient.post<TemplateContent>(query, form2).subscribe(result => {
      let templateContent = result;
      tempCont.innerHTML = templateContent.content;
      tempHolder.appendChild(tempCont);
    }, error => console.error(error));
  }


}
