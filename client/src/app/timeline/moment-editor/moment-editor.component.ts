import { Component, OnInit, OnDestroy, Input, Output, EventEmitter, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Moment } from '../../models/moment.model';
import { TimelineService } from '../../services/timeline.service';

import { environment } from '../../../environments/environment';
import { Subscription } from 'rxjs';
import { Timeline } from '../../models/timeline.model';
import { DatePipe } from '@angular/common';

@Component({
	selector: 'app-moment-editor',
	templateUrl: './moment-editor.component.html'
})

export class MomentEditorComponent implements OnInit, OnDestroy {
	model: Moment = { topicKey: '', recordDate: new Date() };
	timeline: Timeline;
	editorConfig = {
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
			["bold", "italic", "underline"],
			["fontName", "fontSize", "color"],
			["indent", "outdent"],
			["cut", "copy", "delete", "removeFormat", "undo", "redo"],
			["paragraph", "blockquote", "removeBlockquote", "horizontalLine"],
			["link", "unlink", "image", "video"]
		]
	};

	private timelineSub: Subscription;

	constructor(
		@Inject(MAT_DIALOG_DATA) public data: Moment,
		private dialogRef: MatDialogRef<MomentEditorComponent>,
		private service: TimelineService) {

	}

	ngOnInit() {
		if (this.data != null) {
			this.model = this.data;
		}
		this.timelineSub = this.service.activeTimeline$.subscribe(t => {
			this.timeline = t;
			this.editorConfig.imageEndPoint = this.editorConfig.imageEndPoint + `?folder=${t.topicKey}`;
		});
	}

	ngOnDestroy() {
		if (!!this.timelineSub) { this.timelineSub.unsubscribe(); }
	}

	onSubmit(newData: Moment) {
		this.service.insertOrReplaceMoment(newData).toPromise()
			.then((moment) => this.dialogRef.close());
	}

	onCancel() {
		this.dialogRef.close();
	}
}
