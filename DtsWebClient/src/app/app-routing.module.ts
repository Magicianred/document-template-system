import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { EditorPanelComponent } from './editor-panel/editor-panel.component'
import { EditorsTemplatesComponent } from './editors-templates/editors-templates.component';
import { TemplateDataComponent } from './template-data/template-data.component';
import { FormPickerComponent } from './form-picker/form-picker.component';
import { AdminTemplatePanelComponent } from './admin-template-panel/admin-template-panel.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'editorPanel', component: EditorPanelComponent },
  { path: 'editorTemplates', component: EditorsTemplatesComponent },
  { path: 'templateData', component: TemplateDataComponent },
  { path: 'formPicker', component: FormPickerComponent },
  { path: 'adminTemplatePanel', component: AdminTemplatePanelComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
