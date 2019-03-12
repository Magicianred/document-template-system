import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-template-adder',
  templateUrl: './template-adder.component.html',
  styleUrls: ['./template-adder.component.css']
})
export class TemplateAdderComponent implements OnInit {
  htmlContent: string;
  apiClient: HttpClient;

  @Input()
  editorId: string;

  @Output() closeEditor: EventEmitter<string> = new EventEmitter<string>()

  constructor(http: HttpClient, ) {
    this.apiClient = http;
  }
  ngOnInit() {
  }

  saveNewTemplateVersion() {
    let templateData = {
      authorId: this.editorId,
      templateName: prompt("Template Name?"),
      template: this.htmlContent
    }

    let query = `https://localhost:44346/api/templates/`;

    this.apiClient.post(query, templateData).subscribe(result => {
      this.backToMain();
    }, error => console.error(error)); 

  }

  backToMain() {
    this.closeEditor.emit("")
  }
}
