import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { EditorPanelComponent } from './editor-panel/editor-panel.component'
import { EditorsTemplatesComponent } from './editors-templates/editors-templates.component';
import { TemplateDataComponent } from './template-data/template-data.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'editorPanel', component: EditorPanelComponent },
  { path: 'editorTemplates', component: EditorsTemplatesComponent },
  { path: 'templateData', component: TemplateDataComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
