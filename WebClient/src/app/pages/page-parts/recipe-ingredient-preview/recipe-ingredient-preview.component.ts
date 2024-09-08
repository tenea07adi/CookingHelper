import { Component, input, output } from '@angular/core';
import { RecipeIngredientModel } from 'src/app/models/data-models/recipe-ingredient-model';

@Component({
  selector: 'app-recipe-ingredient-preview',
  templateUrl: './recipe-ingredient-preview.component.html',
  styleUrl: './recipe-ingredient-preview.component.css'
})
export class RecipeIngredientPreviewComponent {
  recipeIngredient = input.required<RecipeIngredientModel>(); 
  remove = output<number>();

  displayRemoveIngredientConfirmationModal: boolean = false;

  onRemove(){
    this.remove.emit(this.recipeIngredient().ingredientId);
  }
}
