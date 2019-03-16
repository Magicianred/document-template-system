import { Component, OnInit, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Template } from '../_models/template'
import { TemplateDataUpdate } from '../_models/templateDataUpdate';
import { TemplateVersions } from '../_models/templateVersion';
import { NgbModal} from '@ng-bootstrap/ng-bootstrap';
import queries from '../../assets/queries.json';


@Component({
  selector: 'app-admin-template-panel',
  templateUrl: './admin-template-panel.component.html',
  styleUrls: ['./admin-template-panel.component.css']
})
export class AdminTemplatePanelComponent implements OnInit {
  closeResult: string;
  templates: Template[];
  templateChosen: boolean;
  pickedTemplate: TemplateVersions;
  templateContent: HTMLElement;

  constructor(
    private apiClient: HttpClient,
    private modalService: NgbModal
  ) {
  }

  ngOnInit() {
    this.getTemplates();
    this.templateChosen = false;
  }

  getTemplates() {
    this.apiClient.get<Template[]>(queries.templatesPath).subscribe(result => {
      this.templates = result;
    }, error => console.error(error));
  }

  switchState(id: string, state: string, name:string) {

    if (state == "Inactive") {
      let updateData = new TemplateDataUpdate();
      updateData.id = +id;
      updateData.name = name;
      updateData.ownerId = +prompt("editor id");
      updateData.stateId = 1;
      this.apiClient.put(queries.templatesPath + id, updateData).subscribe(result => {
        this.getTemplates();
      }, error => console.error(error));
    }
    else {
      this.apiClient.delete(queries.templatesPath + id).subscribe(result => {
        this.getTemplates();
      }, error => console.error(error));
    }
  }

  changeOwner(event: any) {
    let templateIndex = event.path[1].rowIndex - 1;
    let template = this.templates[templateIndex];
    let updateData = new TemplateDataUpdate();

    updateData.id = template.id;
    updateData.name = template.name;
    updateData.ownerId = +prompt("New owner id?");

    if (template.templateState == "Active") {
      updateData.stateId = 1;
      this.apiClient.put(queries.templatesPath + template.id, updateData).subscribe(result => {
        this.getTemplates();
      }, error => console.error(error));
    }
    else {
      updateData.stateId = 2;
      this.apiClient.put(queries.templatesPath + template.id, updateData).subscribe(result => {
        this.getTemplates();
      }, error => console.error(error));
    }
  }

  loadTemplateVersions(id: string) {
    this.templateChosen = true;
    this.apiClient.get<TemplateVersions>(queries.templatesPath + id).subscribe(result => {
      this.pickedTemplate = result;
    }, error => console.error(error));
  }

  switchVersionState(id: string, versionState: string) {
    if (versionState == "Active") {
      this.apiClient.delete(queries.templateVersions + id).subscribe(result => {
        this.loadTemplateVersions(this.pickedTemplate.id.toString());
      }, error => console.error(error));
    }
    else {
      this.apiClient.put(queries.templatesPath + this.pickedTemplate.id + "/" + id, "Empty Body").subscribe(result => {
        this.loadTemplateVersions(this.pickedTemplate.id.toString());
      }, error => console.error(error));
    }
  }

  showVersion(event: any, content) {
    this.templateChosen = true;
    let versionIndex = event.path[1].rowIndex - 1;
    let version = this.pickedTemplate.versions[versionIndex].templateVersion;
    this.templateContent = document.createElement("DIV");
    this.templateContent.innerHTML = version;
    this.open(content);
  }

  open(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title', size:'lg' });
    document.getElementById("version-container").appendChild(this.templateContent);
  }

}

