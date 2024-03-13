import { NgFor } from '@angular/common';
import { Component, ElementRef } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { passwordmatch } from 'src/app/CustomValidators/passwordmatch.validtor';
import { errorMessag } from 'src/app/ViewModels/AuthesponseData';
import { AuthenticationService } from 'src/app/services/AuthService/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})

export class AppSideRegisterComponent {
 
  registerForm:FormGroup
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessages:any[];
  constructor(private authService: AuthenticationService,
    private FB: FormBuilder,
    private router: Router,
    private completeModal: ElementRef) {
      this.registerForm=FB.group({
        emailAddress:['', [Validators.required]],
        userName: ['', [Validators.required]],
        password: ['', [Validators.required]],
        confirmPassword:['', [Validators.required]]
       },{validators:[passwordmatch]})
    }
    get userName() {
      return this.registerForm.get('userName');
    }
    get password() {
      return this.registerForm.get('password');
    }
    get emailAddress(){
      return this.registerForm.get('emailAddress');
    }
    get confirmPassword(){
      return this.registerForm.get('confirmPassword');
    }

  Register(){
    
    this.authService.register(this.registerForm.value).subscribe(
      response => {
     
        if (response.isSucceded) {
          this.isSignUpFailed = false;
          this.router.navigateByUrl('/dashboard');
        } else {
          response.errors.forEach(element => {
            this.errorMessages.push(element);
          });
          this.errorMessages = response.errors;
        }
      },
      error => {
        this.errorMessages = error.error.errors
        console.log(this.errorMessages);
        // Handle error occurred during the HTTP request
      }
    );
}
}

