import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TemplateVersions } from '../../_models/templateVersion';
import { Version } from '../../_models/Version';
import { document } from 'ngx-bootstrap';
import queries from '../../../assets/queries.json';
import { Sort } from '@angular/material';


@Component({
  selector: 'app-template-data',
  templateUrl: './template-data.component.html',
  styleUrls: ['./template-data.component.css']
})
export class TemplateDataComponent implements OnInit {
  template: TemplateVersions;
  sortedVersions: Version[];
  templateChosen: boolean;
  version: string;
  htmlContent: string;
  headElements = ['Creation Date', 'Creator', 'Version State'];

  @Input()
  tempId: string;
  @Input()
  editorId: string;

  @Output() back: EventEmitter<string> = new EventEmitter<string>()

  constructor(private apiClient: HttpClient, ) {
  }

  ngOnInit() {
    this.getTemplate();
  }

  sortUsers(sort: Sort) {
    const data = this.sortedVersions;

    if (!sort.active || sort.direction === '') {
      this.sortedVersions = data;
      return;
    }
    this.sortedVersions = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'Creation Date': return this.compare(a.creationTime, b.creationTime, isAsc);
        case 'Creator': return this.compare(a.creator.surname, b.creator.surname, isAsc);
        case 'Version State': return this.compare(a.versionState, b.versionState, isAsc);
        default: return 0;
      }
    });
  }

  compare(a: number | string, b: number | string, isAsc: boolean) {
    return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  filterVersions(searchText: string) {
    this.sortedVersions = this.template.templateVersions.filter(version =>
      version.creationTime.indexOf(searchText) !== -1
      || version.creator.surname.indexOf(searchText) !== -1
      || version.creator.email.indexOf(searchText) !== -1
      || version.versionState.indexOf(searchText) !== -1);
  }

  showVersion(event: any) {
    this.templateChosen = true;
    let versionIndex = event.path[1].rowIndex - 1;
    let templateContent = this.template.templateVersions[versionIndex].content;
    let editorArea = document.getElementsByClassName("ngx-editor-textarea")[0];
    editorArea.innerHTML = templateContent;
  }
  
  getTemplate() {
    this.templateChosen = false;
    this.apiClient.get<TemplateVersions>(queries.templatesPath + this.tempId).subscribe(result => {
      this.template = result;
      this.sortedVersions = result.templateVersions;
    }, error => console.error(error));
  }

  showVersions() {
    this.templateChosen = false;
  }

  saveNewTemplateVersion() {
    let templateData = {
      authorId: this.editorId,
      template: this.htmlContent,
      templateName: this.template.name
    }

    let query = `${queries.templatesPath}template/${this.tempId}/version`;

    this.apiClient.put(query, templateData).subscribe(result => {
      this.templateChosen = false;
      this.getTemplate();
    }, error => console.error(error));
    
  }

  goBackToTemplates() {
    this.back.emit("")
  }

}
