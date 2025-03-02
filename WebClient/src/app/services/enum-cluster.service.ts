import { Injectable } from "@angular/core";

@Injectable({providedIn: "root"})
export class EnumClusterService{
    getIcons() : typeof IconsEnum{
        return IconsEnum;
    }

    getColorClasses() : typeof ColorClassEnum{
        return ColorClassEnum;
    }
}

export enum IconsEnum {
    Ingredient = "fa fa-shopping-basket",
    Recipe = "fa fa-book",
    PreparationStep = "",
    GroceryList = "fa fa-clipboard",
    Details = "fa fa-search-plus",
    Add = "fa fa-plus",
    Update = "fa fa-pencil",
    Up = "fa fa-caret-square-o-up",
    Down = "fa fa-caret-square-o-down",
    Delete = "fa fa-remove",
    Cancel = "fa fa-remove",
    Export = "fa fa-print",
    LogOut = "fa fa-sign-out",
    LogIn = "fa fa-sign-in",
    User = "fa fa-user-circle-o",
    Private = "fa fa-user-secret",
    Public = "fa fa-users",
    Time = "fa fa-clock-o",
    Completed = "fa fa-check-circle",
    CollapseSwitch = "fa fa-sort",
    Pin = "fa fa-map-pin",
    Info = "fa fa-info-circle",
    CaseUp = "fa fa-toggle-up",
    CaseDown = "fa fa-toggle-down",
    Ascending = "fa fa-sort-asc",
    Descending = "fa fa-sort-desc"
}

export enum ColorClassEnum {
    General = "warning",
    Update = "warning",
    Delete = "danger",
    Add = "success",
    Success = "success",
    Inactive = "secondary",
    Info = "info"
}