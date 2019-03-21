import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { UserData, UserChangingData } from '../../_models/user';
import { AuthenticationService } from 'src/app/_services/authService';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import queries from '../../../assets/queries.json';
import { SessionStorageService } from 'angular-web-storage';

@Component({
  selector: 'app-user-panel',
  templateUrl: './user-panel.component.html',
  styleUrls: ['./user-panel.component.css']
})
export class UserPanelComponent implements OnInit {
  private loggedUser: UserData;
  changeCredentialsForm: FormGroup;
  returnUrl: string;
  loaded = false;
  submitted = false;
  submitResult: string;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private apiClient: HttpClient,
    private session: SessionStorageService,
    private auth: AuthenticationService,
  ) {
    this.loggedUser = session.get("userData");
  } 

  ngOnInit() {
    this.changeCredentialsForm = this.formBuilder.group({
      Login: ['', Validators.required],
      Password: ['', Validators.required],
      NewLogin: ['', Validators.required],
      NewPassword : ['', Validators.required],
      passwordConfirm: [''],
    }, { validator: this.checkPasswords });
    this.loaded = false;
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get FormControls() {
    return this.changeCredentialsForm.controls;
  }

  checkPasswords(changeCredentialsForm: FormGroup) {
    let pass = changeCredentialsForm.controls.NewPassword.value;
    let confirmPass = changeCredentialsForm.controls.passwordConfirm.value;

    return pass === confirmPass ? null : { notSame: true }
  }

  changeCredentials() {
    this.submitted = true;


    if (this.changeCredentialsForm.invalid) {
      return;
    }
    console.log(this.changeCredentialsForm.value);
    this.apiClient.put(queries.loginAPIPath, this.changeCredentialsForm.value).subscribe(result => {
      this.loaded = true;
    }, error => this.submitResult = error);
  }

  changeUserData(name: any, surname: any, email: any) {
    let newData = new UserChangingData();

    if (name == "" && surname == "" && email == "") {
      return
    }

    newData.name = name == "" ? this.loggedUser.name : name;
    newData.surname = surname == "" ? this.loggedUser.surname : surname;
    newData.email = email == "" ? this.loggedUser.email : email;
    let mailRegex = /[A-Z0-9._%+-]+@[A-Z0-9.-]+.[A-Z]{2,4}/igm;

    if (!newData.email.match(mailRegex) && email != "") {
      console.log("debil")
      return;
    }

    let query = `${queries.userPath}${this.loggedUser.id}`
    this.apiClient.put(query, newData).subscribe(result => {
      this.auth.updateUserData(String(this.loggedUser.id));
    }, error => console.error(error));
  }
}
