import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
  HTTP_INTERCEPTORS,
  HttpHeaders
} from '@angular/common/http';
import { Observable, catchError, exhaustMap, switchMap, take, throwError } from 'rxjs';
import { AuthenticationService } from '../services/AuthService/authentication.service';
import { EventData } from '../services/_shared/EventData ';
import { EventBusService } from '../services/_shared/EventBusService ';
import { StorageService } from '../services/AuthService/storage.service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Injectable()
export class AuthInterceptorInterceptor implements HttpInterceptor {
  constructor(private authService: AuthenticationService, private cookie: CookieService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler) {
    const userData = localStorage.getItem('userData');

    if (userData) {
      let objectData = JSON.parse(userData);
      console.log(objectData);
      console.log(objectData.token);
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${objectData.token}`
        }
      });
    }

    return next.handle(request).pipe(
      catchError((error) => {
        // Catch "401 Unauthorized" responses
        if (error instanceof HttpErrorResponse && error.status === 401) {
          // Check if the error is due to an expired token
          if (error.error && error.error.message === 'Token expired') {
            // Call the method to get the refresh token from the backend
            this.authService.refreshToken().subscribe((response) => {
              const refreshToken = response.data;
              // Send the refresh token to the backend to get a new access token
              
                // Update the token in the local storage
                const newtoken = refreshToken;
                localStorage.setItem('userData', JSON.stringify(newtoken));

                // Retry the failed request with the new token
                request = request.clone({
                  setHeaders: {
                    Authorization: `Bearer ${newtoken}`
                  }
                });
                return next.handle(request);
              }, (refreshError) => {
                // Refresh token request failed, log out the user
                this.authService.logout();
              
            });
          } else {
            // Token is invalid, log out the user
            Swal.fire({
              title: "sesion time out!",
              icon: "error",
              confirmButtonText: "ok",
              showCloseButton: true,
            }).then(res => {
              this.authService.logout();
            });
          }
        }

        return throwError(error);
      })
    );
  }
}