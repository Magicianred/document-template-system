import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Template } from '../_models/template'
import { TemplateDataUpdate } from '../_models/templateDataUpdate';
import { TemplateVersions } from '../_models/templateVersion';


@Component({
  selector: 'app-admin-template-panel',
  templateUrl: './admin-template-panel.component.html',
  styleUrls: ['./admin-template-panel.component.css']
})
export class AdminTemplatePanelComponent implements OnInit {
  apiClient: HttpClient;
  templates: Template[];
  templateChosen: boolean;
  pickedTemplate: TemplateVersions;

  constructor(http: HttpClient) {
    this.apiClient = http;
  }
  ngOnInit() {
    this.getTemplates();
    this.templateChosen = false;
  }

  getTemplates() {
    this.apiClient.get<Template[]>("https://localhost:44346/api/templates/").subscribe(result => {
      console.log(result);
      this.templates = result;
    }, error => console.error(error));
  }

  switchState(id: string) {
    let query = `https://localhost:44346/api/templates/${id + 1}`;

    if (this.templates[id].templateState == "Inactive") {
      let template = this.templates[id];
      let updateData = new TemplateDataUpdate();
      updateData.id = +id;
      updateData.name = template.name;
      updateData.ownerId = 2;
      updateData.stateId = 1;
      this.apiClient.put(query, updateData).subscribe(result => {
        this.getTemplates();
      }, error => console.error(error));
    }
    else {
      this.apiClient.delete(query).subscribe(result => {
        this.getTemplates();
      }, error => console.error(error));
    }
  }

  changeOwner(id: string) {
    let template = this.templates[id];
    let updateData = new TemplateDataUpdate();
    let query = `https://localhost:44346/api/templates/${id + 1}`;
    updateData.id = +id;
    updateData.name = template.name;
    updateData.ownerId = +prompt("New owner id?");

    if (this.templates[id].templateState == "Active") {
      updateData.stateId = 1;
      this.apiClient.put(query, updateData).subscribe(result => {
        this.getTemplates();
      }, error => console.error(error));
    }
    else {
      updateData.stateId = 2;
      this.apiClient.put(query, updateData).subscribe(result => {
        this.getTemplates();
      }, error => console.error(error));
    }
  }

  loadTemplateVersions(id: string) {
    this.templateChosen = true;
    let query = `https://localhost:44346/api/templates/${id}`
    this.apiClient.get<TemplateVersions>(query).subscribe(result => {
      this.pickedTemplate = result;
    }, error => console.error(error));
  }

  switchVersionState(id: string, versionState: string) {
    console.log(versionState)
    if (versionState == "Active") {
      let query = `https://localhost:44346/api/templates/version/${id}`
      this.apiClient.delete(query).subscribe(result => {
        this.loadTemplateVersions(id);
      }, error => console.error(error));
    }
    else {
      let query = `https://localhost:44346/api/templates/${this.pickedTemplate.id}/${id}`
      this.apiClient.put(query, "Empty Body").subscribe(result => {
        this.loadTemplateVersions(id);
      }, error => console.error(error));
    }
  }
}

