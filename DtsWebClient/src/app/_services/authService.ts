import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { User } from '../_models/user';
import { Token, TokenValue } from '../_models/token';
import { NgForm } from '@angular/forms';
import { SessionStorageService } from 'angular-web-storage';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;
    token: Token;
    tokenValue: TokenValue;



  constructor(private apiClient: HttpClient, private session: SessionStorageService) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('loggedUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

  login(loginForm: NgForm) {
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

    logout() {
      this.session.remove('loggedUser');
      this.currentUserSubject.next(null);
    }
}
