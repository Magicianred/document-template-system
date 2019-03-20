import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { EditorPanelComponent } from './editor-panel/editor-panel.component'
import { FormPickerComponent } from './form-picker/form-picker.component';
import { AdminTemplatePanelComponent } from './admin-template-panel/admin-template-panel.component';
import { AuthGuard } from './_guards/auth.guard';
import { EditorGuard } from './_guards/editor.guard';
import { AdminGuard } from './_guards/admin.guard';
import { AdminUserPanelComponent } from './admin-user-panel/admin-user-panel.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full',
  },
  {
    path: 'editorPanel',
    component: EditorPanelComponent,
    canActivate: [EditorGuard]
  },
  {
    path: 'formPicker',
    component: FormPickerComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'adminTemplatePanel',
    component: AdminTemplatePanelComponent,
    canActivate: [AdminGuard]
  },
  {
    path: 'adminUserPanel',
    component: AdminUserPanelComponent,
    canActivate: [AdminGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
