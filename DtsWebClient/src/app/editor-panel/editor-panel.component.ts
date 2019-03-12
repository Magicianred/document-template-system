import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Template } from '../_models/template'

@Component({
  selector: 'app-editor-panel',
  templateUrl: './editor-panel.component.html',
  styleUrls: ['./editor-panel.component.css']
})
export class EditorPanelComponent implements OnInit {
  htmlContent: string;
  apiClient: HttpClient;
  templates: Template[];
  openData: boolean;
  tempId: string;
  editorsId: string;

  constructor(http: HttpClient) {
    this.apiClient = http;
  }
  ngOnInit() {
    this.getEditorsID();
  }

  getEditorsID() {
    this.editorsId = prompt("Provide Editors ID");
  }

  saveNewTemplate() {
    let authorId = 2;
    let templateName = prompt("New template name?");
    let templateData = {
      authorId: authorId,
      templateName: templateName,
      template: this.htmlContent
    }

    this.apiClient.post("https://localhost:44346/api/templates/", templateData).subscribe(result => {
      console.log(result)
    }, error => console.error(error));
  }

  loadTemplate(event: any) {
    this.openData = true;
    this.tempId = event;
  }

  showTemplates() {
    this.openData = false;
  }
}
