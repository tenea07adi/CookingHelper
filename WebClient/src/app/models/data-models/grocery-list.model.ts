import { BaseDataModel } from "./base-data-model";

export interface GroceryListModel extends BaseDataModel {
    name: string;
    description: string;

    isPrivate: boolean;
    isCompleted: boolean;
    isPinned: boolean;
}