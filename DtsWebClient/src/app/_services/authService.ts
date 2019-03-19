import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User, UserData } from '../_models/user';
import { Token, TokenValue } from '../_models/token';
import { NgForm } from '@angular/forms';
import { SessionStorageService } from 'angular-web-storage';
import queries from '../../assets/queries.json';
import { LoginData } from '../_models/loginData';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {

  constructor(private apiClient: HttpClient, private session: SessionStorageService) { 
    }

  login(loginData: LoginData) {
    this.apiClient.post<TokenValue>(queries.loginAPIPath, loginData).subscribe(result => {
      const user = new User();

      const helper = new JwtHelperService();
      let token = helper.decodeToken(result.content);

      user.id = token.unique_name;
      user.role = token.role;
      user.token = result.content;

      
      this.session.set("loggedUser", user);
      this.apiClient.get(queries.userPath + user.id).subscribe(result => {
        const userData = result;
        this.session.set("userData", userData)
        location.reload(true);
      })
    }, error => console.error(error));
  }

  logout() {
    this.session.remove('loggedUser');
    this.session.remove("userData");
    location.reload(true);
  }

  updateUserData(userId: string) {
    this.apiClient.get(queries.userPath + userId).subscribe(result => {
      const userData = result;
      this.session.set("userData", userData)
      location.reload(true);
    })
  }
}
