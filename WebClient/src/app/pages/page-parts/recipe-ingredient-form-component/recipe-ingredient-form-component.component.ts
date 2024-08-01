import { Component, input, output } from '@angular/core';
import { EnumModel } from 'src/app/models/data-models/enum-model';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';
import { NewRecipeIngredientModel } from 'src/app/models/data-models/new-recipe-ingredient';

@Component({
  selector: 'app-recipe-ingredient-form-component',
  templateUrl: './recipe-ingredient-form-component.component.html',
  styleUrl: './recipe-ingredient-form-component.component.css'
})
export class RecipeIngredientFormComponentComponent {
  ingredients = input.required<IngredientModel[]>();
  measureUnits = input.required<EnumModel[]>();

  submitRecipe = output<NewRecipeIngredientModel>();

  newRecipeIngredient: NewRecipeIngredientModel = {} as NewRecipeIngredientModel;


  onSubmitData(){
    this.newRecipeIngredient.measureUnit = +this.newRecipeIngredient.measureUnit;
    this.submitRecipe.emit(this.newRecipeIngredient);
    this.newRecipeIngredient = {} as NewRecipeIngredientModel;
  }
}
