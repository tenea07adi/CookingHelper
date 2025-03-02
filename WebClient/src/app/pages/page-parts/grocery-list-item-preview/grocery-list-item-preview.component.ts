import { Component, inject, input, output, ViewChild } from '@angular/core';
import { EnumModel } from 'src/app/models/data-models/enum-model';
import { GroceryListItemModel } from 'src/app/models/data-models/grocery-list-item.model';
import { DataModelsMapper } from 'src/app/models/ModelMappers/data-models-mapper';
import { DataSourceService } from 'src/app/services/data-source.service';
import { EnumClusterService } from 'src/app/services/enum-cluster.service';
import { ConfirmationModalComponent } from 'src/app/shared/confirmation-modal/confirmation-modal.component';
import { CardButtonModel } from 'src/app/shared/display-card/model/card-button.model';

@Component({
  selector: 'app-grocery-list-item-preview',
  templateUrl: './grocery-list-item-preview.component.html',
  styleUrl: './grocery-list-item-preview.component.css'
})
export class GroceryListItemPreviewComponent {
  private dataSourceService = inject(DataSourceService);
  
  public enumClusterService = inject(EnumClusterService);
  
  groceryListItem = input.required<GroceryListItemModel>();
  measureUnits = input.required<EnumModel[]>();

  onSwitchedComplete = output<number>();
  onUpdated = output<number>();
  onDeleted = output<number>();

  @ViewChild("confirmationDeleteModal") confirmationDeleteModal! : ConfirmationModalComponent;
  @ViewChild("confirmationSwitchCompletedModal") confirmationSwitchCompletedModal! : ConfirmationModalComponent;

  cardButtons: CardButtonModel[] = [];

  ngOnInit(){
    this.loadCardButtons();
  }

  delete(){
    this.dataSourceService
      .deleteRecord<GroceryListItemModel>(this.groceryListItem().id, DataModelsMapper.GroceryListItem)
      .subscribe({next : (data) => {
        this.onDeleted.emit(this.groceryListItem().id);
      }});
  }

  switchComplete(){
    if(this.groceryListItem().isCompleted){
      this.dataSourceService.switchGroceryListItemToNotCompleted(this.groceryListItem().id)
        .subscribe({
          next : (data) => {
            this.onSwitchedComplete.emit(this.groceryListItem().id);
          }
        });
    }
    else {
      this.dataSourceService.switchGroceryListItemToCompleted(this.groceryListItem().id)
        .subscribe({
          next : (data) => {
            this.onSwitchedComplete.emit(this.groceryListItem().id);
          }
        });
    }
  }

  openConfirmationDeleteModal(){
    this.confirmationDeleteModal.openModal();
  }

  openConfirmationSwitchCompleteModal(){
    this.confirmationSwitchCompletedModal.openModal();
  }

  getMeasureUnitName() : string{
    return this.measureUnits()[this.groceryListItem().measureUnit].name;
  }

  private loadCardButtons(){
    this.cardButtons = [
      {
        text: this.groceryListItem().isCompleted ? "Re-open" : "Complete",
        icon: this.enumClusterService.getIcons().Completed,
        colorClass: this.groceryListItem().isCompleted ? this.enumClusterService.getColorClasses().Inactive : this.enumClusterService.getColorClasses().Success,
        onClick: (identifier) => {
          this.openConfirmationSwitchCompleteModal();
        }
      },
      {
        text: "Delete",
        icon: this.enumClusterService.getIcons().Delete,
        colorClass: this.enumClusterService.getColorClasses().Delete,
        onClick: (identifier) => {
          this.openConfirmationDeleteModal();
        }
      }
    ]
  }
}
