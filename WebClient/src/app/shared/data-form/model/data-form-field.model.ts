import { ValidatorFn } from "@angular/forms";

export interface DataFormFieldModel {
    hidden?: boolean;
    fieldLable: string;
    fieldName: string;    
    fieldType: string;
    fieldPlaceholder?: string;
    fieldValidators?: ValidatorFn[];
    fieldErrorMessage?: string;
    fieldDefaultValue?: string;
    selectValues?: {name:"", value:""}[];
}