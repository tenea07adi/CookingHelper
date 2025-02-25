import { BaseDataModel } from "./base-data-model";

export interface GroceryListItemModel extends BaseDataModel {
    groceryListId: number;

    name: string;
    description: string;

    measureUnit: number;
    quantity: number;

    isCompleted: boolean;
}
