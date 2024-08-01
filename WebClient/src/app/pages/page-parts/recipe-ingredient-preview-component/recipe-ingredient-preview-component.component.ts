import { Component, input, output } from '@angular/core';
import { RecipeIngredientModel } from 'src/app/models/data-models/recipe-ingredient-model';

@Component({
  selector: 'app-recipe-ingredient-preview-component',
  templateUrl: './recipe-ingredient-preview-component.component.html',
  styleUrl: './recipe-ingredient-preview-component.component.css'
})
export class RecipeIngredientPreviewComponentComponent {
  recipeIngredient = input.required<RecipeIngredientModel>(); 
  remove = output<number>();

  displayRemoveIngredientConfirmationModal: boolean = false;

  onRemove(){
    this.remove.emit(this.recipeIngredient().ingredientId);
  }
}
