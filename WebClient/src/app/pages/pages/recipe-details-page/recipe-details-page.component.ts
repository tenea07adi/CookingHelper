import { Component, inject, input, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { EnumModel } from 'src/app/models/data-models/enum-model';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';
import { NewRecipeIngredientModel } from 'src/app/models/data-models/new-recipe-ingredient';
import { RecipeIngredientModel } from 'src/app/models/data-models/recipe-ingredient-model';
import { RecipeModel } from 'src/app/models/data-models/recipe-model';
import { DataModelsMapper } from 'src/app/models/ModelMappers/data-models-mapper';
import { DataSourceService } from 'src/app/services/data-source.service';
import { IngredientsListExportModalComponent } from '../../page-parts/ingredients-list-export-modal/ingredients-list-export-modal.component';
import { PreparationStepModel } from 'src/app/models/data-models/preparation-step-model';

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
  loadedPreparationSteps: boolean = false;

  displayNewIngredientModal: boolean = false;
  displayUpdateRecipeModal: boolean = false;
  displayNewPreparationStepModal: boolean = false;

  displayDeleteRecipeConfirmationModal: boolean = false;

  recipeId = input.required<number>();

  recipe: RecipeModel = {} as RecipeModel;
  measureUnits: EnumModel[] = [];
  ingredients: RecipeIngredientModel[] = [];

  availableIngredients: IngredientModel[] = [];

  preparationSteps: PreparationStepModel[] = [];
  
  @ViewChild("ingredientsExportModal") ingredientsExportModal! : IngredientsListExportModalComponent;

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
        this.loadPreparationSteps();
      }
    })
  }

  loadMeasureUnits(){
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
    this.dataSourceService.getRecipeAvailableIngredients(this.recipeId()).subscribe({
      next: (data) => {
        this.availableIngredients = data;
      }
    })
  }

  loadPreparationSteps(){
    this.dataSourceService.getRecipePreparationSteps(this.recipeId()).subscribe({
      next: (data) => {
        this.preparationSteps = data;
        this.loadedPreparationSteps = true;
      }
    })
  }

  onAddPreparationStep(newPreparationStep: PreparationStepModel){
    newPreparationStep.recipeId = this.recipeId();

    this.dataSourceService.addRecord<PreparationStepModel>(newPreparationStep, DataModelsMapper.PreparationSteps).subscribe({
      next: (data) => {
        this.loadPreparationSteps();
        this.displayNewPreparationStepModal = false;
      }
    })
  }

  onChangedPreparationStep(stepId: number){
    this.loadPreparationSteps();
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

  openIngredientsExportModal(){
    this.ingredientsExportModal.openModal();
  }
}
