import { Component, Output, EventEmitter, OnInit, OnDestroy } from '@angular/core';
import { MomentEditorComponent } from './timeline/moment-editor/moment-editor.component';
import { Observable ,  Subscription } from 'rxjs';
import { Timeline } from './models/timeline.model';
import { TimelineService } from './services/timeline.service';
import { Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { MatDialog } from '@angular/material/dialog';
import { RecordEditorComponent } from './record/record-editor/record-editor.component';

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

	private timelineSub: Subscription;
	private editableSub: Subscription;

	constructor(private timelineService: TimelineService,
		private authSvc: AuthService,
		private dialog: MatDialog) {

		this.getYears();
	}

	ngOnInit() {
		this.authSvc.initAuth();
		this.timelines$ = this.timelineService.getTimelines();
		this.timelineSub = this.timelineService.activeTimeline$.subscribe(t => this.activeTopicKey = t.topicKey);
		this.editableSub = this.authSvc.isAuthorized$.subscribe(l => this.editable = l);
	}

	ngOnDestroy() {
		if (!!this.timelineSub) { this.timelineSub.unsubscribe(); }
		if (!!this.editableSub) { this.editableSub.unsubscribe(); }
	}

	async onAddNewRecordClicked() {
		const dialogRef = this.dialog.open(RecordEditorComponent);
		await dialogRef.afterClosed().toPromise();
	}

	getYears() {
		var today = new Date();
		for (var i = 2017; i <= today.getFullYear(); i++) {
			this.years.push(i);
		}
	}

	login() {
		this.authSvc.login();
	}

	logout() {
		this.authSvc.logout();
	}
}
