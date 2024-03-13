import { Component } from "@angular/core";
import { Subscription } from "rxjs";
import { AuthenticationService } from "./services/AuthService/authentication.service";
import { EventBusService } from "./services/_shared/EventBusService ";
import { StorageService } from "./services/AuthService/storage.service";

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    //styleUrls: ['./app.component.css']
  })
  export class AppComponent {
    private roles: string[] = [];
    isLoggedIn = false;
    showAdminBoard = false;
    showModeratorBoard = false;
    username?: string;
  
    eventBusSub?: Subscription;
  
    constructor(
      private storageService: StorageService,
      private authService: AuthenticationService,
      private eventBusService: EventBusService
    ) {}
  
    ngOnInit(): void {
      this.isLoggedIn = this.storageService.isLoggedIn();
  
      if (this.isLoggedIn) {
        const user = this.storageService.getUser();
        this.roles = user.roles;
  
        this.showAdminBoard = this.roles.includes('ROLE_ADMIN');
        this.showModeratorBoard = this.roles.includes('ROLE_MODERATOR');
  
        this.username = user.username;
      }
  
      this.eventBusSub = this.eventBusService.on('logout', () => {
        this.logout();
      });
    }
  
    logout(): void {
      this.authService.logout()
    }
  }