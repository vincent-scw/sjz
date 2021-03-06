import { Component, Output, EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Timeline, PeriodGroupLevel } from './models/timeline.model';
import { TimelineService } from './services/timeline.service';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { MomentEditorComponent } from './areas/moment/moment-editor/moment-editor.component';
import { User } from './models/user.model';
import { TimelineEditorComponent } from './areas/timeline/timeline-editor/timeline-editor.component';

@Component({
	selector: 'app-navbar',
	templateUrl: './navbar.component.html'
})

export class NavbarComponent implements OnInit, OnDestroy {
	timelines$: Observable<Timeline[]>;
	editable: boolean;
	activeTopicKey: string;
	burgerActive: boolean;
	years: number[] = new Array<number>();

	public userData: User = { sub: '', name: '' };
	private userDataSubscription: Subscription;

	private isAuthorizedSubscription: Subscription = new Subscription();
	public isAuthorized = false;

	private timelineSub: Subscription;
	private editableSub: Subscription;

	constructor(private timelineService: TimelineService,
		private authSvc: AuthService,
		private dialog: MatDialog) {
		this.isAuthorizedSubscription = authSvc.isAuthorized$.subscribe(
			(isAuthorized: boolean) => {
				this.isAuthorized = isAuthorized;
			});

		this.userDataSubscription = authSvc.userData$.subscribe(userData => {
			this.userData = userData;
			this.refresh();
		});

		this.getYears();
	}

	ngOnInit() {
		this.authSvc.initAuth();
		this.timelineSub = this.timelineService.activeTimeline$.subscribe(t => this.activeTopicKey = t.timelineId);
		this.editableSub = this.authSvc.isAuthorized$.subscribe(l => this.editable = l);
	}

	ngOnDestroy() {
		if (!!this.timelineSub) { this.timelineSub.unsubscribe(); }
		if (!!this.editableSub) { this.editableSub.unsubscribe(); }
		if (!!this.isAuthorizedSubscription) { this.isAuthorizedSubscription.unsubscribe(); }
		if (!!this.userDataSubscription) { this.userDataSubscription.unsubscribe(); }
	}

	refresh() {
		this.timelines$ = this.timelineService.getTimelines(this.userData.sub);
	}

	getYears() {
		const today = new Date();
		for (let i = 2017; i <= today.getFullYear(); i++) {
			this.years.push(i);
		}
	}

	login() {
		this.authSvc.login();
	}

	logout() {
		this.authSvc.logout();
	}

	createNew() {
		this.dialog.open(TimelineEditorComponent, { data: { periodGroupLevel: PeriodGroupLevel.byDay } })
			.afterClosed().toPromise().then(() => this.refresh());
	}
}
