import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Template } from '../_models/template'

@Component({
  selector: 'app-editors-templates',
  templateUrl: './editors-templates.component.html',
  styleUrls: ['./editors-templates.component.css']
})
export class EditorsTemplatesComponent implements OnInit {
  baseUrl: string;
  apiClient: HttpClient;
  templates: Template[];


  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiClient = http;
    this.baseUrl = baseUrl;
  }
  ngOnInit() {
    this.getTemplates();
  }

  getTemplates() {
    this.apiClient.get<Template[]>("https://localhost:44346/api/templates/editor/2").subscribe(result => {
      console.log(result);
      this.templates = result;
    }, error => console.error(error));
  }


}
