import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-confirmation-modal-component',
  templateUrl: './confirmation-modal-component.component.html',
  styleUrl: './confirmation-modal-component.component.css'
})
export class ConfirmationModalComponentComponent {
  display = input.required<boolean>();
  questionText = input.required<string>();
  close = output<void>();
  confirm = output<void>();

  onClose(){
    this.close.emit();
  }

  onConfirm(){
    this.onClose();
    this.confirm.emit();
  }
}


