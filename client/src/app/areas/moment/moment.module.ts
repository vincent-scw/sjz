import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared.module';
import { MomentViewComponent } from './moment-view/moment-view.component';
import { MomentEditorComponent } from './moment-editor/moment-editor.component';
import { MomentRoutingModule } from './moment-routing.module';

@NgModule({
  imports: [
    SharedModule,
    MomentRoutingModule
  ],
  declarations: [MomentViewComponent, MomentEditorComponent],
  entryComponents: [
    MomentEditorComponent
  ]
})
export class MomentModule { }
