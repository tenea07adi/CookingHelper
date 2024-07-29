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

  nextOffset: number = -1; 

  loadedRecipes: boolean = false;
  recipesList: RecipeModel[] = [];

  ngOnInit(){
    this.loadRecipes();
  }

  private loadRecipes(){
    //this.recipesList = this.dataSourceService.getRecipes();
    this.dataSourceService.getRecords<RecipeModel>(DataModelsMapper.Recipe).subscribe({next: (data) => {
      this.recipesList = data.records;
      this.nextOffset = data.nextOffset;
      this.loadedRecipes = true;
    }});
  }

}
