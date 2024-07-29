import { BaseDataModel } from "./base-data-model";

export interface RecipeModel extends BaseDataModel {
    name: string;
    description: string;
    imageUrl: string;
    externalLink : string;
    estimatedDurationInMinutes: number;
}