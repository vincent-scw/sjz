import { Component, OnInit, OnDestroy } from "@angular/core";
import { Observable ,  Subscription } from "rxjs";
import { ImageService } from "../services/image.service";
import { Timeline } from "../models/timeline.model";
import { TimelineService } from "../services/timeline.service";

@Component({
  selector: 'app-image-manager',
  templateUrl: './image-manager.component.html',
  styleUrls: ['./image-manager.component.css']
})
export class ImageManagerComponent implements OnInit, OnDestroy {
  column1: string[] = new Array();
  column2: string[] = new Array();
  column3: string[] = new Array();

  timelineSelected: string = '';
  timelines$: Observable<Timeline[]>;

  private imagesSub: Subscription;

  constructor(private imageService: ImageService,
    private timelineService: TimelineService) {

  }

  ngOnInit() {
    this.timelines$ = this.timelineService.getTimelines();

    this.retreiveImages();
  }

  ngOnDestroy() {
    if (!!this.imagesSub) { this.imagesSub.unsubscribe(); }
  }

  retreiveImages() {
    this.imagesSub = this.imageService.getImageUrls(this.timelineSelected == '' ? null : this.timelineSelected)
      .subscribe(imgs => {
        this.column1 = new Array();
        this.column2 = new Array();
        this.column3 = new Array();
        for (let i = 0; i < imgs.length; i++) {
          if (i % 3 == 0) { this.column1.push(imgs[i]); }
          else if (i % 3 == 1) { this.column2.push(imgs[i]); }
          else { this.column3.push(imgs[i]); }
        }
      });
  }

  timelineChanged() {
    this.retreiveImages();
  }
}