import { Component, input } from '@angular/core';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';

@Component({
  selector: 'app-ingredient-preview-component',
  templateUrl: './ingredient-preview-component.component.html',
  styleUrl: './ingredient-preview-component.component.css'
})
export class IngredientPreviewComponentComponent {

  ingredient = input.required<IngredientModel>();

}
