import { BaseDataModel } from "./base-data-model";

export interface PreparationStepModel extends BaseDataModel {
    recipeId: number;

    instructions: string;
    orderNumber: number;
}