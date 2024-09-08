import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';
import { RecipeModel } from 'src/app/models/data-models/recipe-model';
import { DataModelsMapper } from 'src/app/models/ModelMappers/data-models-mapper';
import { DataSourceService } from 'src/app/services/data-source.service';

@Component({
  selector: 'app-actions-shortcuts',
  templateUrl: './actions-shortcuts.component.html',
  styleUrl: './actions-shortcuts.component.css'
})
export class ActionsShortcutsComponent {

  private dataSourceService = inject(DataSourceService);
  private router = inject(Router);

  displayNewRecipeModal: boolean = false;
  displayNewIngredientModal: boolean = false;

  onAddRecipe(data: RecipeModel){
    let newUserId: number = 0;
    this.dataSourceService.addRecord<RecipeModel>(data,DataModelsMapper.Recipe).subscribe({
      next: (data) => {
        newUserId = data.id;
      },
      complete: () =>{
        this.displayNewRecipeModal = false;
        this.router.navigate(['/recipe-details', newUserId]);
      }
    })
  }

  onAddIngredient(data: IngredientModel){
    this.dataSourceService.addRecord<IngredientModel>(data,DataModelsMapper.Ingredient).subscribe({
      next: (data) => {
      },
      complete: () =>{
        this.displayNewIngredientModal = false;
        this.router.navigate(['/ingredients']);
      }
    })
  }
}
