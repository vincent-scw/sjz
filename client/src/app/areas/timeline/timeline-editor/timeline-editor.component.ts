import { Component, OnInit, OnDestroy, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { Timeline } from '../../../models/timeline.model';
import { TimelineService } from '../../../services/timeline.service';


@Component({
  selector: 'app-timeline-editor',
  templateUrl: './timeline-editor.component.html'
})
export class TimelineEditorComponent implements OnInit {
  constructor(
    private timelineService: TimelineService,
    private dialogRef: MatDialogRef<TimelineEditorComponent>,
    @Inject(MAT_DIALOG_DATA) public model: Timeline) { }

  ngOnInit() {

  }

  onSubmit(value: Timeline) {
    this.timelineService.insertOrReplaceTimeline(value).toPromise()
      .then(t => this.dialogRef.close(true));
  }

  onCancel() {
    this.dialogRef.close(false);
  }
}
