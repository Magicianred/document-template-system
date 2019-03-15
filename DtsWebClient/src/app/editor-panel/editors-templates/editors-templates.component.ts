import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Template } from '../../_models/template';
import queries from '../../../assets/queries.json';

@Component({
  selector: 'app-editors-templates',
  templateUrl: './editors-templates.component.html',
  styleUrls: ['./editors-templates.component.css']
})
export class EditorsTemplatesComponent implements OnInit {


  templates: Template[];

  @Input()
  editorId: string;

  @Output() sendChosenId: EventEmitter<string> = new EventEmitter<string>()

  constructor(private apiClient: HttpClient) {


  }
  ngOnInit() {
    this.getTemplates();
  }
  
  getTemplates() {
    this.apiClient.get<Template[]>(queries.editorTemplates + this.editorId).subscribe(result => {
      this.templates = result;
    }, error => console.error(error));
  }
  
  loadTemplate(id: string) {
    this.sendChosenId.emit(id);
  }


}
