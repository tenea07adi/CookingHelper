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

  ingredientsOffset: number = 0;

  recordsOnRow: number = 1;
  
  loadedIngredients: boolean = false;
  error: boolean = false;

  ingredients: IngredientModel[] = [];

  ngOnInit(){
    this.loadIngredients();
  }

  loadIngredients(){
    this.dataSourceService.getRecords<IngredientModel>(DataModelsMapper.Ingredient, this.ingredientsOffset)
    .subscribe({next: async (data) => {
      this.ingredients = [...this.ingredients, ...data.records];
      this.ingredientsOffset = data.nextOffset;
      this.loadedIngredients = true;
    }});
  }

  onUpdateIngredient(ingredient: IngredientModel){
    this.dataSourceService.updateRecord<IngredientModel>(ingredient, DataModelsMapper.Ingredient).subscribe({
      complete: () => {
        this.reloadIngredients();
      }
    })
  }

  onDeleteIngredient(ingredientId: number){
    this.dataSourceService.deleteRecord<IngredientModel>(ingredientId, DataModelsMapper.Ingredient).subscribe({
      complete: () => {
        this.reloadIngredients();
      }
    })
  }

  reloadIngredients(){
    this.loadedIngredients = false;
    this.ingredientsOffset = 0;
    this.ingredients = [];
    this.loadIngredients();
  }

  setRowSize(size: number){
    this.recordsOnRow = size;
  }

}
