import { Component, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Template } from '../_models/template'
import queries from '../../assets/queries.json'
import { SessionStorageService } from 'angular-web-storage';
import { AuthenticationService } from '../_services/authService';
@Component({
  selector: 'app-editor-panel',
  templateUrl: './editor-panel.component.html',
  styleUrls: ['./editor-panel.component.css']
})
export class EditorPanelComponent implements OnInit {
  htmlContent: string;
  templates: Template[];
  openData: boolean;
  tempId: string;
  editorId: string;
  createNew: boolean;

  constructor(
    private apiClient: HttpClient,
    private session: SessionStorageService,
  ) {
    
  }
  ngOnInit() {
    this.getEditorsID();
  }

  getEditorsID() {
    this.editorId = this.session.get("loggedUser").id
  }

  saveNewTemplate() {
    let templateName = prompt("New template name?");
    let templateData = {
      authorId: this.editorId,
      templateName: templateName,
      template: this.htmlContent
    }

    this.apiClient.post(queries.templatesPath, templateData).subscribe(result => {
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
