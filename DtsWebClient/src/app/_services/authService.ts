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

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;



  constructor(private apiClient: HttpClient, private session: SessionStorageService) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('loggedUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

  login(loginForm: NgForm) {
    

    this.apiClient.post<TokenValue>(queries.loginAPIPath, loginForm).subscribe(result => {
      const user = new User();

      const helper = new JwtHelperService();
      let token = helper.decodeToken(result.content);

      user.id = token.unique_name;
      user.role = token.role;
      user.token = result.content;

      
      this.session.set("loggedUser", user);
      this.apiClient.get(queries.userDataPath + user.id).subscribe(result => {
        const userData = result;
        this.session.set("userData", userData)
        location.reload(true);
      })
    }, error => console.error(error));
  }

    logout() {
      this.session.remove('loggedUser');
      this.session.remove("userData");
      this.currentUserSubject.next(null);
      location.reload(true);
    }
}
