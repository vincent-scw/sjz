import { Component, Input } from "@angular/core";

@Component({
  selector: 'app-image-box',
  templateUrl: './image-box.component.html'
})
export class ImageBoxComponent {
  @Input() src: string;
}