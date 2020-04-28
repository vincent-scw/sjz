import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-action-panel',
  templateUrl: './action-panel.component.html'
})
export class ActionPanelComponent {
  @Output() add = new EventEmitter<void>();
  @Output() edit = new EventEmitter<void>();
  @Output() delete = new EventEmitter<void>();
}