import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Template } from '../../_models/template';
import queries from '../../../assets/queries.json';
import { Sort } from '@angular/material';
import { UserData } from 'src/app/_models/user';
import { template } from '@angular/core/src/render3';
import { SessionStorageService } from 'angular-web-storage';
import { TemplateVersions } from 'src/app/_models/templateVersion';

@Component({
  selector: 'app-editors-templates',
  templateUrl: './editors-templates.component.html',
  styleUrls: ['./editors-templates.component.css']
})
export class EditorsTemplatesComponent implements OnInit {

  headElements = ['Name', 'Template State', 'Version Count'];
  templates: TemplateVersions[];
  sortedTemplates: TemplateVersions[];
  editors: UserData[];
  selectedEditor: UserData;


  @Input()
  editorId: string;

  @Output() sendChosenId: EventEmitter<string> = new EventEmitter<string>()

  constructor(
    private apiClient: HttpClient,
    private session: SessionStorageService
  ) { }

  ngOnInit() {
    this.getTemplates();
    this.getEditors();
    this.selectedEditor = this.session.get("userData");
  }
  
  getTemplates() {
    this.apiClient.get<TemplateVersions[]>(queries.editorTemplates + this.editorId).subscribe(result => {
      this.templates = result;
      this.sortedTemplates = result;
    }, error => console.error(error));
  }

  getEditors() {
    let query = `${queries.userPath}type/editor`
    this.apiClient.get<UserData[]>(query).subscribe(result => {
      this.editors = result;
    }, error => console.error(error));
  }

  switchSelectedEditor(editor: UserData) {
    this.selectedEditor=editor;
  }

  changeTemplateOwner(templateId: string, name: string) {
    let query = `${queries.templatesPath}${templateId}`
    let DTO = {
      name: name,
      ownerId: this.selectedEditor.id,
      stateId: 2,
    };
    this.apiClient.put(query, DTO).subscribe(result => {
      location.reload(true);
    }, error => console.error(error));
  }

  sortTemplates(sort: Sort) {
    const data = this.sortedTemplates;

    if (!sort.active || sort.direction === '') {
      this.sortedTemplates = data;
      return;
    }
    this.sortedTemplates = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'Name': return this.compare(a.name, b.name, isAsc);
        case 'Template State': return this.compare(a.templateState, b.templateState, isAsc);
        case 'Version Count': return this.compare(a.templateVersions.length, b.templateVersions.length, isAsc);
        default: return 0;
      }
    });
  }

  filterTemplates(searchText: string) {
    this.sortedTemplates = this.templates.filter(template =>
      template.name.indexOf(searchText) !== -1
      || template.templateState.indexOf(searchText) !== -1);
  }

  compare(a: number | string, b: number | string, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }
  
  loadTemplate(id: string) {
    this.sendChosenId.emit(id);
  }


}
