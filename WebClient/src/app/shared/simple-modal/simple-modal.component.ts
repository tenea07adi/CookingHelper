import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-simple-modal',
  templateUrl: './simple-modal.component.html',
  styleUrl: './simple-modal.component.css'
})
export class SimpleModalComponent {
  display = input<boolean>(false);
  headerText = input.required<string>();
  close = output<void>();

  onCloseModal(){
    this.close.emit();
  }
}
