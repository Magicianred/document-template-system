import { Component, OnInit } from '@angular/core';
import { NgForm, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthenticationService } from 'src/app/_services/authService';
import { ActivatedRoute, Router } from '@angular/router';
import { retry } from 'rxjs/operators';


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
  passMatch = false;

  constructor(private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService) { }

  ngOnInit() {
    this.signInForm = this.formBuilder.group({
      name: ['', Validators.required],
      surname: ['', Validators.required],
      login: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      passwordConfirm: ['', Validators.required],
    }, { validator: this.checkPasswords });

    // reset login status
    this.authenticationService.logout();

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get FormControls() {
    return this.signInForm.controls;
  }

  signIn() {
    this.submitted = true;
    this.passMatch = this.signInForm.hasError('notSame');
    if (this.signInForm.invalid) {
      return;
    }
    
    this.loading = true;
    console.log(this.FormControls.name.value);
  }

  checkPasswords(signInForm: FormGroup) { // here we have the 'passwords' group
    let pass = signInForm.controls.password.value;
    let confirmPass = signInForm.controls.passwordConfirm.value;

    return pass === confirmPass ? null : { notSame: true }
  }

}
