import { Component, inject, input, output, ViewChild } from '@angular/core';
import { RecipeIngredientModel } from 'src/app/models/data-models/recipe-ingredient-model';
import { DataSourceService } from 'src/app/services/data-source.service';
import { IngredientsListExportModalComponent } from '../ingredients-list-export-modal/ingredients-list-export-modal.component';

@Component({
  selector: 'app-selected-recipes-actions',
  templateUrl: './selected-recipes-actions.component.html',
  styleUrl: './selected-recipes-actions.component.css'
})
export class SelectedRecipesActionsComponent {
  selectedRecipesList = input.required<number[]>();
  onResetSelection = output<void>();

  ingredients : RecipeIngredientModel[] = [];

  @ViewChild("ingredientsExportModal") ingredientsExportModal! : IngredientsListExportModalComponent;

  dataSourceService = inject(DataSourceService);

  exportIngredientsList(){
    this.dataSourceService.getRecipesIngredients(this.selectedRecipesList()).subscribe({
      next: (data) => {
        this.ingredients = data;
        this.openIngredientsExportModal();
      }
    })
  }

  resetSelection(){
    this.ingredients = [];
    this.onResetSelection.emit();
  }
  
  openIngredientsExportModal(){
    this.ingredientsExportModal.openModal();
  }
}
