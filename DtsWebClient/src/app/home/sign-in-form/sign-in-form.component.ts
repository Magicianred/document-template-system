import { Component, OnInit } from '@angular/core';
import { NgForm, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/_services/authService';
import { ActivatedRoute, Router } from '@angular/router';
import { retry } from 'rxjs/operators';
import { HttpClient } from '@angular/common/http';
import { SignInForm } from 'src/app/_models/signIn';
import queries from '../../../assets/queries.json';

@Component({
  selector: 'app-sign-in-form',
  templateUrl: './sign-in-form.component.html',
  styleUrls: ['./sign-in-form.component.css']
})
export class SignInFormComponent implements OnInit {
  signInForm: FormGroup;
  returnUrl: string;
  loading = false;
  submitted = false;

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private apiClient: HttpClient) { }

  ngOnInit() {
    this.signInForm = this.formBuilder.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      login: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      passwordConfirm: [''],
    }, { validator: this.checkPasswords });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get FormControls() {
    return this.signInForm.controls;
  }

  signIn() {
    this.submitted = true;


    if (this.signInForm.invalid) {
      return;
    }
    console.log(this.signInForm.value)
    this.loading = true;

    this.apiClient.post(queries.signInPath, this.signInForm.value).subscribe(result => {
      
      console.log(result);
      this.loading = false;
    }, error => console.error(error));
  }

  checkPasswords(signInForm: FormGroup) {
    let pass = signInForm.controls.password.value;
    let confirmPass = signInForm.controls.passwordConfirm.value;

    return pass === confirmPass ? null : { notSame: true }
  }

}
