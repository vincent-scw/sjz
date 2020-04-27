import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { TimelineService } from './timeline.service';
import { ProtectLevel } from '../models/timeline.model';
import { AccessKeyDialogComponent } from '../timeline/access-key-dialog/access-key-dialog.component';

@Injectable()
export class TimelineAccessGuard implements CanActivate {
  constructor(private timelineService: TimelineService,
    private router: Router,
    private dialog: MatDialog) {

  }

  async canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> {
    const topicKey = route.params.timeline;
    if (topicKey != null) {
      if (!(await this.CheckTimelineCanAccess(topicKey))) {
        this.router.navigate(['']);
        return false;
      }
    }
    return true;
  }

  private async CheckTimelineCanAccess(key: string): Promise<boolean> {
    return true;
    // const timeline = await this.timelineService.getTimeline(key).toPromise();
    // if (timeline.protectLevel == ProtectLevel.public) {
    //   return true;
    // }

    // const dialogRef = this.dialog.open(AccessKeyDialogComponent, { data: { topicKey: key } });
    // return await dialogRef.afterClosed().toPromise<boolean>();
  }
}