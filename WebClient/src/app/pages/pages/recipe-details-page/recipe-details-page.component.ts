import { Component, inject, input } from '@angular/core';
import { Router } from '@angular/router';
import { EnumModel } from 'src/app/models/data-models/enum-model';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';
import { NewRecipeIngredientModel } from 'src/app/models/data-models/new-recipe-ingredient';
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

  displayNewIngredientModal: boolean = false;
  displayUpdateRecipeModal: boolean = false;

  displayDeleteRecipeConfirmationModal: boolean = false;

  recipeId = input.required<number>();

  recipe: RecipeModel = {} as RecipeModel;
  measureUnits: EnumModel[] = [];
  ingredients: RecipeIngredientModel[] = [];

  availableIngredients: IngredientModel[] = [];

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
        this.loadMeasureUnits();
      }
    })
  }

  loadMeasureUnits(){
    this.loadedIngredients = false;
    this.dataSourceService.getMeasureUnits().subscribe({
      next: (data) => {
        this.measureUnits = data;
      }
    })
  }

  loadIngredeints(){
    this.loadedIngredients = false;
    this.dataSourceService.getRecipeIngredients(this.recipeId()).subscribe({
      next: (data) => {
        this.ingredients = data;
        this.loadedIngredients = true;
        this.loadAvailableIngredients();
      }
    })
  }

  loadAvailableIngredients(){
    this.dataSourceService.getAllRecords<IngredientModel>(DataModelsMapper.Ingredient).subscribe({
      next: (data) => {
        this.availableIngredients = data.filter((aing) => this.ingredients.filter((ing) => ing.ingredientId == aing.id).length <= 0);
      }
    })
  }

  onAddIngredient(newRecipeIngredient: NewRecipeIngredientModel){
    this.dataSourceService.addRecipeIngredient(this.recipeId(), newRecipeIngredient.ingredientId, newRecipeIngredient).subscribe({
      complete: () => {
        this.loadIngredeints();
        this.displayNewIngredientModal = false;
      }
    })
  }

  onRemoveIngredient(ingredientId: number){
    this.dataSourceService.removeRecipeIngredient(this.recipeId(), ingredientId).subscribe({
      complete: () => {
        this.loadIngredeints();
      }
    })
  }

  onUpdateRecipe(recipe: RecipeModel){
    this.dataSourceService.updateRecord<RecipeModel>(recipe, DataModelsMapper.Recipe).subscribe({
      complete: () => {
        this.displayUpdateRecipeModal = false;
        this.router.navigate(['/recipe-details', this.recipeId()]);
      }
    })
  }

  onDeleteRecipe(){
    this.dataSourceService.deleteRecord<RecipeModel>(this.recipeId(), DataModelsMapper.Recipe).subscribe({
      complete: () => {
        this.router.navigate(['/recipes']);
      }
    })
  }

}
