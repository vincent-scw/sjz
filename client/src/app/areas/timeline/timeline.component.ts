import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { Title } from '@angular/platform-browser';
import { Observable ,  Subscription ,  BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { TimelineService } from '../../services/timeline.service';
import { Record, GroupedRecords } from '../../models/record.model';
import { RecordEditorComponent } from './record-editor/record-editor.component';
import { Timeline, PeriodGroupLevel } from '../../models/timeline.model';
import { AuthService } from '../../services/auth.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-timeline',
  templateUrl: './timeline.component.html',
  providers: [DatePipe]
})
export class TimelineComponent implements OnInit, OnDestroy {
  timeline: Timeline;
  groupedRecords: GroupedRecords[];
  loaded: boolean;
  editable: boolean;
  align: number = -1;

  private topicKey: string;
  private timelineSubscription: Subscription;
  private momentsSubscription: Subscription;
  private editableSub: Subscription;
  private routeSub: Subscription;

  private timeline$: Observable<Timeline>;

  constructor(private timelineService: TimelineService,
    private authSvc: AuthService,
    private dialog: MatDialog,
    private activatedRoute: ActivatedRoute,
    private title: Title,
    private router: Router,
    private datePipe: DatePipe) { 
    this.groupedRecords = new Array();
    this.loaded = false;
  }

  ngOnInit() {
    this.routeSub = this.activatedRoute.paramMap.subscribe((params) => {
      this.topicKey = params.get('timeline');
      this.timeline$ = this.timelineService.getTimeline(this.topicKey);
      this.refresh();
    })

    this.editableSub = this.authSvc.isAuthorized$.subscribe(l => this.editable = l);
  }

  ngOnDestroy() {
    if (!!this.timelineSubscription) { this.timelineSubscription.unsubscribe(); }
    if (!!this.momentsSubscription) { this.momentsSubscription.unsubscribe(); }
    if (!!this.editableSub) { this.editableSub.unsubscribe(); }
    if (!!this.routeSub) { this.routeSub.unsubscribe(); }
  }

  refresh() {
    this.timelineSubscription = this.timeline$.subscribe((t) => {
      this.groupedRecords = new Array();
      this.loaded = false;

      this.timeline = t;
      this.timelineService.activeTimeline = this.timeline;
      this.title.setTitle(`${t.title} | 时间轴`);

      t.items && t.items.forEach(i => {
        this.groupByLevel(this.timeline.periodGroupLevel, i);
      });

      this.loaded = true;
    });
  }

  onDelete(recordId: string) {
    this.timelineService.deleteRecord(this.timeline.id, recordId)
      .toPromise().then(() => this.refresh());
  }

  groupByLevel(level: PeriodGroupLevel, m: Record) {
    const date = new Date(m.date);
    let groupKey: string;
    switch (level) {
      case PeriodGroupLevel.byDay:
        groupKey = this.datePipe.transform(date, 'yyyy-MM-dd');
        break;
      case PeriodGroupLevel.byMonth:
        groupKey = this.datePipe.transform(date, 'yyyy-MM');
        break;
      case PeriodGroupLevel.byYear:
        groupKey = this.datePipe.transform(date, 'yyyy');
        break;
    }

    const grouped = this.groupedRecords.find(g => g.group == groupKey);
    if (grouped == null) {
      this.groupedRecords.push({ group: groupKey, records: [m] });
    } else {
      grouped.records.push(m);
    }
  }

  onEditTimelineClicked() {
    this.router.navigateByUrl(`timeline/${this.timeline.id}/edit`);
  }

  onDeleteTimelineClicked() {
    if (confirm('Are you sure to delete it?')) {
      this.timelineService.deleteTimeline(this.timeline.id).toPromise();
    }
  }

  onAddMomentClicked() {
    this.dialog.open(RecordEditorComponent, { data: { topicKey: this.timeline.id } })
      .afterClosed().toPromise().then(() => this.refresh());
  }
}
