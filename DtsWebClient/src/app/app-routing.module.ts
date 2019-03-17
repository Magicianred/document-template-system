import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { EditorPanelComponent } from './editor-panel/editor-panel.component'
import { FormPickerComponent } from './form-picker/form-picker.component';
import { AdminTemplatePanelComponent } from './admin-template-panel/admin-template-panel.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full',
  },
  {
    path: 'editorPanel',
    component: EditorPanelComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'formPicker',
    component: FormPickerComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'adminTemplatePanel',
    component: AdminTemplatePanelComponent,
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
