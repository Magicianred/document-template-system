import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-editor-panel',
  templateUrl: './editor-panel.component.html',
  styleUrls: ['./editor-panel.component.css']
})
export class EditorPanelComponent implements OnInit {
  htmlContent: string;
  baseUrl: string;
  apiClient: HttpClient;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiClient = http;
    this.baseUrl = baseUrl;
  }

  ngOnInit() {
  }

  saveNewTemplate() {
    let authorId = 2;
    let templateName = "test50500";
    let templateData = {
      authorId: authorId,
      templateName: templateName,
      template: this.htmlContent
    }

    this.apiClient.post("https://localhost:44346/api/templates/", templateData).subscribe(result => {
      console.log(result)
    }, error => console.error(error));
  }
}
