import { Component, input, output, ViewChild } from '@angular/core';
import { RecipeIngredientModel } from 'src/app/models/data-models/recipe-ingredient-model';
import { ConfirmationModalComponent } from 'src/app/shared/confirmation-modal/confirmation-modal.component';

@Component({
  selector: 'app-recipe-ingredient-preview',
  templateUrl: './recipe-ingredient-preview.component.html',
  styleUrl: './recipe-ingredient-preview.component.css'
})
export class RecipeIngredientPreviewComponent {
  recipeIngredient = input.required<RecipeIngredientModel>(); 
  remove = output<number>();

  @ViewChild("deleteConfirmationModal") deleteConfirmationModal! : ConfirmationModalComponent;

  onRemove(){
    this.remove.emit(this.recipeIngredient().ingredientId);
  }

  openDeleteConfirmationModal(){
    this.deleteConfirmationModal.openModal();
  }
}
