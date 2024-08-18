import { Component, input } from '@angular/core';

@Component({
  selector: 'app-external-link',
  templateUrl: './external-link.component.html',
  styleUrl: './external-link.component.css'
})
export class ExternalLinkComponent {
  url = input.required<string>();
  text = input.required<string>();
  type = input<number>(1); // 0: text, 1: lable, 2: button
  showConfirmationModal = input<boolean>(false);

  displayConfirmationModal: boolean = false;

  onClick(){
    if(this.showConfirmationModal()){
      this.displayConfirmationModal = true;
    }
    else{
      this.openUrl();
    }
  }

  openUrl(){
    window.open(this.url(), "_blank");
  }
}
