import { Component, OnInit, Inject} from '@angular/core';
import { HttpClient} from '@angular/common/http';
import { Template } from '../_models/template'
import { TemplateContent } from '../_models/templateContent';

@Component({
  selector: 'app-form-picker',
  templateUrl: './form-picker.component.html',
  styleUrls: ['./form-picker.component.css']
})
export class FormPickerComponent implements OnInit {
  baseUrl: string;
  apiClient: HttpClient;
  templates: Template[];
  formBase: object;
  chosenFormId: number;
  gotForm: boolean;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.apiClient = http;
    this.baseUrl = baseUrl;

  }
  ngOnInit() {
    this.getTemplates();
    this.gotOutput = false;
    this.gotForm = false;
  }

  getTemplates() {
    this.apiClient.get<Template[]>("https://localhost:44346/api/templates/").subscribe(result => {
      this.templates = result;
    }, error => console.error(error));
  }

  showVersion(id: number) {
    this.chosenFormId = id;
    let query = `https://localhost:44346/api/templates/form/${id}`;
    this.apiClient.get<object>(query).subscribe(result => {
      this.formBase = result;
      this.gotForm = true;
    }, error => console.error(error));
  }

  postData(formValues: any) {
    
    let query = `https://localhost:44346/api/templates/form/${this.chosenFormId}`;

    this.apiClient.post<TemplateContent>(query, formValues).subscribe(result => {
      let templateContent = result;
      const tempCont = document.createElement("DIV");
      tempCont.innerHTML = templateContent.content;
      this.gotForm = false;

      let w = window.open();
    
      $(w.document.body).html(tempCont);

    }, error => console.error(error));
  }


}
