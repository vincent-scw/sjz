import { Routes, RouterModule } from "@angular/router";

import { NgModule } from "@angular/core";
import { LoginComponent } from "./login.component";

const routes: Routes = [
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