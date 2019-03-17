import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { first } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http'
import { AuthenticationService } from '../../_services/authService';

@Component({
  selector: 'app-login-form',
  templateUrl: 'login-form.component.html'
})
export class LoginComponent implements OnInit {
  password: string;
  login: string;

  constructor(private apiClient: HttpClient, private authenticationService: AuthenticationService) {
  }
  ngOnInit() {
  }

  logUserIn(loginForm: NgForm) {
    this.authenticationService.login(loginForm)
  }

  logout() {
    this.authenticationService.logout();
  }
}
