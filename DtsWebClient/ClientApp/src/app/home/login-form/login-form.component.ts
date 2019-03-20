import { Component, OnInit, Output, EventEmitter } from '@angular/core';
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
  @Output() loading: EventEmitter<boolean> = new EventEmitter<boolean>()

  constructor(private authenticationService: AuthenticationService) {
  }
  ngOnInit() {
  }

  logUserIn() {
    let loginData = new LoginData();
    loginData.Login = this.login;
    loginData.Password = this.password;
    this.loading.emit(true);
    this.authenticationService.login(loginData);
  }

  logout() {
    this.authenticationService.logout();
  }
}
