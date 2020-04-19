import { Component, OnInit, OnDestroy } from '@angular/core';
import { TimelineService } from './services/timeline.service';
import { RecordService } from './services/record.service';
import { Subscription ,  Observable } from 'rxjs';
import { Timeline } from './models/timeline.model';

@Component({
  selector: 'app-main',
  templateUrl: 'main.component.html'
})
export class MainComponent implements OnInit, OnDestroy{
  timelines$: Observable<Timeline[]>;
  years: number[] = new Array<number>();
  
  constructor(private timelineService: TimelineService) {
    this.getYears();
  }

  ngOnInit() {
    this.timelines$ = this.timelineService.getTimelines();
  }

  ngOnDestroy() {

  }

  getYears() {
    var today = new Date();
    for (var i = 2017; i <= today.getFullYear(); i++) {
      this.years.push(i);
    }
  }
}