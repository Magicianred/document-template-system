import { Component, OnInit, Input, HostListener } from '@angular/core';
import { UserData } from '../_models/user';
import queries from '../../assets/queries.json';
import { HttpClient } from '@angular/common/http';
import { Sort } from '@angular/material';

@Component({
  selector: 'app-admin-user-panel',
  templateUrl: './admin-user-panel.component.html',
  styleUrls: ['./admin-user-panel.component.css']
})
export class AdminUserPanelComponent implements OnInit {
  closeResult: string;
  users: UserData[];
  userChosen: boolean;
  chosenUser: UserData;
  sortedUsers: UserData[];
  searchText: string = '';
  headElements = ['name', 'surname', 'type', 'email', 'status'];

  constructor(
    private apiClient: HttpClient,
  ) {}

  ngOnInit() {
    this.getUsers();
    this.userChosen = false;
  }

  sortUsers(sort: Sort) {
    const data = this.sortedUsers;
    
    if (!sort.active || sort.direction === '') {
      this.sortedUsers = data;
      return;
    }
    this.sortedUsers = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'name': return this.compare(a.name, b.name, isAsc);
        case 'surname': return this.compare(a.surname, b.surname, isAsc);
        case 'role': return this.compare(a.role, b.role, isAsc);
        case 'email': return this.compare(a.email, b.email, isAsc);
        case 'type': return this.compare(a.type, b.type, isAsc);
        case 'status': return this.compare(a.status, b.status, isAsc);
        default: return 0;
      }
    });
  }

  compare(a: number | string, b: number | string, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  getUsers() {
    this.apiClient.get<UserData[]>(queries.userPath).subscribe(result => {
      this.users = result;
      this.sortedUsers = result;
    }, error => console.error(error));
  }

  filterUsers(searchText: string) {
    this.sortedUsers = this.users.filter(user =>
      user.name.indexOf(searchText) !== -1
      || user.email.indexOf(searchText) !== -1
      || user.surname.indexOf(searchText) !== -1
      || user.type.indexOf(searchText) !== -1
      || user.status.indexOf(searchText) !== -1);
  }

  changeUserState(id: string, status: string) {
    if (status == "Active") {
      this.apiClient.delete(queries.userPath + id).subscribe(result => {
        this.getUsers();
      }, error => console.error(error));
    }
    else {
      this.apiClient.put(queries.userPath + id + "/activate", id).subscribe(result => {
        this.getUsers();
      }, error => console.error(error));
    }
    
  }
  
}
