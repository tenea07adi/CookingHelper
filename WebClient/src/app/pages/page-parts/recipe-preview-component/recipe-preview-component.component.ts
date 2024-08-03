import { Component, input } from '@angular/core';
import { RecipeModel } from 'src/app/models/data-models/recipe-model';

@Component({
  selector: 'app-recipe-preview-component',
  templateUrl: './recipe-preview-component.component.html',
  styleUrls: ['./recipe-preview-component.component.css']
})
export class RecipePreviewComponentComponent {
  recipe = input.required<RecipeModel>();
}