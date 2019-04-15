import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import queries from '../../../assets/queries.json';

@Component({
  selector: 'app-template-adder',
  templateUrl: './template-adder.component.html',
  styleUrls: ['./template-adder.component.css']
})
export class TemplateAdderComponent implements OnInit {
  htmlContent: string;


  @Input()
  editorId: string;

  @Output() closeEditor: EventEmitter<string> = new EventEmitter<string>()

  constructor( private apiClient: HttpClient, ) {

  }
  ngOnInit() {
  }

  saveNewTemplateVersion() {
    let templateData = {
      authorId: this.editorId,
      templateName: prompt("Template Name?"),
      template: this.htmlContent
    }


    this.apiClient.post(queries.templatesPath, templateData).subscribe(result => {
      this.backToMain();
    }, error => console.error(error)); 

  }

  backToMain() {
    this.closeEditor.emit("")
  }
}
