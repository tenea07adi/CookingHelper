import { Component, input, output } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { RecipeModel } from 'src/app/models/data-models/recipe-model';
import { DataFormFieldModel } from 'src/app/shared/data-form/model/data-form-field.model';

@Component({
  selector: 'app-recipe-form',
  templateUrl: './recipe-form.component.html',
  styleUrls: ['./recipe-form.component.css']
})
export class RecipeFormComponent {

  recipe = input<RecipeModel>({} as RecipeModel);
  submitRecipe = output<RecipeModel>();

  public get formStructure() : DataFormFieldModel[]{
    let formData = [
      {
        hidden: false,
        fieldLable: "Name",
        fieldName: "name",    
        fieldType: "text",
        fieldValidators: [Validators.required, Validators.minLength(1)],
        fieldDefaultValue: this.recipe()?.name
      },
      {
        hidden: false,
        fieldLable: "Image url",
        fieldName: "imageUrl",    
        fieldType: "text",
        fieldValidators: [],
        fieldDefaultValue: this.recipe()?.imageUrl
      },
      {
        hidden: false,
        fieldLable: "External link",
        fieldName: "externalLink",    
        fieldType: "text",
        fieldValidators: [],
        fieldDefaultValue: this.recipe()?.externalLink
      },
      {
        hidden: false,
        fieldLable: "Duration",
        fieldName: "estimatedDurationInMinutes",    
        fieldType: "number",
        fieldValidators: [Validators.required, Validators.min(1), Validators.pattern("^[0-9]*$")],
        fieldDefaultValue: this.recipe()?.estimatedDurationInMinutes?.toString()
      },
      {
        hidden: false,
        fieldLable: "Description",
        fieldName: "description",    
        fieldType: "textarea",
        fieldValidators: [],
        fieldDefaultValue: this.recipe()?.description
      }
    ]

    if(this.recipe().id){
      formData.push(
        {
          hidden: true,
          fieldLable: "id",
          fieldName: "id",
          fieldType: "number",
          fieldDefaultValue: this.recipe()?.id.toString(),
          fieldValidators: []
        }
      );
    }

    return formData;
  }

  onSubmitForm(dataForm: FormGroup){
    this.submitRecipe.emit(dataForm.value);
  }
}
