import { Component, input, output } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { EnumModel } from 'src/app/models/data-models/enum-model';
import { IngredientModel } from 'src/app/models/data-models/ingredient-model';
import { NewRecipeIngredientModel } from 'src/app/models/data-models/new-recipe-ingredient';
import { DataFormFieldModel } from 'src/app/shared/data-form/model/data-form-field.model';
import { SelectFormFieldValueModel } from 'src/app/shared/data-form/model/select-form-field-value.model';

@Component({
  selector: 'app-recipe-ingredient-form',
  templateUrl: './recipe-ingredient-form.component.html',
  styleUrl: './recipe-ingredient-form.component.css'
})
export class RecipeIngredientFormComponent {
  ingredients = input.required<IngredientModel[]>();
  measureUnits = input.required<EnumModel[]>();

  submitRecipeIngredient = output<NewRecipeIngredientModel>();

  public get formStructure() : DataFormFieldModel[]{
    let formData = [
      {
        hidden: false,
        fieldLable: "Ingredient",
        fieldName: "ingredientId",    
        fieldType: "select",
        fieldValidators: [Validators.required],
        selectValues: this.getIngredientForSelect()
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
        fieldValidators: [Validators.required, Validators.min(0.1), Validators.pattern(/^-?\d*[.,]?\d{0,2}$/)]
      }
    ]

    return formData;
  }

  getIngredientForSelect(): SelectFormFieldValueModel[]{
    let values : SelectFormFieldValueModel[] = [];

    this.ingredients().map((ingredient) => { values.push({value: ingredient.id.toString(), name: ingredient.name})});

    return values;
  }

  getMeasureUnitsForSelect(): SelectFormFieldValueModel[]{
    let values : SelectFormFieldValueModel[] = [];

    this.measureUnits().map((unit) => { values.push({value: unit.value.toString(), name: unit.name})});

    return values;
  }

  onSubmitForm(dataForm: FormGroup){
    let value : NewRecipeIngredientModel = dataForm.value;
    value.measureUnit = +value.measureUnit;
    this.submitRecipeIngredient.emit(value);
  }
}
