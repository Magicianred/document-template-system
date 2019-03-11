import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TemplateVersions } from '../_models/templateVersion'


@Component({
  selector: 'app-template-data',
  templateUrl: './template-data.component.html',
  styleUrls: ['./template-data.component.css']
})
export class TemplateDataComponent implements OnInit {
  baseUrl: string;
  apiClient: HttpClient;
  template: TemplateVersions;



  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiClient = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.getTemplate();
  }

  getTemplate() {
    this.apiClient.get<TemplateVersions>("https://localhost:44346/api/templates/1").subscribe(result => {
      console.log(result);
      this.template = result;
    }, error => console.error(error));
  }

}
