import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgxEditorModule } from 'ngx-editor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './login-form/login-form.component'
import { SignInFormComponent } from './sign-in-form/sign-in-form.component';
import { EditorPanelComponent } from './editor-panel/editor-panel.component';
import { FormsModule } from '@angular/forms';
import { EditorsTemplatesComponent } from './editor-panel/editors-templates/editors-templates.component';
import { TemplateDataComponent } from './editor-panel/template-data/template-data.component';
import { FormPickerComponent } from './form-picker/form-picker.component';
import { AdminTemplatePanelComponent } from './admin-template-panel/admin-template-panel.component';
import { TemplateAdderComponent } from './editor-panel/template-adder/template-adder.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    HomeComponent,
    LoginComponent,
    SignInFormComponent,
    EditorPanelComponent,
    EditorsTemplatesComponent,
    TemplateDataComponent,
    FormPickerComponent,
    AdminTemplatePanelComponent,
    TemplateAdderComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgxEditorModule,
    FormsModule
  ],
  providers: [{ provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}
