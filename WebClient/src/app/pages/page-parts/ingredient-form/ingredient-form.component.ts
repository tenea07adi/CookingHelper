import { Component, input, output } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';
import { DataFormFieldModel } from 'src/app/shared/data-form/model/data-form-field.model';

@Component({
  selector: 'app-ingredient-form',
  templateUrl: './ingredient-form.component.html',
  styleUrl: './ingredient-form.component.css'
})
export class IngredientFormComponent {
  ingredient = input<IngredientModel>({} as IngredientModel);

  submitIngredient = output<IngredientModel>();

  public get formStructure() : DataFormFieldModel[]{
    let formData = [
      {
        hidden: false,
        fieldLable: "Name",
        fieldName: "name",    
        fieldType: "text",
        fieldValidators: [Validators.required, Validators.minLength(1)],
        fieldDefaultValue: this.ingredient().name
      },
      {
        hidden: false,
        fieldLable: "Description",
        fieldName: "description",    
        fieldType: "textarea",
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
          fieldValidators: []
        }
      );
    }

    return formData;
  }

  onSubmitForm(dataForm: FormGroup){
    this.submitIngredient.emit(dataForm.value);
  }
}
