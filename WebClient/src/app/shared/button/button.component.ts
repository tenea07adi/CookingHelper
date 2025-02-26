import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrl: './button.component.css'
})
export class ButtonComponent {
  text = input.required<string>();
  icon = input.required<string>();
  colorClass = input.required<string>();
  onClick = output<void>();

  public click(){
    this.onClick.emit();
  }
}
