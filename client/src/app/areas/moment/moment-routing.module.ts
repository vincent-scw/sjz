import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "../../services/auth-guard.service";
import { MomentEditorComponent } from "./moment-editor/moment-editor.component";
import { MomentViewComponent } from "./moment-view/moment-view.component";
import { NgModule } from "@angular/core";

const recordRoutes: Routes = [
  {
    path: 'records',
    children: [

      {
        path: ':year',
        component: MomentViewComponent
      },
      {
        path: 'create',
        component: MomentEditorComponent,
        canActivate: [AuthGuard]
      },
      {
        path: ':record/edit',
        component: MomentEditorComponent,
        canActivate: [AuthGuard]
      },
      
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(recordRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class MomentRoutingModule { }