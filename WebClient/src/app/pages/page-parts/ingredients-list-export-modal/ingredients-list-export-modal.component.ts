import { Component, input } from '@angular/core';
import { RecipeIngredientModel } from 'src/app/models/data-models/recipe-ingredient-model';

@Component({
  selector: 'app-ingredients-list-export-modal',
  templateUrl: './ingredients-list-export-modal.component.html',
  styleUrl: './ingredients-list-export-modal.component.css'
})
export class IngredientsListExportModalComponent {
  ingredients = input.required<RecipeIngredientModel[]>();
  recipeName = input<string>('');

  displayIngredientsListModal: boolean = false;

  onCopyIngredientsToClipboard(){
    let lineSeparator = '\r\n';
    
    let val = `Ingredients for "${this.recipeName()}": ${lineSeparator}`;

    this.ingredients().forEach((ing) =>{
      val += `- ${ing.name} | ${ing.quantity} ${ing.measureUnitName} ${lineSeparator}`; 
    })

    const selBox = document.createElement('textarea');
    selBox.style.position = 'fixed';
    selBox.style.left = '0';
    selBox.style.top = '0';
    selBox.style.opacity = '0';
    selBox.value = val;
    document.body.appendChild(selBox);
    selBox.focus();
    selBox.select();
    document.execCommand('copy');
    document.body.removeChild(selBox);
  }

  openModal(){
    this.displayIngredientsListModal = true;
  }

  closeModal(){
    this.displayIngredientsListModal = false;
  }
}
