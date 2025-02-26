import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-simple-modal',
  templateUrl: './simple-modal.component.html',
  styleUrl: './simple-modal.component.css'
})
export class SimpleModalComponent {
  headerText = input.required<string>();
  close = output<void>();

  display : boolean = false;

  onCloseModal(){
    this.close.emit();
  }

  openModal(){
    this.display = true;
  }

  closeModal(){
    this.display = false;
    this.onCloseModal();
  }


}
