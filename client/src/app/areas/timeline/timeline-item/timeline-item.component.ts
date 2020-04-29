import { Component, Input } from '@angular/core';
import { Record } from '../../../models/record.model';

@Component({
  selector: 'app-timeline-item',
  templateUrl: './timeline-item.component.html'
})
export class TimelineItemComponent {
  @Input() record: Record;
  @Input() editable: boolean;
}