
import {filter} from 'rxjs/operators';
import { Component, OnInit, AfterViewChecked, HostListener, ElementRef } from '@angular/core';
import { Router, NavigationEnd, RouterEvent } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { MatDialog } from '@angular/material/dialog';

import { ImageViewerComponent } from './controls/image-viewer.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit, AfterViewChecked {
  tocMaxHeight: string;
  private tocMaxHeightOffset = 0;

  constructor(private router: Router,
    private title: Title,
    private hostElement: ElementRef,
    private dialog: MatDialog) {

  }

  ngOnInit() {
    this.router.events.pipe(filter((event) => event instanceof NavigationEnd))
      .subscribe((event: RouterEvent) => {
        this.title.setTitle('时间轴');
      });
  }

  ngAfterViewChecked() {
    // const el = this.hostElement.nativeElement as Element;
    // const images = el.querySelectorAll('img');
    // for (let i = 0; i < images.length; i++) {
    //   let ele = images.item(i);
    //   ele.addEventListener('click', () => {
    //     this.dialog.open(ImageViewerComponent, { data: ele.src });
    //   });
    // }
  }

  // Dynamically change height of table of contents container
  @HostListener('window:scroll')
  onScroll() {
    if (!this.tocMaxHeightOffset) {
      // Must wait until now for mat-toolbar to be measurable.
      const el = this.hostElement.nativeElement as Element;
      this.tocMaxHeightOffset =
        el.querySelector('app-navbar')!.clientHeight +
        24; //  fudge margin
    }

    this.tocMaxHeight = (document.body.scrollHeight - window.pageYOffset - this.tocMaxHeightOffset).toFixed(2);
  }

  // Restrain scrolling inside an element, when the cursor is over it
  restrainScrolling(evt: WheelEvent) {
    const elem = evt.currentTarget as Element;
    const scrollTop = elem.scrollTop;

    if (evt.deltaY < 0) {
      // Trying to scroll up: Prevent scrolling if already at the top.
      if (scrollTop < 1) {
        evt.preventDefault();
      }
    } else {
      // Trying to scroll down: Prevent scrolling if already at the bottom.
      const maxScrollTop = elem.scrollHeight - elem.clientHeight;
      if (maxScrollTop - scrollTop < 1) {
        evt.preventDefault();
      }
    }
  }
}
