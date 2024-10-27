import { Component, input, output } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { PreparationStepModel } from 'src/app/models/data-models/preparation-step-model';
import { DataFormFieldModel } from 'src/app/shared/data-form/model/data-form-field.model';

@Component({
  selector: 'app-preparation-step-form',
  templateUrl: './preparation-step-form.component.html',
  styleUrl: './preparation-step-form.component.css'
})
export class PreparationStepFormComponent {
  preparationStep = input<PreparationStepModel>({} as PreparationStepModel);
  submitPreparationStep = output<PreparationStepModel>();

  public get formStructure() : DataFormFieldModel[]{
    let formData = [
      {
        hidden: false,
        fieldLable: "Instructions",
        fieldName: "instructions",    
        fieldType: "textarea",
        fieldValidators: [Validators.required],
        fieldDefaultValue: this.preparationStep()?.instructions
      }
    ]

    if(this.preparationStep().recipeId){
      formData.push(
        {
          hidden: true,
          fieldLable: "RecipeId",
          fieldName: "recipeId",    
          fieldType: "text",
          fieldValidators: [],
          fieldDefaultValue: this.preparationStep()?.recipeId.toString()
        }
      )
    }

    if(this.preparationStep().id){
      formData.push(
        {
          hidden: true,
          fieldLable: "id",
          fieldName: "id",
          fieldType: "number",
          fieldDefaultValue: this.preparationStep()?.id.toString(),
          fieldValidators: []
        }
      );
    }

    return formData;
  }

  onSubmitForm(dataForm: FormGroup){
    this.submitPreparationStep.emit(dataForm.value);
  }
}
