import { BaseDataModel } from "../data-models/base-data-model";

export interface PaginatedListModel<T extends BaseDataModel>{
    nextOffset : number;
    count: number;
    records: T[]
}