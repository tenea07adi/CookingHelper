import { Component, input, output } from '@angular/core';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';

@Component({
  selector: 'app-ingredient-preview',
  templateUrl: './ingredient-preview.component.html',
  styleUrl: './ingredient-preview.component.css'
})
export class IngredientPreviewComponent {

  ingredient = input.required<IngredientModel>();

  delete = output<number>();
  update = output<IngredientModel>();

  displayUpdateIngredientModal: boolean = false;
  displayDeleteIngredientModal: boolean = false;

  onDelete(){
    this.displayDeleteIngredientModal = false;
    this.delete.emit(this.ingredient().id);
  }

  onUpdate(ingredient: IngredientModel){
    this.displayUpdateIngredientModal = false;
    this.update.emit(ingredient);
  }
}
