import { Routes, RouterModule } from "@angular/router";

import { NgModule } from "@angular/core";
import { AuthCallbackComponent } from "./auth-callback.component";
import { LoginComponent } from "./login.component";

const routes: Routes = [
  { path: 'oauth2/:type', component: AuthCallbackComponent },
  {
    path: 'account',
    children: [
      { path: 'login', component: LoginComponent }
    ]
  }
]

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AccountRoutingModule {

}