import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-simple-modal-component',
  templateUrl: './simple-modal-component.component.html',
  styleUrl: './simple-modal-component.component.css'
})
export class SimpleModalComponentComponent {
  display = input<boolean>(false);
  headerText = input.required<string>();
  close = output<void>();

  onCloseModal(){
    this.close.emit();
  }
}
