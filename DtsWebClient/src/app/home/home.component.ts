import { Component, OnInit } from '@angular/core';
import { SessionStorageService } from 'angular-web-storage';
import { User } from '../_models/user';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  loggedUser: User;

  constructor(
    private session: SessionStorageService,
  ) {
    this.loggedUser = session.get("userData")
  }

  ngOnInit() {
  }
}
