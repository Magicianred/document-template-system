import { Component, OnInit } from '@angular/core';
import { SessionStorageService } from 'angular-web-storage'
import { User } from '../../_models/user';

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent implements OnInit {
  private loggedUser: User;

  constructor(private session: SessionStorageService) {
    this.loggedUser = session.get("userData")}

  ngOnInit() {
  }

}
