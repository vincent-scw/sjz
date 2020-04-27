import { Routes, RouterModule } from "@angular/router";

import { TimelineEditorComponent } from "./timeline-editor/timeline-editor.component";
import { AuthGuard } from "../../services/auth-guard.service";
import { TimelineComponent } from "./timeline.component";
import { TimelineAccessGuard } from "../../services/timeline-access-guard.service";
import { NgModule } from "@angular/core";

const timelineRoutes: Routes = [
  {
    path: 'timeline',
    children: [
      {
        path: 'create',
        component: TimelineEditorComponent,
        canActivate: [AuthGuard]
      },
      {
        path: ':timeline/edit',
        component: TimelineEditorComponent,
        canActivate: [AuthGuard]
      },
      {
        path: ':timeline',
        component: TimelineComponent,
        canActivate: [TimelineAccessGuard]
      }
    ]
  }
];

@NgModule({
  imports: [
    RouterModule.forChild(timelineRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class TimelineRoutingModule { }
