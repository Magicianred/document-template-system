import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Template } from '../_models/template'
import { NgForm, FormGroup, FormBuilder } from '@angular/forms';
import { Key } from 'protractor';

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
    let query = `https://localhost:44346/api/templates/form/${id}`;
    this.apiClient.get<object>(query).subscribe(result => {
      this.formBase = result;
    }, error => console.error(error));
  }

  postData(form2 : any) {
    console.log(form2);
    
  }


}
