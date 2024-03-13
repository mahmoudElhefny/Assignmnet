import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthesponseData } from 'src/app/ViewModels/AuthesponseData';
import { ApiResponse } from 'src/app/ViewModels/Response';
import { AuthenticationService } from 'src/app/services/AuthService/authentication.service';
import { StorageService } from 'src/app/services/AuthService/storage.service';
import Swal from 'sweetalert2';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class AppSideLoginComponent {
  loginForm: FormGroup;
  UserNameerrorMessage: any;
  PassworderrorMessage: any;
  UnkownerrorMessage: any;
  isLoggedIn = false;
  isLoginFailed = false;
  isloading = false;
  error: string = '';
  

  constructor(
    private authService: AuthenticationService,
    private FB: FormBuilder,
    private router: Router,
    private storageService:StorageService
    
  ) {
   this.loginForm=FB.group({
    userName: ['', [Validators.required]],
    password: ['', [Validators.required]]
   })
  }
  get userName() {
    return this.loginForm.get('userName');
  }
  get password() {
    return this.loginForm.get('password');
  }
  Login() {
    
    let authObs: Observable<ApiResponse<AuthesponseData>>;
    authObs = this.authService.login(this.loginForm.value);
    
    authObs.subscribe(
      (response) => {
        if(response.isSucceded){
          this.isLoggedIn = true;
          this.isloading = false;
          this.UserNameerrorMessage = '';
          this.PassworderrorMessage = '';
          
          this.storageService.saveUser(JSON.stringify(response.data))
        
          this.router.navigateByUrl('/dashboard');
        }
        else{
          this.UnkownerrorMessage=response.errors[0]
          
        }       
      },
      (error: HttpErrorResponse) => {
        this.isloading = false;
        if (error.error instanceof ErrorEvent) {
          // Client-side error occurred
          this.UnkownerrorMessage = 'An error occurred: ' + error.error.message;
        } else {
          // Backend error occurred
          this.UnkownerrorMessage = 'Backend returned status code: ' + error.status + ', error message: ' + error.error;
        }
      }
    );
    
  }
}