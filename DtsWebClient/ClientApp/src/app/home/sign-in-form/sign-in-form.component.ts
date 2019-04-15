import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import queries from '../../../assets/queries.json';

@Component({
  selector: 'app-sign-in-form',
  templateUrl: './sign-in-form.component.html',
  styleUrls: ['./sign-in-form.component.css']
})
export class SignInFormComponent implements OnInit {
  signInForm: FormGroup;
  returnUrl: string;
  submitted = false;
  submitResult: string;

  @Output() loading: EventEmitter<boolean> = new EventEmitter<boolean>()

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private apiClient: HttpClient)
  { }

  

  ngOnInit() {
    this.signInForm = this.formBuilder.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      login: ['', Validators.required],
      email: ['', Validators.email],
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
    this.loading.emit(true);
    this.apiClient.post(queries.signInPath, this.signInForm.value).subscribe(result => {
      alert("Account created properly, please wait for activation")
      location.reload(true);
    });
  }

  checkPasswords(signInForm: FormGroup) {
    let pass = signInForm.controls.password.value;
    let confirmPass = signInForm.controls.passwordConfirm.value;

    return pass === confirmPass ? null : { notSame: true }
  }

}
