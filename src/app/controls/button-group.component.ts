import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'button-group',
  templateUrl: './button-group.component.html'
})

export class ButtonGroupComponent {
  @Input() isValid = false;
  @Output() submit = new EventEmitter();
  @Output() cancel = new EventEmitter();

  onSubmit() {
    if (this.isValid) {
      this.submit.emit();
    }
  }

  onCancel() {
    this.cancel.emit();
  }
}
