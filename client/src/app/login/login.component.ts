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
  isLoggedinSocial?: boolean;
  isLoggedinManual?: boolean;
  status?: string;
  name?: string;
  email?: string;
  token?: string;

  constructor(
    private formBuilder: FormBuilder,
    private socialAuthService: SocialAuthService
  ) {}

  ngOnInit() {
    this.isLoggedinSocial = false;
    this.isLoggedinManual = false;
    this.status = 'Not logged in';

    this.loginForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });

    this.socialAuthService.authState.subscribe(async (user) => {
      this.socialUser = user;
      this.isLoggedinSocial = !!user;
      console.log('logged in:', this.isLoggedinSocial);
      console.log(this.socialUser);
      console.log(user);

      if (user) {
        const response = await fetch('http://localhost:5001/api/gauth', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(user),
        });

        if (response.ok) {
          const responseData = await response.json();
          console.log('Response data:', responseData);

          this.status = 'Logged in via Google';
          this.email = responseData.email;
          this.name = responseData.name;
          this.token = responseData.token.substr(0, 20);
        } else {
          console.error(
            'Failed to fetch data:',
            response.status,
            response.statusText
          );

          this.status = 'Error logging in via Google';
        }
      }
    });
  }

  loginWithGoogle(): void {
    this.socialAuthService
      .signIn(GoogleLoginProvider.PROVIDER_ID)
      .then((data: any) => console.log(data));
  }

  logOutSocial(): void {
    this.socialAuthService.signOut();

    this.status = 'Not logged in';
    this.email = '';
    this.name = '';
    this.token = '';
  }

  logOutManual(): void {
    this.isLoggedinManual = false;
    this.status = 'Not logged in';
    this.email = '';
    this.name = '';
    this.token = '';
  }

  async submit(login: NgForm): Promise<void> {
    console.log('formdata', login.form.value);

    if (!login.form.value.email || !login.form.value.name) return;

    const email = login.form.value.email;
    const name = login.form.value.name;

    this.isLoggedinManual = true;
    this.status = 'Logged in manually';
    this.email = email;
    this.name = name;

    login.reset();

    // const response = await fetch('http://localhost:5001/api/manauth', {
    //   method: 'POST',
    //   headers: {
    //     'Content-Type': 'application/json',
    //   },
    //   body: JSON.stringify({email: email, name: name}),
    // });

    // if (response.ok) {
    //   const responseData = await response.json();
    //   console.log('Response data:', responseData);

    //   this.status = 'Logged in manually';
    //   this.email = responseData.email;
    //   this.name = responseData.name;
    //   this.token = responseData.token.substr(0, 20);
    // } else {
    //   console.error(
    //     'Failed to fetch data:',
    //     response.status,
    //     response.statusText
    //   );

    //   this.status = 'Error logging in manually';
    // }

    // window.alert(
    //   `form submitted. Name: ${login.form.value.name}, Email: ${login.form.value.email}`
    // );
  }
}
