import { Component, input } from '@angular/core';
import { GroceryListModel } from 'src/app/models/data-models/grocery-list.model';

@Component({
  selector: 'app-grocery-list-preview',
  templateUrl: './grocery-list-preview.component.html',
  styleUrl: './grocery-list-preview.component.css'
})
export class GroceryListPreviewComponent {
  groceryList = input.required<GroceryListModel>();
}
