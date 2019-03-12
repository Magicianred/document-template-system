import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Template } from '../../_models/template'

@Component({
  selector: 'app-editors-templates',
  templateUrl: './editors-templates.component.html',
  styleUrls: ['./editors-templates.component.css']
})
export class EditorsTemplatesComponent implements OnInit {

  apiClient: HttpClient;
  templates: Template[];

  @Input()
  editorId: string;

  @Output() sendChosenId: EventEmitter<string> = new EventEmitter<string>()

  constructor(http: HttpClient) {
    this.apiClient = http;

  }
  ngOnInit() {
    this.getTemplates();
  }
  
  getTemplates() {
    let query = `https://localhost:44346/api/templates/editor/${this.editorId}`
    this.apiClient.get<Template[]>(query).subscribe(result => {
      this.templates = result;
    }, error => console.error(error));
  }
  
  loadTemplate(id: string) {
    this.sendChosenId.emit(id);
  }


}
