import { inject, Injectable } from "@angular/core";
import { BaseDataModel } from "../models/data-models/base-data-model";
import { PaginatedListModel } from "../models/structural-models/paginated-list-model";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { DataModelsMapper } from "../models/ModelMappers/data-models-mapper";
import { RecipeIngredientModel } from "../models/data-models/recipe-ingredient-model";

@Injectable({providedIn: "root"})
export class DataSourceService {

  private restApiUrl : string = "https://localhost:7156/api"

    private httpClient = inject(HttpClient)

    // Generic functions

    getRecords<T extends BaseDataModel>(entityMapp: DataModelsMapper) : Observable<PaginatedListModel<T>>{
        let url = this.compozeUrl(entityMapp);
        return this.httpClient.get<PaginatedListModel<T>>(url);
    }

    getRecordById<T extends BaseDataModel>(id: number, entityMapp: DataModelsMapper) : Observable<T> {
        let url = this.compozeUrl(entityMapp) + "/" + id;
        return this.httpClient.get<T>(url);
    }

    addRecord<T extends BaseDataModel>(record: T, entityMapp: DataModelsMapper) : Observable<T>{
      let url = this.compozeUrl(entityMapp);
      return this.httpClient.post<T>(url, record);
    }

    updateRecord<T extends BaseDataModel>(record: T, entityMapp: DataModelsMapper) : Observable<T>{
      let url = this.compozeUrl(entityMapp);
      return this.httpClient.put<T>(url, record);
    }

    deleteRecord<T extends BaseDataModel>(recordId: number, entityMapp: DataModelsMapper) : Observable<any>{
      let url = this.compozeUrl(entityMapp) + "/" + recordId;
      return this.httpClient.delete<T>(url);
    }

    // Recipe specific functions

    getRecipeIngredients(recipeId: number) : Observable<RecipeIngredientModel[]>{
      let url = `${this.restApiUrl}/recipes/${recipeId}/ingredients`; 
      return this.httpClient.get<RecipeIngredientModel[]>(url);
    }

    addRecipeIngredient(recipeId: number, ingredientId: number, data: {measureUnit: number, quantity: number}) : Observable<RecipeIngredientModel>{
      let url = `${this.restApiUrl}/recipes/${recipeId}/ingredient/${ingredientId}`; 
      return this.httpClient.post<RecipeIngredientModel>(url, null);
    }

    removeRecipeIngredient(recipeId: number, ingredientId: number) : Observable<RecipeIngredientModel>{
      let url = `${this.restApiUrl}/recipes/${recipeId}/ingredient/${ingredientId}`; 
      return this.httpClient.delete<RecipeIngredientModel>(url);
    }

    // Private

    private compozeUrl(entityMapp: DataModelsMapper) : string {
      return `${this.restApiUrl}/${entityMapp.valueOf()}`;
    }

}