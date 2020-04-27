import { Component, OnInit, OnDestroy } from '@angular/core';
import { MomentService } from '../../../services/moment.service';
import { Moment } from '../../../models/moment.model';
import { Observable ,  Subscription } from 'rxjs';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { switchMap } from 'rxjs/operators';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-moment-view',
  templateUrl: './moment-view.component.html',
  styles: [
    `.masonry-item { 
      width: 400px; 
      margin-bottom: 5px;
    }`
  ],
  providers: [DatePipe]
})
export class MomentViewComponent implements OnInit, OnDestroy {
  year: number;
  editable: boolean;

  images: any[];

  private moments$: Observable<Moment[]>;
  private momentsSub: Subscription;
  
  constructor(private momentService: MomentService,
    private activatedRoute: ActivatedRoute,
    private title: Title,
    private datePipe: DatePipe) { }

  ngOnInit() {
    this.moments$ = this.activatedRoute.paramMap.pipe(
      switchMap((params: ParamMap) => {
        this.year = parseInt(params.get('year'));
        return this.momentService.getRecords(this.year);
      })
    );

    this.momentsSub = this.moments$.subscribe((r) => {
      this.images = r.map(e => {
        return {
          url: e.imageUrl,
          thumbnailUrl: e.thumbnailUrl,
          title: `${this.datePipe.transform(e.date, 'yyyy/MM/dd')} ${e.title} @ ${e.location}`
        }
      });
      this.title.setTitle(`刻 ${this.year} | 时间轴`);
    });
  }

  ngOnDestroy() {
    if (!!this.momentsSub) { this.momentsSub.unsubscribe(); }
  }
}
