import { Component, inject } from '@angular/core';
import { GroceryListModel } from 'src/app/models/data-models/grocery-list.model';
import { DataModelsMapper } from 'src/app/models/ModelMappers/data-models-mapper';
import { DataSourceService } from 'src/app/services/data-source.service';

@Component({
  selector: 'app-grocery-lists-page',
  templateUrl: './grocery-lists-page.component.html',
  styleUrl: './grocery-lists-page.component.css'
})
export class GroceryListsPageComponent {
  private dataSourceService = inject(DataSourceService);

  public listsTypes(): typeof ListType{
    return ListType;
  }

  public groceryListType : ListType = ListType.Active;

  recordsOnRow: number = 1;

  searchField: string = 'Name';
  searchType: number = 2;
  searchValue: string = '';

  groceryListsOffset: number = 0;

  groceryLists: GroceryListModel[] = [];

  loadedGroceryLists: boolean = false;

  ngOnInit(){
    this.loadGroceryLists();
  }

  onSearch(searchTerm: string){
    this.searchValue = searchTerm;
    this.groceryListsOffset = 0;
    this.groceryLists = [];

    this.loadGroceryLists();
  }

  setRowSize(size: number){
    this.recordsOnRow = size;
  }

  public toggleLists(type: ListType){
    this.groceryListType = type;

    this.groceryListsOffset = 0;
    this.groceryLists = [];

    this.loadGroceryLists();
  }

  public getListTypeActiveClass(type: ListType) : string[] {
    let classes = ["nav-link"];

    if(this.groceryListType == type || this.searchValue != ""){
      classes.push("active");
      classes.push("cursor-not-allowed");
    }
    else{
      classes.push("text-warning");
      classes.push("cursor-pointer");
    }

    return classes;
  }

  public loadGroceryLists(){
    let efectiveSearchField = "";
    let efectiveSearchValue = "";
    let efectiveSearchType = 0;

    if(this.searchValue == ""){
      switch(this.groceryListType){
        case ListType.Active: {
          efectiveSearchField = "IsCompleted";
          efectiveSearchValue = "False";
          break;
        }
        case ListType.Completed: {
          efectiveSearchField = "IsCompleted";
          efectiveSearchValue = "True";
          break;
        }
        case ListType.Pinned: {
          efectiveSearchField = "IsPinned";
          efectiveSearchValue = "True";
          break;
        }
        default : {
          break;
        }
      }
    }
    else {
      efectiveSearchField = this.searchField;
      efectiveSearchValue = this.searchValue;
      efectiveSearchType = this.searchType;
    }

    this.dataSourceService.getRecords<GroceryListModel>(
      DataModelsMapper.GroceryList, 
      this.groceryListsOffset, 
      efectiveSearchField, 
      efectiveSearchValue, 
      efectiveSearchType)
    .subscribe({next: async (data) => {
      this.groceryLists = [...this.groceryLists, ...data.records];
      this.groceryListsOffset = data.nextOffset
      this.loadedGroceryLists = true;
    }})
  }
}

export enum ListType {
  Active, 
  Completed, 
  Pinned, 
}
