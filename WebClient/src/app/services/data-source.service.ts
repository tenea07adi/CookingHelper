import { inject, Injectable } from "@angular/core";
import { BaseDataModel } from "../models/data-models/base-data-model";
import { PaginatedListModel } from "../models/structural-models/paginated-list-model";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { DataModelsMapper } from "../models/ModelMappers/data-models-mapper";
import { RecipeIngredientModel } from "../models/data-models/recipe-ingredient-model";
import { EnumModel } from "../models/data-models/enum-model";
import { NewRecipeIngredientModel } from "../models/data-models/new-recipe-ingredient";

@Injectable({providedIn: "root"})
export class DataSourceService {

  private restApiUrl : string = "https://localhost:7156/api"

    private httpClient = inject(HttpClient)

    // Generic functions

    getAllRecords<T extends BaseDataModel>(entityMapp: DataModelsMapper) : Observable<T[]>{
      let url = this.compozeUrl(entityMapp) + "/all";
      return this.httpClient.get<T[]>(url);
    }

    getRecords<T extends BaseDataModel>(entityMapp: DataModelsMapper, offset?: number) : Observable<PaginatedListModel<T>>{
        if(offset == undefined){
          offset = 0;
        }

        let params = new HttpParams();

        params= params.append('offset', offset);
        params= params.append('maxsize', 20);

        let url = this.compozeUrl(entityMapp);

        return this.httpClient.get<PaginatedListModel<T>>(url, {params: params});
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
      let url = this.compozeUrl(entityMapp) + "/" + record.id;
      return this.httpClient.put<T>(url, record);
    }

    deleteRecord<T extends BaseDataModel>(recordId: number, entityMapp: DataModelsMapper) : Observable<any>{
      let url = this.compozeUrl(entityMapp) + "/" + recordId;
      return this.httpClient.delete<T>(url);
    }

    // Ingredient specific functions

    getMeasureUnits() : Observable<EnumModel[]>{
      let url = `${this.restApiUrl}/ingredients/measureunits`; 
      return this.httpClient.get<EnumModel[]>(url);
    }

    // Recipe specific functions

    getRecipeIngredients(recipeId: number) : Observable<RecipeIngredientModel[]>{
      let url = `${this.restApiUrl}/recipes/${recipeId}/ingredients`; 
      return this.httpClient.get<RecipeIngredientModel[]>(url);
    }

    addRecipeIngredient(recipeId: number, ingredientId: number, data: NewRecipeIngredientModel) : Observable<RecipeIngredientModel>{
      let url = `${this.restApiUrl}/recipes/${recipeId}/ingredient/${ingredientId}`; 
      let reqData = {MeasureUnit: data.measureUnit, Quantity: data.quantity};
      console.log(reqData);
      return this.httpClient.post<RecipeIngredientModel>(url, reqData);
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