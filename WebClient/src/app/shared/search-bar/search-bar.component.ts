import { Component, input, output } from '@angular/core';
import { DataFormFieldModel } from '../data-form/model/data-form-field.model';
import { FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrl: './search-bar.component.css'
})
export class SearchBarComponent {

  initialSearch = input('');
  onSearch = output<string>();

  currentSearch : string = '';

  public get formStructure() : DataFormFieldModel[]{
    let formData = [
      {
        hidden: false,
        fieldLable: "Search",
        fieldName: "search",    
        fieldType: "text",
        fieldValidators: [Validators.required]
      }
    ]
  
    return formData;
  }

  ngOnInit(){
    this.currentSearch = this.initialSearch();

    if(this.currentSearch != ''){
      this.onSearch.emit(this.currentSearch);
    }
  }

  onSubmit(formData: FormGroup){
    this.currentSearch = formData.value['search'];
    this.onSearch.emit(this.currentSearch);
  }

  resetSearch(){
    this.currentSearch = '';
    this.onSearch.emit(this.currentSearch);
  }
}
