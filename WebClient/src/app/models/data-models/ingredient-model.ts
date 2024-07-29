import { BaseDataModel } from "./base-data-model";

export interface IngredientModel extends BaseDataModel {
    name: string;
    description: string;
}