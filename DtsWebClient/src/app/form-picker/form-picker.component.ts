import { Component, OnInit, Inject} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Template } from '../_models/template'
import { TemplateContent } from '../_models/templateContent';
import queries from '../../assets/queries.json';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-form-picker',
  templateUrl: './form-picker.component.html',
  styleUrls: ['./form-picker.component.css']
})
export class FormPickerComponent implements OnInit {
  baseUrl: string;
  templates: Template[];
  formBase: object;
  chosenFormId: number;
  gotForm: boolean;

  constructor(
    private apiClient: HttpClient,
    private modalService: NgbModal
  ) {
  }

  ngOnInit() {
    this.getTemplates();
    this.gotForm = false;
  }

  getTemplates() {
    this.apiClient.get<Template[]>(queries.templatesPath).subscribe(result => {
      this.templates = result;
    }, error => console.error(error));
  }

  showVersion(id: number) {
    this.chosenFormId = id;
    this.apiClient.get<object>(queries.formPath + id).subscribe(result => {
      this.formBase = result;
      this.gotForm = true;
    }, error => console.error(error));
  }

  postData(formValues: any, content) { 

    this.apiClient.post<TemplateContent>(queries.formPath + this.chosenFormId, formValues).subscribe(result => {
      let templateContent = result;
      const tempCont = document.createElement("DIV");
      tempCont.innerHTML = templateContent.content;
      //this.gotForm = false;

      this.open(content, tempCont)

    }, error => console.error(error));
  }

  open(content, tempCont: HTMLElement) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title', size: 'lg' });
    document.getElementById("version-container").appendChild(tempCont);
  }


}
