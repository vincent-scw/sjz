import { Component } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  template: `
    <div>
      <a (click)="linkedInLogin()">
        <img src="/assets/linkedin/Sign-In-Large-Default.png" />
      </a>
      <a (click)="logout()">Logout
      </a>
    </div>
  `
})
export class LoginComponent {
  constructor(private authService: AuthService) {
    
  }

  linkedInLogin() {
    AuthService.getProvider('linkedin', this.authService).signIn();
  }

  logout() {
    this.authService.logout();
  }
}