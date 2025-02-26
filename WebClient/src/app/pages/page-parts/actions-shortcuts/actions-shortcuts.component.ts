import { Component, inject, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { GroceryListModel } from 'src/app/models/data-models/grocery-list.model';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';
import { RecipeModel } from 'src/app/models/data-models/recipe-model';
import { DataModelsMapper } from 'src/app/models/ModelMappers/data-models-mapper';
import { DataSourceService } from 'src/app/services/data-source.service';
import { SimpleModalComponent } from 'src/app/shared/simple-modal/simple-modal.component';

@Component({
  selector: 'app-actions-shortcuts',
  templateUrl: './actions-shortcuts.component.html',
  styleUrl: './actions-shortcuts.component.css'
})
export class ActionsShortcutsComponent {

  private dataSourceService = inject(DataSourceService);
  private router = inject(Router);

  @ViewChild("newRecipeModal") newRecipeModal! : SimpleModalComponent;
  @ViewChild("newIngredientModal") newIngredientModal! : SimpleModalComponent;
  @ViewChild("newGroceryListModal") newGroceryListModal! : SimpleModalComponent;
  
  onAddRecipe(data: RecipeModel){
    let newUserId: number = 0;
    this.dataSourceService.addRecord<RecipeModel>(data,DataModelsMapper.Recipe).subscribe({
      next: (data) => {
        newUserId = data.id;
      },
      complete: () =>{
        this.newRecipeModal.closeModal();
        this.router.navigate(['/recipe-details', newUserId]);
      }
    })
  }

  onAddIngredient(data: IngredientModel){
    this.dataSourceService.addRecord<IngredientModel>(data,DataModelsMapper.Ingredient).subscribe({
      next: (data) => {
      },
      complete: () =>{
        this.newIngredientModal.closeModal();
        this.router.navigate(['/ingredients']);
      }
    })
  }

  onAddGroceryList(data: GroceryListModel){
    let newListId = 0;
    this.dataSourceService.addRecord<GroceryListModel>(data,DataModelsMapper.GroceryList).subscribe({
      next: (data) => {
        newListId = data.id;
      },
      complete: () =>{
        this.newGroceryListModal.closeModal();
        this.router.navigate(['/grocery-list-details', newListId]);
      }
    })
  }

  displayNewRecipeModal(){
    this.newRecipeModal.openModal();
  }

  displayNewIngredientModal(){
    this.newIngredientModal.openModal();
  }

  displayNewGroceryListModal(){
    this.newGroceryListModal.openModal();
  }
}
