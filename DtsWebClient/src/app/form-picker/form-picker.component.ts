import { Component, OnInit, Inject} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Template } from '../_models/template'
import { TemplateContent } from '../_models/templateContent';
import queries from '../../assets/queries.json';

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

  constructor(private apiClient: HttpClient) {
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

  postData(formValues: any) { 

    this.apiClient.post<TemplateContent>(queries.formPath + this.chosenFormId, formValues).subscribe(result => {
      let templateContent = result;
      const tempCont = document.createElement("DIV");
      tempCont.innerHTML = templateContent.content;
      this.gotForm = false;

      let w = window.open();
    
      $(w.document.body).html(tempCont);

    }, error => console.error(error));
  }


}
