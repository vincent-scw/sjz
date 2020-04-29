import { Component, OnInit, OnDestroy, Input, Output, EventEmitter, Inject, Injector } from '@angular/core';
import { Record } from '../../../models/record.model';
import { TimelineService } from '../../../services/timeline.service';
import { Subscription } from 'rxjs';
import { Timeline } from '../../../models/timeline.model';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
	selector: 'app-record-editor',
	templateUrl: './record-editor.component.html'
})

export class RecordEditorComponent implements OnInit, OnDestroy {
	@Input() model: Record = { recordId: '', date: new Date() };
	@Output() complete: EventEmitter<boolean> = new EventEmitter<boolean>();
	timeline: Timeline;
	public editorConfig = {
		"editable": true,
		"spellcheck": true,
		"minHeight": "200px",
		"width": "auto",
		"translate": "yes",
		"enableToolbar": true,
		"showToolbar": true,
		"placeholder": "Enter text here...",
		"imageEndPoint": `${location.href.substring(0, location.href.indexOf(location.pathname))}/api/images/upload`,
		"toolbar": [
			["bold", "italic"],
			["removeFormat", "undo", "redo"],
			["link", "unlink", "image"]
		]
	};

	private timelineSub: Subscription;
	private dialogRef: MatDialogRef<RecordEditorComponent>;

	constructor(
		private injector: Injector,
		private service: TimelineService) {
		this.dialogRef = injector.get(MatDialogRef, null);
	}

	ngOnInit() {
		this.timelineSub = this.service.activeTimeline$.subscribe(t => {
			this.timeline = t;
		});
	}

	ngOnDestroy() {
		if (!!this.timelineSub) { this.timelineSub.unsubscribe(); }
	}

	onSubmit(newData: Record) {
		this.service.insertOrReplaceRecord(this.timeline.timelineId, newData).toPromise()
			.then(() => {
				this.complete.emit(true);
				this.dialogRef && this.dialogRef.close();
			});
	}

	onCancel() {
		this.complete.emit(false);
		this.dialogRef && this.dialogRef.close();
	}
}
