import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { ImageManagerComponent } from "./image-manager.component";
import { AuthGuard } from "../services/auth-guard.service";

const managementRoutes: Routes = [
  {
    path: 'manage',
    children: [
      {path: 'images', component: ImageManagerComponent, canActivate: [AuthGuard]}
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(managementRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class ManagementRoutingModule {}