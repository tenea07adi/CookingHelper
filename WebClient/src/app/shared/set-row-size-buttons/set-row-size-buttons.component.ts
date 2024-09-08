import { Component, input, output } from '@angular/core';

@Component({
  selector: 'app-set-row-size-buttons',
  templateUrl: './set-row-size-buttons.component.html',
  styleUrl: './set-row-size-buttons.component.css'
})
export class SetRowSizeButtonsComponent {

  initialSize = input<number>(2);

  minSize = input<number>(1);
  maxSize = input<number>(12);

  onSizeChange = output<number>();

  private currentSize: number = this.initialSize();

  ngOnInit(){
    this.currentSize = this.initialSize();
  }

  public Increase() {
    if(this.currentSize < this.maxSize()){
      this.currentSize ++;
      this.onSizeChange.emit(this.currentSize);
    }
  }

  public Decrease(){
    if(this.currentSize > this.minSize()){
      this.currentSize --;
      this.onSizeChange.emit(this.currentSize);
    }
  }

}
