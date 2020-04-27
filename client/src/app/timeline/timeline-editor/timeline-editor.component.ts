import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { Timeline, ProtectLevel, PeriodGroupLevel } from '../../models/timeline.model';
import { TimelineService } from '../../services/timeline.service';


@Component({
  selector: 'app-timeline-editor',
  templateUrl: './timeline-editor.component.html'
})
export class TimelineEditorComponent implements OnInit {
  model: Timeline = {
    id: '',
    protectLevel: ProtectLevel.public,
    periodGroupLevel: PeriodGroupLevel.any,
    isCompleted: false,
    startTime: new Date()
  };
  constructor(private timelineService: TimelineService,
    private activatedRoute: ActivatedRoute) {  }

  ngOnInit() {
    const editEntity = this.activatedRoute.snapshot.paramMap.get('timeline');
    if (editEntity != null) {
      this.timelineService.getTimeline(editEntity)
        .toPromise().then(t => this.model = t);
    }
  }

  onSubmit(value: Timeline) {
    this.timelineService.insertOrReplaceTimeline(value).toPromise()
      .then(t => { alert('Done'); });
  }

  onCancel() {
    
  }
}
