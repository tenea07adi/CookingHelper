import { inject, Injectable } from "@angular/core";
import { BaseDataModel } from "../models/data-models/base-data-model";
import { PaginatedListModel } from "../models/structural-models/paginated-list-model";
import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { DataModelsMapper } from "../models/ModelMappers/data-models-mapper";
import { RecipeIngredientModel } from "../models/data-models/recipe-ingredient-model";
import { EnumModel } from "../models/data-models/enum-model";
import { NewRecipeIngredientModel } from "../models/data-models/new-recipe-ingredient";
import { IngredientModel } from "../models/data-models/ingredient-model";
import { AppConfigService } from "./app-config.service";
import { AuthService } from "./auth.service";

@Injectable({providedIn: "root"})
export class DataSourceService {

    private get restApiUrl() {
      return this.appConfigService.getRestApiUrl();
    }

    private httpClient = inject(HttpClient);
    private appConfigService = inject(AppConfigService);
    private authService = inject(AuthService);


    // Generic functions

    getAllRecords<T extends BaseDataModel>(entityMapp: DataModelsMapper) : Observable<T[]>{
      let url = this.compozeUrl(entityMapp) + "/all";
      return this.DoGet<T[]>(url);
    }

    getRecords<T extends BaseDataModel>(entityMapp: DataModelsMapper, offset?: number, 
      searchField?: string, searchValue?: string, searchType?: number) : Observable<PaginatedListModel<T>>{
        if(offset == undefined){
          offset = 0;
        }

        let params = new HttpParams();

        params= params.append('offset', offset);
        params= params.append('maxsize', 20);

        if(searchField && searchField.length > 0 && 
          searchValue && searchValue.length > 0 && 
          searchType){
            params= params.append('filterField', searchField);
            params= params.append('filterValue', searchValue);
            params= params.append('filterType', searchType);
          }
          
        let url = this.compozeUrl(entityMapp);

        return this.DoGet<PaginatedListModel<T>>(url, params);
    }

    getRecordById<T extends BaseDataModel>(id: number, entityMapp: DataModelsMapper) : Observable<T> {
        let url = this.compozeUrl(entityMapp) + "/" + id;
        return this.DoGet<T>(url);
    }

    addRecord<T extends BaseDataModel>(record: T, entityMapp: DataModelsMapper) : Observable<T>{
      let url = this.compozeUrl(entityMapp);
      return this.DoPost<T>(url, record);
    }

    updateRecord<T extends BaseDataModel>(record: T, entityMapp: DataModelsMapper) : Observable<T>{
      let url = this.compozeUrl(entityMapp) + "/" + record.id;
      return this.DoPut<T>(url, record);
    }

    deleteRecord<T extends BaseDataModel>(recordId: number, entityMapp: DataModelsMapper) : Observable<any>{
      let url = this.compozeUrl(entityMapp) + "/" + recordId;
      return this.DoDelete<T>(url);
    }

    // Ingredient specific functions

    getMeasureUnits() : Observable<EnumModel[]>{
      let url = `${this.restApiUrl}/ingredients/measureunits`; 
      return this.DoGet<EnumModel[]>(url);
    }

    // Recipe specific functions

    getRecipeIngredients(recipeId: number) : Observable<RecipeIngredientModel[]>{
      let url = `${this.restApiUrl}/recipes/${recipeId}/ingredients`; 
      return this.DoGet<RecipeIngredientModel[]>(url);
    }

    getRecipeAvailableIngredients(recipeId: number) : Observable<IngredientModel[]>{
      let url = `${this.restApiUrl}/recipes/${recipeId}/availableIngredients`; 
      return this.DoGet<IngredientModel[]>(url);
    }

    addRecipeIngredient(recipeId: number, ingredientId: number, data: NewRecipeIngredientModel) : Observable<RecipeIngredientModel>{
      let url = `${this.restApiUrl}/recipes/${recipeId}/ingredient/${ingredientId}`; 
      let reqData = {MeasureUnit: data.measureUnit, Quantity: data.quantity};
      return this.DoPost<RecipeIngredientModel>(url, reqData);
    }

    removeRecipeIngredient(recipeId: number, ingredientId: number) : Observable<RecipeIngredientModel>{
      let url = `${this.restApiUrl}/recipes/${recipeId}/ingredient/${ingredientId}`; 
      return this.DoDelete<RecipeIngredientModel>(url);
    }

    // Private

    private compozeUrl(entityMapp: DataModelsMapper) : string {
      return `${this.restApiUrl}/${entityMapp.valueOf()}`;
    }

    private getAuthHeaders(){
      let token = this.authService.getJwtToken();

      if(token == null || token == undefined){
        token = "";
      }

      return {'AuthToken': token};
    }

    private DoGet<T>(url: string, params?: HttpParams): Observable<T>{
      if(params == undefined){
        params = new HttpParams();
      }

      return this.httpClient.get<T>(url, {params: params, headers : this.getAuthHeaders()});
    }

    private DoPost<T>(url: string, record: any, params?: HttpParams): Observable<T>{
      if(params == undefined){
        params = new HttpParams();
      }

      return this.httpClient.post<T>(url, record, {params: params, headers : this.getAuthHeaders()});
    }

    private DoPut<T>(url: string, record: T, params?: HttpParams): Observable<T>{
      if(params == undefined){
        params = new HttpParams();
      }

      return this.httpClient.put<T>(url, record, {params: params, headers : this.getAuthHeaders()});
    }

    private DoDelete<T>(url: string, params?: HttpParams): Observable<T>{
      if(params == undefined){
        params = new HttpParams();
      }

      return this.httpClient.delete<T>(url, {params: params, headers : this.getAuthHeaders()});
    }
}