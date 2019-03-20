import { Component, OnInit } from '@angular/core';
import { SessionStorageService } from 'angular-web-storage';
import { User } from '../_models/user';
import { AuthenticationService } from '../_services/authService';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  appTitle: string = 'Document Template System';
  loggedUser: User;
  constructor(
    private session: SessionStorageService,
    private authenticationService: AuthenticationService
  ) { }

  ngOnInit() {
    this.loggedUser = this.session.get("userData");
  }

  logout() {
    this.authenticationService.logout();
  }

}
