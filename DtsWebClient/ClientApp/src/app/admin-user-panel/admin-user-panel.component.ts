import { Component, OnInit, Input, HostListener } from '@angular/core';
import { UserData, UserChangingData } from '../_models/user';
import { UserType } from '../_models/userType';
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
  types: UserType[];
  chosenUser: UserData;
  sortedUsers: UserData[];
  searchText: string = '';
  headElements = ['Name', 'Surname', 'Email', 'Type', 'Status'];

  constructor(
    private apiClient: HttpClient,
  ) {}

  ngOnInit() {
    this.getUsers();
    this.getUserTypes();
  }

  getUsers() {
    this.apiClient.get<UserData[]>(queries.userPath).subscribe(result => {
      this.users = result;
      this.sortedUsers = result;
    }, error => console.error(error));
  }

  getUserTypes() {
    this.apiClient.get<UserType[]>(queries.userTypesPath).subscribe(result => {
      this.types = result;
    })
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
        case 'Name': return this.compare(a.name, b.name, isAsc);
        case 'Surname': return this.compare(a.surname, b.surname, isAsc);
        case 'Role': return this.compare(a.role, b.role, isAsc);
        case 'Email': return this.compare(a.email, b.email, isAsc);
        case 'Type': return this.compare(a.type, b.type, isAsc);
        case 'Status': return this.compare(a.status, b.status, isAsc);
        default: return 0;
      }
    });
  }

  compare(a: number | string, b: number | string, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
  }

  filterUsers(searchText: string) {
    this.sortedUsers = this.users.filter(user =>
      user.name.indexOf(searchText.charAt(0).toUpperCase() + searchText.slice(1)) !== -1
      || user.email.indexOf(searchText) !== -1
      || user.surname.indexOf(this.capitalize(searchText)) !== -1
      || user.type.indexOf(this.capitalize(searchText)) !== -1
      || user.status.indexOf(this.capitalize(searchText)) !== -1);
  }

  capitalize(str: string) {
    return str.charAt(0).toUpperCase() + str.slice(1);
  }

  changeUserState(user: UserData, event: any ) {
    let button = event.path[0];
    this.swapButtonContentWithSpinner(button);

    if (user.status == "Active") {
      this.apiClient.delete(queries.userPath + user.id).subscribe(result => {
        this.swapSpinnerWithTick(button);
        this.getUsers();
      }, error => {
        console.error(error);
        this.swapSpinnerWithTick(button);
      });
    }
    else {
      this.apiClient.put(queries.userPath + user.id + "/activate", user.id).subscribe(result => {
        this.swapSpinnerWithTick(button);
        this.getUsers();
      }, error => {
        console.error(error);
        this.swapSpinnerWithTick(button);
      });
    }
  }

  changeUserType(user: UserData, type: string) {
    if (type == user.type) {
      user.newType = type;
      return
    }
    user.newType = type;
  } 

  changeUserData(name: any, surname: any, email: any, user: UserData, event: any) {
    
    this.chosenUser = user;
   
    let button = event.path[0];
    this.swapButtonContentWithSpinner(button);
    if (this.changeInUserData(name, surname, email, user)) {
      this.swapSpinnerWithTick(button);
      return
    }

    let newData = new UserChangingData();
    newData.id = String(this.chosenUser.id);
    newData.name = name == "" ? this.chosenUser.name : name;
    newData.surname = surname == "" ? this.chosenUser.surname : surname;
    newData.email = email == "" ? this.chosenUser.email : email;

    let dataQuery = `${queries.userPath}${this.chosenUser.id}`
    this.apiClient.put(dataQuery, newData).subscribe(result => {

      let typeQuery = `${queries.userPath}${user.id}/type/${user.newType}`
      this.apiClient.put(typeQuery, "EmptyBody").subscribe(result => {
        this.getUsers();
        this.swapSpinnerWithTick(button);
      });
      
    }, error => {
      console.error(error);
      this.swapSpinnerWithTick(button);
    });
  }

  changeInUserData(name: string, surname: string, email: string, user: UserData) {
    return (name == "" && surname == "" && email == "" && (user.newType == user.type || user.newType == null))
  }

  swapButtonContentWithSpinner(button: any) {
    button.disabled = 'disabled';
    button.innerHTML = '<div class="spinner-border" role = "status"> <span class="sr-only" > Loading...</span></div>'
  }

  swapSpinnerWithTick(button: any) {
    button.disabled = '';
    button.innerHTML = '&#x2713';
  }

}
