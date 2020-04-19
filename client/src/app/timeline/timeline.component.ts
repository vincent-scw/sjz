import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { MatDialog } from '@angular/material';
import { Title } from '@angular/platform-browser';
import { Observable ,  Subscription ,  BehaviorSubject } from 'rxjs';
import { switchMap } from 'rxjs/operators';

import { TimelineService } from '../services/timeline.service';
import { Moment, GroupedMoments } from '../models/moment.model';
import { MomentEditorComponent } from './moment-editor/moment-editor.component';
import { Timeline, PeriodGroupLevel } from '../models/timeline.model';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-timeline',
  templateUrl: './timeline.component.html'
})
export class TimelineComponent implements OnInit, OnDestroy {
  timeline: Timeline;
  groupedMoments: GroupedMoments[];
  loaded: boolean;
  editable: boolean;
  align: number = -1;

  private timelineSubscription: Subscription;
  private momentsSubscription: Subscription;
  private editableSub: Subscription;

  private timeline$: Observable<Timeline>;

  constructor(private timelineService: TimelineService,
    private authSvc: AuthService,
    private dialog: MatDialog,
    private activatedRoute: ActivatedRoute,
    private title: Title,
    private router: Router) { 
    this.groupedMoments = new Array();
    this.loaded = false;
  }

  ngOnInit() {
    this.timeline$ = this.activatedRoute.paramMap.pipe(
      switchMap((params: ParamMap) => this.timelineService.getTimeline(params.get('timeline')))
    );
    
    this.timelineSubscription = this.timeline$.subscribe((t) => {
      this.groupedMoments = new Array();
      this.loaded = false;
      
      this.timeline = t;
      this.timelineService.activeTimeline = this.timeline;
      this.title.setTitle(`${t.title} | 时间轴`);
      this.momentsSubscription = this.timelineService.getMoments(t.topicKey).subscribe(x => {
        x.map((m) => {
          this.groupByLevel(this.timeline.periodGroupLevel, m);
        });
        this.loaded = true;
      });
    });

    this.editableSub = this.authSvc.isLoggedIn$.subscribe(l => this.editable = l);
  }

  ngOnDestroy() {
    if (!!this.timelineSubscription) { this.timelineSubscription.unsubscribe(); }
    if (!!this.momentsSubscription) { this.momentsSubscription.unsubscribe(); }
    if (!!this.editableSub) { this.editableSub.unsubscribe(); }
  }

  onEdit(moment: Moment) {
    this.dialog.open(MomentEditorComponent, {data: moment});
  }

  onDelete(moment: Moment) {
    this.timelineService.deleteMoment(moment.topicKey, moment.recordDate).toPromise();
  }

  groupByLevel(level: PeriodGroupLevel, m: Moment) {
    const date = new Date(m.recordDate);
    let groupKey: string;
    switch (level) {
      case PeriodGroupLevel.byDay:
        groupKey = date.toLocaleDateString('en-US', { year: 'numeric', month: 'short', day: 'numeric' });  
        break;
      case PeriodGroupLevel.byMonth:
        groupKey = date.toLocaleDateString('en-US', { year: 'numeric', month: 'short' });
        break;
      case PeriodGroupLevel.byYear:
        groupKey = date.toLocaleDateString('en-US', { year: 'numeric' });  
        break;
    }

    const grouped = this.groupedMoments.find(g => g.group == groupKey);
    if (grouped == null) {
      this.groupedMoments.push({ group: groupKey, moments: [m] });
    } else {
      grouped.moments.push(m);
    }
  }

  onEditTimelineClicked() {
    this.router.navigateByUrl(`timeline/${this.timeline.topicKey}/edit`);
  }

  onDeleteTimelineClicked() {
    if (confirm('确定要删除吗？')) {
      this.timelineService.deleteTimeline(this.timeline.topicKey).toPromise();
    }
  }

  onAddMomentClicked() {
    this.dialog.open(MomentEditorComponent, { data: { topicKey: this.timeline.topicKey } });
  }
}
