import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  template: `
    <div>
      <a (click)="logout()">Logout
      </a>
    </div>
  `
})
export class LoginComponent {
  constructor(private authService: AuthService) {
    
  }

  logout() {
    this.authService.logout();
  }
}