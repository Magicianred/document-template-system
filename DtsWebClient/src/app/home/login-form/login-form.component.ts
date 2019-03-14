import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';
import { first } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http'
import { Token, TokenValue } from 'src/app/_models/token';
import { JwtHelperService } from '@auth0/angular-jwt';
import { SessionStorageService } from 'angular-web-storage';
import { User } from 'src/app/_models/user';

@Component({
  selector: 'app-login-form',
  templateUrl: 'login-form.component.html'
})
export class LoginComponent implements OnInit {
  password: string;
  login: string;
  apiClient: HttpClient;
  token: Token;
  tokenValue: TokenValue;

  constructor(http: HttpClient, public session: SessionStorageService) {
    this.apiClient = http;
  }

  ngOnInit() {
    

  }

  logUserIn(loginForm: NgForm) {
    let query = `https://localhost:44381/api/auth/login`;

    this.apiClient.post(query, loginForm).subscribe(result => {

      const user = new User();
      this.tokenValue = result;

      const helper = new JwtHelperService();
      this.token = helper.decodeToken(this.tokenValue.content);

      user.id = this.token.unique_name;
      user.role = this.token.role;
      user.token = this.tokenValue.content;

      this.session.set("loggedUser", user);      
    }, error => console.error(error));
  }
}
