import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";

@Component({
  template: `
  <div class="image-viewer">
    <img [src]="data">
  </div>
  `
})
export class ImageViewerComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public data: string,
    private dialogRef: MatDialogRef<ImageViewerComponent>) {
    
    }
}