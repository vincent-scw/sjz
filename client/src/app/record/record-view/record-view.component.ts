import { Component, OnInit, OnDestroy } from '@angular/core';
import { RecordService } from '../../services/record.service';
import { Record } from '../../models/record.model';
import { Observable ,  Subscription } from 'rxjs';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { switchMap } from 'rxjs/operators';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-record-view',
  templateUrl: './record-view.component.html',
  styles: [
    `.masonry-item { 
      width: 400px; 
      margin-bottom: 5px;
    }`
  ],
  providers: [DatePipe]
})
export class RecordViewComponent implements OnInit, OnDestroy {
  year: number;
  editable: boolean;

  images: any[];

  private records$: Observable<Record[]>;
  private recordsSub: Subscription;
  
  constructor(private recordService: RecordService,
    private activatedRoute: ActivatedRoute,
    private title: Title,
    private datePipe: DatePipe) { }

  ngOnInit() {
    this.records$ = this.activatedRoute.paramMap.pipe(
      switchMap((params: ParamMap) => {
        this.year = parseInt(params.get('year'));
        return this.recordService.getRecords(this.year);
      })
    );

    this.recordsSub = this.records$.subscribe((r) => {
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
    if (!!this.recordsSub) { this.recordsSub.unsubscribe(); }
  }
}
