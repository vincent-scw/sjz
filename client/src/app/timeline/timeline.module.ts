import { NgModule } from '@angular/core';
import { SharedModule } from '../shared.module';
import { TimelineRoutingModule } from './timeline-routing.module';

import { TimelineComponent } from '../timeline/timeline.component';
import { RecordEditorComponent } from './record-editor/record-editor.component';
import { TimelineEditorComponent } from './timeline-editor/timeline-editor.component';
import { TimelineAccessGuard } from '../services/timeline-access-guard.service';

@NgModule({
  imports: [
    SharedModule,
    TimelineRoutingModule
  ],
  declarations: [
    TimelineComponent,
    RecordEditorComponent,
    TimelineEditorComponent
  ],
  providers: [
    TimelineAccessGuard
  ],
  entryComponents: [
    RecordEditorComponent
  ]
})
export class TimelineModule { }