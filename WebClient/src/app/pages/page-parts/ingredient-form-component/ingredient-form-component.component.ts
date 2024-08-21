import { Component, input, output } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';
import { DataFormFieldModel } from 'src/app/shared/data-form/model/data-form-field.model';

@Component({
  selector: 'app-ingredient-form-component',
  templateUrl: './ingredient-form-component.component.html',
  styleUrl: './ingredient-form-component.component.css'
})
export class IngredientFormComponentComponent {
  ingredient = input<IngredientModel>({} as IngredientModel);

  submitIngredient = output<IngredientModel>();

  public get formStructure() : DataFormFieldModel[]{
    let formData = [
      {
        hidden: false,
        fieldLable: "Name",
        fieldName: "name",    
        fieldType: "text",
        fieldPlaceholder: "Enter name",
        fieldValidators: [Validators.required, Validators.minLength(1)],
        fieldDefaultValue: this.ingredient().name
      },
      {
        hidden: false,
        fieldLable: "Description",
        fieldName: "description",    
        fieldType: "textarea",
        fieldPlaceholder: "Enter description",
        fieldValidators: [Validators.required, Validators.minLength(1)],
        fieldDefaultValue: this.ingredient().description
      }
    ]

    if(this.ingredient().id){
      formData.push(
        {
          hidden: true,
          fieldLable: "id",
          fieldName: "id",
          fieldType: "number",
          fieldDefaultValue: this.ingredient().id.toString(),
          fieldPlaceholder: '',
          fieldValidators: []
        }
      );
    }

    return formData;
  }

  onSubmitForm(dataForm: FormGroup){
    console.log(dataForm.value);
    this.submitIngredient.emit(dataForm.value);
  }
}
