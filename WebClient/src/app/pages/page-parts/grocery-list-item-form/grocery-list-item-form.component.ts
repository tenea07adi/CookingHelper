import { Component, input, output, ViewChild } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { EnumModel } from 'src/app/models/data-models/enum-model';
import { GroceryListItemModel } from 'src/app/models/data-models/grocery-list-item.model';
import { DataFormFieldModel } from 'src/app/shared/data-form/model/data-form-field.model';
import { SelectFormFieldValueModel } from 'src/app/shared/data-form/model/select-form-field-value.model';

@Component({
  selector: 'app-grocery-list-item-form',
  templateUrl: './grocery-list-item-form.component.html',
  styleUrl: './grocery-list-item-form.component.css'
})
export class GroceryListItemFormComponent {
  groceryListItem = input<GroceryListItemModel>({} as GroceryListItemModel);
  
  measureUnits = input.required<EnumModel[]>();

  submitGroceryListItem = output<GroceryListItemModel>();

  public get formStructure() : DataFormFieldModel[]{
    let formData = [
      {
        hidden: false,
        fieldLable: "Name",
        fieldName: "name",    
        fieldType: "text",
        fieldValidators: [Validators.required, Validators.minLength(1)],
        fieldDefaultValue: this.groceryListItem()?.name
      },
      {
        hidden: false,
        fieldLable: "Description",
        fieldName: "description",    
        fieldType: "textarea",
        fieldValidators: [],
        fieldDefaultValue: this.groceryListItem()?.description
      },
      {
        hidden: false,
        fieldLable: "Measure unit",
        fieldName: "measureUnit",    
        fieldType: "select",
        fieldValidators: [Validators.required],
        selectValues: this.getMeasureUnitsForSelect()
      },
      {
        hidden: false,
        fieldLable: "Quantity",
        fieldName: "quantity",    
        fieldType: "number",
        fieldDefaultValue: "1",
        fieldValidators: [Validators.required, Validators.min(0.1), Validators.pattern(/^-?\d*[.,]?\d{0,2}$/)]
      }
    ]

    if(this.groceryListItem().id){
      formData.push(
        {
          hidden: true,
          fieldLable: "id",
          fieldName: "id",
          fieldType: "number",
          fieldDefaultValue: this.groceryListItem().id.toString(),
          fieldValidators: []
        }
      );
    }

    return formData;
  }
  
    getMeasureUnitsForSelect(): SelectFormFieldValueModel[]{
      let values : SelectFormFieldValueModel[] = [];
  
      this.measureUnits().map((unit) => { values.push({value: unit.value.toString(), name: unit.name})});
  
      return values;
    }
  

  onSubmitForm(dataForm: FormGroup){
    this.submitGroceryListItem.emit(dataForm.value);
  }
}
