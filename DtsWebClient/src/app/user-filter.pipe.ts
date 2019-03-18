import { Pipe, PipeTransform } from '@angular/core';
import { UserData } from './_models/user';

@Pipe({ name: 'searchUsers' })
export class SearchUsersPipe implements PipeTransform {
  transform(users: UserData[], searchText: string) {
    return users.filter(user =>
      user.name.indexOf(searchText) !== -1
      || user.email.indexOf(searchText) !== -1
      || user.surname.indexOf(searchText) !== -1
      || user.type.indexOf(searchText) !== -1
      || user.status.indexOf(searchText) !== -1);
  }
}
