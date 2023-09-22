import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import {
  SocialAuthService,
  GoogleLoginProvider,
  SocialUser,
} from '@abacritt/angularx-social-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  socialUser!: SocialUser;
  isLoggedin?: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private socialAuthService: SocialAuthService
  ) {}

  ngOnInit() {
    this.isLoggedin = false;
    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
    this.socialAuthService.authState.subscribe((user) => {
      this.socialUser = user;
      this.isLoggedin = !!user;
      console.log('logged in:', this.isLoggedin);
      console.log(this.socialUser);
    });
  }
  // loginWithGoogle(): void {
  //   this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  // }
  logOut(): void {
    this.socialAuthService.signOut();
  }

  submit(login: NgForm): void {
    console.log(login.form.value);
    window.alert(
      `form submitted. Name: ${login.form.value.name}, Email: ${login.form.value.email}`
    );
  }

  // constructor(private router: Router) {}
  // navigateToLogin() {
  //   this.router.navigate(['/login']);
  // }
}
