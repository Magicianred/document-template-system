import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgxEditorModule } from 'ngx-editor';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginComponent } from './home/login-form/login-form.component'
import { SignInFormComponent } from './home/sign-in-form/sign-in-form.component';
import { EditorPanelComponent } from './editor-panel/editor-panel.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { EditorsTemplatesComponent } from './editor-panel/editors-templates/editors-templates.component';
import { TemplateDataComponent } from './editor-panel/template-data/template-data.component';
import { FormPickerComponent } from './form-picker/form-picker.component';
import { AdminTemplatePanelComponent } from './admin-template-panel/admin-template-panel.component';
import { TemplateAdderComponent } from './editor-panel/template-adder/template-adder.component';
import { AngularWebStorageModule } from 'angular-web-storage';
import { JwtInterceptor } from './_helpers/jwt.interceptor';
import { ErrorInterceptor } from './_helpers/error.interceptor';
import { UserPanelComponent } from './home/user-panel/user-panel.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


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
    TemplateAdderComponent,
    UserPanelComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgxEditorModule,
    FormsModule,
    AngularWebStorageModule,
    ReactiveFormsModule,
    NgbModule
  ],
  providers: [
    { provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}
