import { BaseDataModel } from "./base-data-model";

export interface RecipeIngredientModel extends BaseDataModel {
    recipeId : number;
    ingredientId: number;
    name: string;
    description: string;
    measureUnit: number;
    quantity: number;
}