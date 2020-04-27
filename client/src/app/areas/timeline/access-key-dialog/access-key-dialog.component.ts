import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Timeline } from '../../../models/timeline.model';
import { TimelineService } from '../../../services/timeline.service';

@Component({
  templateUrl: './access-key-dialog.component.html'
})
export class AccessKeyDialogComponent implements OnInit {
  model: Timeline;
  verificationSucceed: boolean;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Timeline,
    private dialogRef: MatDialogRef<AccessKeyDialogComponent>,
    private timelineService: TimelineService) {
    this.verificationSucceed = true;
  }

  ngOnInit() {
    this.model = this.data;
  }

  async onSubmit(data: Timeline) {
    this.verificationSucceed = await this.timelineService.verifyAccessCode(data).toPromise();
    if (this.verificationSucceed) {
      this.dialogRef.close(true);
    }
  }

  onCancel() {
    this.dialogRef.close(false);
  }
}