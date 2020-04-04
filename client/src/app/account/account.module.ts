import { NgModule } from '@angular/core';
import { SharedModule } from '../shared.module';
import { AccountRoutingModule } from './account-routing.module';
import { AuthCallbackComponent } from './auth-callback.component';
import { LoginComponent } from './login.component';

@NgModule({
  imports: [
    SharedModule,
    AccountRoutingModule
  ],
  declarations: [
    AuthCallbackComponent,
    LoginComponent
  ]
})
export class AccountModule {
  
}