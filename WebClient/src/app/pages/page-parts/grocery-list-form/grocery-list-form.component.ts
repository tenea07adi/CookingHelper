import { Component, input, output } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { GroceryListModel } from 'src/app/models/data-models/grocery-list.model';
import { DataFormFieldModel } from 'src/app/shared/data-form/model/data-form-field.model';

@Component({
  selector: 'app-grocery-list-form',
  templateUrl: './grocery-list-form.component.html',
  styleUrl: './grocery-list-form.component.css'
})
export class GroceryListFormComponent {
  groceryList = input<GroceryListModel>({} as GroceryListModel);

  submitGroceryList = output<GroceryListModel>();

  public get formStructure() : DataFormFieldModel[]{
    let formData = [
      {
        hidden: false,
        fieldLable: "Name",
        fieldName: "name",    
        fieldType: "text",
        fieldValidators: [Validators.required, Validators.minLength(1)],
        fieldDefaultValue: this.groceryList()?.name
      },
      {
        hidden: false,
        fieldLable: "Description",
        fieldName: "description",    
        fieldType: "textarea",
        fieldValidators: [],
        fieldDefaultValue: this.groceryList()?.description
      },
      {
        hidden: false,
        fieldLable: "Is private",
        fieldName: "isPrivate",    
        fieldType: "checkbox",
        fieldValidators: [],
        fieldDefaultValue: this.groceryList()?.isPrivate ? "true" : "false"
      }
    ]

    if(this.groceryList().id){
      formData.push(
        {
          hidden: true,
          fieldLable: "id",
          fieldName: "id",
          fieldType: "number",
          fieldDefaultValue: this.groceryList().id.toString(),
          fieldValidators: []
        }
      );
    }

    return formData;
  }

  onSubmitForm(dataForm: FormGroup){
    this.submitGroceryList.emit(dataForm.value);
  }
}
