import { Component, OnInit, Inject, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TemplateVersions } from '../_models/templateVersion'
import { document } from 'ngx-bootstrap';


@Component({
  selector: 'app-template-data',
  templateUrl: './template-data.component.html',
  styleUrls: ['./template-data.component.css']
})
export class TemplateDataComponent implements OnInit {
  baseUrl: string;
  apiClient: HttpClient;
  template: TemplateVersions;
  templateChosen: boolean;
  version: string;



  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiClient = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
    this.getTemplate();
  }

  showVersion(id: number) {
    let tempHolder = document.getElementById("tempHolder");

    if (tempHolder.firstChild) {
      tempHolder.removeChild(tempHolder.firstChild)
    }
    const tempCont = document.createElement("DIV");
    tempCont.innerHTML = this.template.versions[id].templateVersion;

    tempHolder.appendChild(tempCont);
  }

  getTemplate() {
    this.apiClient.get<TemplateVersions>("https://localhost:44346/api/templates/1").subscribe(result => {
      console.log(result);
      this.template = result;
    }, error => console.error(error));
  }

}
