import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { BehaviorSubject } from 'rxjs';
import { userToken } from 'src/app/pages/authentication/userToken';
const USER_KEY = 'userData';
const refreshTokenKey = 'refreshToken';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  usertoken = new BehaviorSubject<userToken | null>(null)
  constructor(private cookieService: CookieService) { }
  clean(): void {
    localStorage.clear();
  }

  public saveUser(response: any): void {
    //const user=new userToken(response.data.roles,response.data.token,response.data.expiration)
   // this.usertoken.next(user)
    console.log('after ogin vaue',this.usertoken.value)
    //localStorage.setItem('userData',JSON.stringify(user))
    localStorage.setItem('userData',response)
  }
  public getUser(): any {
    const user =this.cookieService.get(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }

    return {};
  }
  public refreshToken(): any {
    const user =this.cookieService.get(refreshTokenKey);
    if (user) {
      return JSON.parse(user);
    }

    return {};
  }
  public isLoggedIn(): boolean {
    const user = this.cookieService.get(USER_KEY);
    if (user) {
      return true;
    }

    return false;
  }
}
