import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TemplateVersions } from '../../_models/templateVersion'
import { document } from 'ngx-bootstrap';



@Component({
  selector: 'app-template-data',
  templateUrl: './template-data.component.html',
  styleUrls: ['./template-data.component.css']
})
export class TemplateDataComponent implements OnInit {

  apiClient: HttpClient;
  template: TemplateVersions;
  templateChosen: boolean;
  version: string;
  htmlContent: string;


  @Input()
  tempId: string;
  @Input()
  editorId: string;

  @Output() back: EventEmitter<string> = new EventEmitter<string>()

  constructor(http: HttpClient, ) {
    this.apiClient = http;
  }

  ngOnInit() {
    this.getTemplate();
  }

  

  showVersion(event: any) {
    this.templateChosen = true;
    let versionIndex = event.path[1].rowIndex - 1;
    let templateContent = this.template.versions[versionIndex].templateVersion;
    let editorArea = document.getElementsByClassName("ngx-editor-textarea")[0];
    editorArea.innerHTML = templateContent;
  }
  
  getTemplate() {
    this.templateChosen = false;
    let query = `https://localhost:44346/api/templates/${this.tempId}`
    this.apiClient.get<TemplateVersions>(query).subscribe(result => {
      this.template = result;
    }, error => console.error(error));
  }

  showVersions() {
    this.templateChosen = false;
  }

  saveNewTemplateVersion() {
    let templateData = {
      authorId: this.editorId,
      template: this.htmlContent
    }

    let query = `https://localhost:44346/api/templates/template/${this.tempId}/version`;

    this.apiClient.put(query, templateData).subscribe(result => {
      this.templateChosen = false;
      this.getTemplate();
    }, error => console.error(error));
    
  }

  goBackToTemplates() {
    this.back.emit("")
  }

}
