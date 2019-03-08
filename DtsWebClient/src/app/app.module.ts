import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavComponent } from './nav/nav.component';
import { AboutComponent } from './about/about.component';
import { ContactComponent } from './contact/contact.component';
import { HomeComponent } from './home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { ProcessesComponent } from './home/processes/processes.component';
import { ProcessDetailsComponent } from './home/processdetails/processdetails.component';


@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    AboutComponent,
    ContactComponent,
    HomeComponent,
    ProcessesComponent,
    ProcessDetailsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [{ provide: 'BASE_URL', useFactory: getBaseUrl, deps: [] }],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function getBaseUrl() {
  return document.getElementsByTagName('base')[0].href;
}
