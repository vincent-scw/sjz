import { Component, OnInit, OnDestroy, Input, Output, EventEmitter, Inject, Injector } from '@angular/core';
import { Record } from '../../../models/record.model';
import { TimelineService } from '../../../services/timeline.service';
import { Subscription } from 'rxjs';
import { Timeline } from '../../../models/timeline.model';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { environment } from '../../../../environments/environment';
import { AngularEditorConfig } from '@kolkov/angular-editor';

@Component({
	selector: 'app-record-editor',
	templateUrl: './record-editor.component.html'
})

export class RecordEditorComponent implements OnInit, OnDestroy {
	timeline: Timeline;
	public editorConfig: AngularEditorConfig = {
		editable: true,
		spellcheck: true,
		minHeight: '200px',
		width: 'auto',
		translate: 'yes',
		enableToolbar: true,
		showToolbar: true,
		sanitize: true,
		placeholder: 'Enter text here...',
		uploadUrl: `${environment.imageSvcUrl}/api/images/upload`,
		toolbarHiddenButtons: [
			['subscript',
				'superscript',
				'justifyLeft',
				'justifyCenter',
				'justifyRight',
				'justifyFull',
				'heading',
				'fontName'],
			['fontSize',
				'textColor',
				'backgroundColor',
				'customClasses',
				'insertHorizontalRule',
				'toggleEditorMode']
		]
	};

	private timelineSub: Subscription;

	constructor(
		private dialogRef: MatDialogRef<RecordEditorComponent>,
		private service: TimelineService,
		@Inject(MAT_DIALOG_DATA) public model: Record) {
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
				this.dialogRef.close(true);
			});
	}

	onCancel() {
		this.dialogRef.close(false);
	}
}
