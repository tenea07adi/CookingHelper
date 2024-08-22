import { ValidatorFn } from "@angular/forms";
import { SelectFormFieldValueModel } from "./select-form-field-value.model";

export interface DataFormFieldModel {
    hidden?: boolean;
    fieldLable: string;
    fieldName: string;    
    fieldType: string;
    fieldPlaceholder?: string;
    fieldValidators?: ValidatorFn[];
    fieldErrorMessage?: string;
    fieldDefaultValue?: string;
    selectValues?: SelectFormFieldValueModel[];
}