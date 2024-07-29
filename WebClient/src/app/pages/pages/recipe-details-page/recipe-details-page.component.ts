import { Component, inject, input } from '@angular/core';
import { Router } from '@angular/router';
import { RecipeIngredientModel } from 'src/app/models/data-models/recipe-ingredient-model';
import { RecipeModel } from 'src/app/models/data-models/recipe-model';
import { DataModelsMapper } from 'src/app/models/ModelMappers/data-models-mapper';
import { DataSourceService } from 'src/app/services/data-source.service';

@Component({
  selector: 'app-recipe-details-page',
  templateUrl: './recipe-details-page.component.html',
  styleUrls: ['./recipe-details-page.component.css']
})
export class RecipeDetailsPageComponent {

  private dataSourceService = inject(DataSourceService);
  private router = inject(Router);

  loadedRecipe: boolean = false;
  loadedIngredients: boolean = false;

  displayIngredientsListModal: boolean = false;

  displayDeleteRecipeConfirmationModal: boolean = false;
  displayDeleteIngredientConfirmationModal: boolean = false;


  recipeId = input.required<number>();

  recipe: RecipeModel = {} as RecipeModel;
  ingredients: RecipeIngredientModel[] = [];

  ngOnInit(){
    this.loadRecipe();
  }

  loadRecipe(){
    this.loadedRecipe = false;
    this.loadedIngredients = false;
    this.dataSourceService.getRecordById<RecipeModel>(this.recipeId(), DataModelsMapper.Recipe).subscribe({
      next: (data) => {
        this.recipe = data;
        this.loadedRecipe = true;
        this.loadIngredeints();
      }
    })
  }

  loadIngredeints(){
    this.loadedIngredients = false;
    this.dataSourceService.getRecipeIngredients(this.recipeId()).subscribe({
      next: (data) => {
        this.ingredients = data;
        this.loadedIngredients = true;
      }
    })
  }

  onAddIngredient(){
    this.loadIngredeints();
  }

  onRemoveIngredeint(){
    this.loadIngredeints();
  }

  onDeleteRecipe(){
    this.dataSourceService.deleteRecord<RecipeModel>(this.recipeId(), DataModelsMapper.Recipe).subscribe({
      complete: () => {
        this.router.navigate(['/recipes']);
      }
    })
  }
}
