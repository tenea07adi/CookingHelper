import { Component, input } from '@angular/core';
import { RecipeModel } from 'src/app/models/data-models/recipe-model';

@Component({
  selector: 'app-recipe-preview-component',
  templateUrl: './recipe-preview-component.component.html',
  styleUrls: ['./recipe-preview-component.component.css']
})
export class RecipePreviewComponentComponent {
  recipe = input.required<RecipeModel>();

  get description(): string {
    if(this.recipe().description == null || this.recipe().description == null){
      return '';
    }

    return this.recipe().description.substring(0, 200);
  }
}
