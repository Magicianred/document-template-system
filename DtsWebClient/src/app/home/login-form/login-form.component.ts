import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthenticationService } from '../../_services/authService';
import { LoginData } from 'src/app/_models/loginData';

@Component({
  selector: 'app-login-form',
  templateUrl: 'login-form.component.html'
})
export class LoginComponent implements OnInit {
  password: string;
  login: string;
  loading: boolean

  constructor(private authenticationService: AuthenticationService) {
  }
  ngOnInit() {
    this.loading = false;
  }

  logUserIn() {
    this.loading = true;
    let loginData = new LoginData();
    loginData.Login = this.login;
    loginData.Password = this.password;
    this.authenticationService.login(loginData);
  }

  logout() {
    this.authenticationService.logout();
  }
}
