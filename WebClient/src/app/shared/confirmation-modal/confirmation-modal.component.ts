import { Component, input, output, ViewChild } from '@angular/core';
import { SimpleModalComponent } from '../simple-modal/simple-modal.component';

@Component({
  selector: 'app-confirmation-modal',
  templateUrl: './confirmation-modal.component.html',
  styleUrl: './confirmation-modal.component.css'
})
export class ConfirmationModalComponent {
  questionText = input.required<string>();
  close = output<void>();
  confirm = output<void>();

  @ViewChild("simpleModal") simpleModal! : SimpleModalComponent;
  
  onClose(){
    this.close.emit();
  }

  onConfirm(){
    this.closeModal();
    this.confirm.emit();
  }

  openModal(){
    this.simpleModal.openModal();
  }

  closeModal(){
    this.simpleModal.closeModal();
    this.onClose();
  }
}