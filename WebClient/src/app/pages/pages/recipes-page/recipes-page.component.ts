import { Component, inject } from '@angular/core';
import { RecipeModel } from 'src/app/models/data-models/recipe-model';
import { DataModelsMapper } from 'src/app/models/ModelMappers/data-models-mapper';
import { DataSourceService } from 'src/app/services/data-source.service';

@Component({
  selector: 'app-recipes-page',
  templateUrl: './recipes-page.component.html',
  styleUrls: ['./recipes-page.component.css']
})
export class RecipesPageComponent {

  private dataSourceService = inject(DataSourceService);

  recipesOffset: number = 0;

  loadedRecipes: boolean = false;
  recipesList: RecipeModel[] = [];

  ngOnInit(){
    this.loadRecipes();
  }

  loadRecipes(){
    //this.recipesList = this.dataSourceService.getRecipes();
    this.dataSourceService.getRecords<RecipeModel>(DataModelsMapper.Recipe, this.recipesOffset).subscribe({next: (data) => {
      this.recipesList = [...this.recipesList, ...data.records];
      this.recipesOffset = data.nextOffset;
      this.loadedRecipes = true;
    }});
  }

}
