import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import { ButtonGroupComponent } from './button-group.component';
import { ImageViewerComponent } from './image-viewer.component';

@NgModule({
  imports: [
    ReactiveFormsModule
  ],
  declarations: [
    ButtonGroupComponent,
    ImageViewerComponent
  ],
  exports: [
    ButtonGroupComponent,
    ImageViewerComponent
  ],
  entryComponents: [
    ImageViewerComponent
  ]
})
export class ControlsModule {}
