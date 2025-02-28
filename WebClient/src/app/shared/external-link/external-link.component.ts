import { Component, input, ViewChild } from '@angular/core';
import { ConfirmationModalComponent } from '../confirmation-modal/confirmation-modal.component';

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

  @ViewChild("confirmationModal") confirmationModal! : ConfirmationModalComponent;

  get isUrlValid() : boolean {
    if(this.url() == undefined){
      return false;
    }

    if(this.url() == null){
      return false;
    }

    if(this.url() == '' || this.url().trim() == ''){
      return false;
    }

    if(this.url() == undefined || this.url() == null || this.url() == '' || this.url().trim() == ''){
      return false;
    }

    return true;
  }

  onClick(){
    if(this.showConfirmationModal()){
      this.openConfirmationModal();
    }
    else{
      this.openUrl();
    }
  }

  openUrl(){
    window.open(this.url(), "_blank");
  }

  openConfirmationModal(){
    this.confirmationModal.openModal();
  }
}
