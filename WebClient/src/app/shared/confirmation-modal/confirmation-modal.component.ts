import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrl: './confirmation-modal.component.css'
})
export class ConfirmationModalComponent {
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


