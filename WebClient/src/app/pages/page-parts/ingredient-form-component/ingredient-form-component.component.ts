import { Component, input, output } from '@angular/core';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';

@Component({
  selector: 'app-ingredient-form-component',
  templateUrl: './ingredient-form-component.component.html',
  styleUrl: './ingredient-form-component.component.css'
})
export class IngredientFormComponentComponent {
  ingredient = input<IngredientModel>({} as IngredientModel);

  submitIngredient = output<IngredientModel>();

  onSubmitData(){
    this.submitIngredient.emit(this.ingredient());
  }
}
