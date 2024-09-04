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

  searchField: string = 'Name';
  searchType: number = 2;
  searchValue: string = '';

  recordsOnRow: number = 3;

  recipesOffset: number = 0;

  loadedRecipes: boolean = false;
  recipesList: RecipeModel[] = [];

  selectedRecipesList: number[] = [];

  ngOnInit(){
    this.loadRecipes();
  }

  loadRecipes(){
    this.dataSourceService.getRecords<RecipeModel>(DataModelsMapper.Recipe, this.recipesOffset, this.searchField, this.searchValue, this.searchType).subscribe({next: (data) => {
      this.recipesList = [...this.recipesList, ...data.records];
      this.recipesOffset = data.nextOffset;
      this.loadedRecipes = true;
    }});
  }

  setRowSize(size: number){
    this.recordsOnRow = size;
  }

  onSearch(searchTerm: string){
    this.searchValue = searchTerm;
    this.recipesOffset = 0;
    this.recipesList = [];

    this.loadRecipes();
  }

  // start selection logic
  selectRecipe(id: number){
    this.selectedRecipesList.push(id);
  }

  unselectRecipe(id: number){
    this.selectedRecipesList = this.selectedRecipesList.filter(c => c != id);
  }

  resetSelection(){
    this.selectedRecipesList = [];
  }

  toggleSelection(id: number){
    if(this.selectedRecipesList.find(c => c == id) != undefined){
      this.unselectRecipe(id);
    }
    else {
      this.selectRecipe(id);
    }
  }

  getSelectionStyles(id: number) :string {
    if(this.selectedRecipesList.find(c => c == id) != undefined){
      return 'selected';
    }
    else {
      return '';
    }
  }
    // end selection logic
}
