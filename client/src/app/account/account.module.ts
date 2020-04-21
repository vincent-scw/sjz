import { NgModule } from '@angular/core';
import { SharedModule } from '../shared.module';
import { AccountRoutingModule } from './account-routing.module';
import { LoginComponent } from './login.component';

@NgModule({
  imports: [
    SharedModule,
    AccountRoutingModule
  ],
  declarations: [
    LoginComponent
  ]
})
export class AccountModule {
  
}