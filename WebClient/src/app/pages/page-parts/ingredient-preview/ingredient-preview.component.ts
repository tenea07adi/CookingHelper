import { Component, input, output, ViewChild } from '@angular/core';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';
import { ConfirmationModalComponent } from 'src/app/shared/confirmation-modal/confirmation-modal.component';
import { SimpleModalComponent } from 'src/app/shared/simple-modal/simple-modal.component';

@Component({
  selector: 'app-ingredient-preview',
  templateUrl: './ingredient-preview.component.html',
  styleUrl: './ingredient-preview.component.css'
})
export class IngredientPreviewComponent {

  ingredient = input.required<IngredientModel>();

  delete = output<number>();
  update = output<IngredientModel>();

  @ViewChild("updateIngredientModal") updateIngredientModal! : SimpleModalComponent;
  @ViewChild("deleteConfirmationModal") deleteConfirmationModal! : ConfirmationModalComponent;

  onDelete(){
    this.deleteConfirmationModal.closeModal();
    this.delete.emit(this.ingredient().id);
  }

  onUpdate(ingredient: IngredientModel){
    this.updateIngredientModal.closeModal();
    this.update.emit(ingredient);
  }

  openUpdateIngredientModal(){
    this.updateIngredientModal.openModal();
  }

  openDeleteConfirmationModal(){
    this.deleteConfirmationModal.openModal();
  }
}
