import { HttpClient, HttpErrorResponse, HttpHeaders, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable, catchError, tap, throwError } from 'rxjs';
import { AuthesponseData } from 'src/app/ViewModels/AuthesponseData';
import { IuserRegister } from 'src/app/ViewModels/IUserRegister';
import { IuserLogin } from 'src/app/ViewModels/IuserLogin';
import { ApiResponse } from 'src/app/ViewModels/Response';
import { StorageService } from './storage.service';
import { userToken } from 'src/app/pages/authentication/userToken';


@Injectable({
  providedIn: 'root'
})

export class AuthenticationService {
  // usertoken = new BehaviorSubject<userToken | null>(null)
  private baseUrl = 'https://localhost:7269/api/Authentication';
  constructor(private http: HttpClient, private router: Router, private storageService: StorageService) { }

  login(usermodel: IuserLogin): Observable<ApiResponse<AuthesponseData>> {
    const url = `${this.baseUrl}/Login`;
    return this.http.post<ApiResponse<AuthesponseData>>(url, usermodel).
       pipe(catchError(this.handleError))//, tap(response => {

      // }));
  }

  register(usermodel: IuserRegister): Observable<ApiResponse<AuthesponseData>> {
    const url = `${this.baseUrl}/register`;
    return this.http.post<ApiResponse<AuthesponseData>>(url, usermodel);
  }
  refreshToken():Observable<ApiResponse<string>> {
    return this.http.get<ApiResponse<string>>(`${this.baseUrl}/refreshToken`);
  }
  logout() {
    this.storageService.clean()
    this.router.navigateByUrl('/authentication/login')

  }
  private handleError(errorRes: HttpErrorResponse) {
    let errormessage = "unknown Error Ocurred"
    if (!errorRes.error) {
      return throwError(errormessage);
    }
        errormessage=errorRes.error
    return throwError(errormessage)
  }

}
