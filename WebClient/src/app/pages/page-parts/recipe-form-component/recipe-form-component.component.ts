import { Component, input, output } from '@angular/core';
import { RecipeModel } from 'src/app/models/data-models/recipe-model';

@Component({
  selector: 'app-recipe-form-component',
  templateUrl: './recipe-form-component.component.html',
  styleUrls: ['./recipe-form-component.component.css']
})
export class RecipeFormComponentComponent {

  recipe = input<RecipeModel>({} as RecipeModel);
  submitRecipe = output<RecipeModel>();

  onSubmitData(){
    this.submitRecipe.emit(this.recipe());
  }
}
