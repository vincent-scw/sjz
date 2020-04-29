import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Record } from '../../../models/record.model';

@Component({
  selector: 'app-timeline-item',
  templateUrl: './timeline-item.component.html'
})
export class TimelineItemComponent {
  @Input() record: Record;
  @Input() editable: boolean;
  @Output() delete: EventEmitter<string> = new EventEmitter<string>();

  isReadMode: boolean = true;
}