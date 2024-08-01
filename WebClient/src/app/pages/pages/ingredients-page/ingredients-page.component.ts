import { Component, inject } from '@angular/core';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';
import { DataModelsMapper } from 'src/app/models/ModelMappers/data-models-mapper';
import { DataSourceService } from 'src/app/services/data-source.service';

@Component({
  selector: 'app-ingredients-page',
  templateUrl: './ingredients-page.component.html',
  styleUrl: './ingredients-page.component.css'
})
export class IngredientsPageComponent {

  private dataSourceService = inject(DataSourceService);
  
  loadedIngredients: boolean = false;
  error: boolean = false;

  ingredients: IngredientModel[] = [];

  ngOnInit(){
    this.loadIngredients();
  }

  loadIngredients(){
    this.loadedIngredients = false;
    this.dataSourceService.getRecords<IngredientModel>(DataModelsMapper.Ingredient)
    .subscribe({next: async (data) => {
      this.ingredients = data.records;
      this.loadedIngredients = true;
    }});
  }

  onUpdateIngredient(ingredient: IngredientModel){
    this.dataSourceService.updateRecord<IngredientModel>(ingredient, DataModelsMapper.Ingredient).subscribe({
      complete: () => {
        this.loadIngredients();
      }
    })
  }

  onDeleteIngredient(ingredientId: number){
    this.dataSourceService.deleteRecord<IngredientModel>(ingredientId, DataModelsMapper.Ingredient).subscribe({
      complete: () => {
        this.loadIngredients();
      }
    })
  }
}
