import { NgModule } from '@angular/core';
import { SharedModule } from '../shared.module';
import { RecordViewComponent } from './record-view/record-view.component';
import { RecordEditorComponent } from './record-editor/record-editor.component';
import { RecordRoutingModule } from './record-routing.module';

@NgModule({
  imports: [
    SharedModule,
    RecordRoutingModule
  ],
  declarations: [RecordViewComponent, RecordEditorComponent],
  entryComponents: [
    RecordEditorComponent
  ]
})
export class RecordModule { }
