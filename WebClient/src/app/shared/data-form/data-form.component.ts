import { Component, input, output } from '@angular/core';
import { DataFormFieldModel } from './model/data-form-field.model';
import { FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-data-form',
  templateUrl: './data-form.component.html',
  styleUrl: './data-form.component.css'
})
export class DataFormComponent {
  public formName = input<string>();
  public submitButtonText = input<string>("Submit");
  public formFields = input.required<DataFormFieldModel[]>();

  public activateFormChangeDetection = input<boolean>(false);

  public onChangedForm = output<FormGroup>();
  public onSubmitedForm = output<FormGroup>();

  public dataForm = new FormGroup({});

  ngOnInit(){
    this.populateForm();

    if(this.activateFormChangeDetection()){
      this.dataForm.valueChanges.subscribe({
        next: () => {
          this.onChanged();
        }
      })
    }
  }

  public onChanged(){
    this.onChangedForm.emit(this.dataForm);
  }

  public onSubmit(){
    if(this.dataForm.invalid){
      return;
    }

    this.onSubmitedForm.emit(this.dataForm);
    this.dataForm.reset();
  }

  public isFieldInvalid(fieldName: string){
    if(this.dataForm.get(fieldName)?.invalid && this.dataForm.get(fieldName)?.touched){
      return true;
    }
    else {
      return false;
    }
  }

  public getPlaceHolder(field: DataFormFieldModel): string{
    if(field.fieldPlaceholder){
      return field.fieldPlaceholder;
    } else {
      return `Enter ${field.fieldLable.toLowerCase()}`;
    }
  }

  private populateForm(){
    this.formFields().forEach(field => {
      this.dataForm.addControl(field.fieldName, new FormControl(field.fieldDefaultValue, field.fieldValidators));
    });
  }
}
