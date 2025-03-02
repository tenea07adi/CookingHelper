import { Component, inject, input, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { EnumModel } from 'src/app/models/data-models/enum-model';
import { GroceryListItemModel } from 'src/app/models/data-models/grocery-list-item.model';
import { GroceryListModel } from 'src/app/models/data-models/grocery-list.model';
import { DataModelsMapper } from 'src/app/models/ModelMappers/data-models-mapper';
import { AuthService } from 'src/app/services/auth.service';
import { DataSourceService } from 'src/app/services/data-source.service';
import { EnumClusterService } from 'src/app/services/enum-cluster.service';
import { ConfirmationModalComponent } from 'src/app/shared/confirmation-modal/confirmation-modal.component';
import { CardButtonModel } from 'src/app/shared/display-card/model/card-button.model';
import { SimpleModalComponent } from 'src/app/shared/simple-modal/simple-modal.component';

@Component({
  selector: 'app-grocery-list-details-page',
  templateUrl: './grocery-list-details-page.component.html',
  styleUrl: './grocery-list-details-page.component.css'
})
export class GroceryListDetailsPageComponent {
    private dataSourceService = inject(DataSourceService);
    private router = inject(Router);
    public enumClusterService = inject(EnumClusterService);
    private authService = inject(AuthService);

    groceryListId = input.required<number>();

    @ViewChild("newGroceryListItemModal") newGroceryListItemModal! : SimpleModalComponent;

    @ViewChild("updateGroceryListModal") updateGroceryListModal! : SimpleModalComponent;

    @ViewChild("confirmationDeleteModal") confirmationDeleteModal! : ConfirmationModalComponent;
    @ViewChild("confirmationSwitchCompletedModal") confirmationSwitchCompletedModal! : ConfirmationModalComponent;
    @ViewChild("confirmationSwitchPinnedModal") confirmationSwitchPinnedModal! : ConfirmationModalComponent;

    cardButtons: CardButtonModel[] = [];
    
    loadedGroceryList: boolean = false;
    loadedGroceryListItems: boolean = false;

    isCreatedByCurrentUser : boolean = false;

    groceryList: GroceryListModel = {} as GroceryListModel;

    measureUnits: EnumModel[] = [];

    groceryListItemsCompleted: GroceryListItemModel[] = [];
    groceryListItemsNotCompleted: GroceryListItemModel[] = [];

    ngOnInit(){
      this.loadGroceryList();
      this.loadMeasureUnits();
    }
  
    loadGroceryList(){
      this.loadedGroceryList = false;
      this.loadedGroceryListItems = false;

      this.dataSourceService.getRecordById<GroceryListModel>(this.groceryListId(), DataModelsMapper.GroceryList).subscribe({
        next: (data) => {
          this.groceryList = data;
          this.loadedGroceryList = true;
          this.checkIsCreatedByCurrentUser();
          this.loadCardButtons();
          this.loadGroceryListItems();
        }
      })
    }

    loadMeasureUnits(){
      this.dataSourceService.getMeasureUnits().subscribe({
        next: (data) => {
          this.measureUnits = data;
        }
      })
    }

    loadGroceryListItems(){
      this.loadedGroceryListItems = false;

      this.dataSourceService.getGroceryListItems(this.groceryListId()).subscribe({
        next: (data) => {
          this.sortItems(data);
          this.loadedGroceryListItems = true;
        }
      })
    }

    addGroceryListItem(item: GroceryListItemModel){
      item.groceryListId = this.groceryListId();
      item.isCompleted = false;

      item.measureUnit = +item.measureUnit;
      
      this.dataSourceService.addRecord<GroceryListItemModel>(item, DataModelsMapper.GroceryListItem).subscribe({
        next: (data) => {
          this.loadGroceryListItems();
        },
        complete: () => {
          this.newGroceryListItemModal.closeModal();
        }
      })
    }

    openNewGroceryListItemModal(){
      this.newGroceryListItemModal.openModal();
    }

    openUpdateGroceryListModal(){
      this.updateGroceryListModal.openModal();
    }

    openConfirmationDeleteModal(){
      this.confirmationDeleteModal.openModal();
    }
  
    openConfirmationSwitchCompleteModal(){
      this.confirmationSwitchCompletedModal.openModal();
    }

    openConfirmationSwitchPinnedModal(){
      this.confirmationSwitchPinnedModal.openModal();
    }

    switchListCompleted(){
      if(this.groceryList.isCompleted){
        this.dataSourceService.switchGroceryListToNotCompleted(this.groceryListId())
          .subscribe({
            next : (data) => {
              this.navigateToRoot();
            }
          });
      }
      else {
        this.dataSourceService.switchGroceryListToCompleted(this.groceryListId())
          .subscribe({
            next : (data) => {
              this.navigateToRoot();
            }
          });
      }
    }

    switchListPinned(){
      if(this.groceryList.isPinned){
        this.dataSourceService.switchGroceryListToNotPinned(this.groceryListId())
          .subscribe({
            next : (data) => {
              this.navigateToRoot();
            }
          });
      }
      else {
        this.dataSourceService.switchGroceryListToPinned(this.groceryListId())
          .subscribe({
            next : (data) => {
              this.navigateToRoot();
            }
          });
      }
    }

    updateList(list: GroceryListModel){
      this.dataSourceService.updateRecord<GroceryListModel>(list, DataModelsMapper.GroceryList).subscribe({
        next : (data) => {
          this.loadGroceryList();
        }
      })
    }

    deleteList(){
      this.dataSourceService.deleteRecord<GroceryListItemModel>(this.groceryListId(), DataModelsMapper.GroceryList)
        .subscribe({
          next : (data) => {
            this.router.navigate(["grocery-lists"]);
          }
        });
    }

    navigateToRoot(){
      this.router.navigate(["grocery-lists"]);
    }

    private sortItems(items: GroceryListItemModel[]){
      this.groceryListItemsCompleted = [];
      this.groceryListItemsNotCompleted = [];
      
      items.forEach(item => {
        if(item.isCompleted){
          this.groceryListItemsCompleted.push(item);
        }
        else {
          this.groceryListItemsNotCompleted.push(item);
        }
      })
    }

    private loadCardButtons(){
      this.cardButtons = [
        {
          text: this.groceryList.isCompleted ? "Re-open" : "Complete",
          icon: this.enumClusterService.getIcons().Completed,
          colorClass: this.groceryList.isCompleted ? this.enumClusterService.getColorClasses().Inactive : this.enumClusterService.getColorClasses().Success,
          onClick: (identifier) => {
            this.openConfirmationSwitchCompleteModal();
          }
        },
        {
          text: this.groceryList.isPinned ? "Remove pin" : "Pin",
          icon: this.enumClusterService.getIcons().Pin,
          colorClass: this.groceryList.isPinned ? this.enumClusterService.getColorClasses().Inactive : this.enumClusterService.getColorClasses().Info,
          onClick: (identifier) => {
            this.openConfirmationSwitchPinnedModal();
          }
        },
        {
          text: "Update",
          icon: this.enumClusterService.getIcons().Completed,
          colorClass: this.enumClusterService.getColorClasses().Update,
          disabled: !this.isCreatedByCurrentUser,
          onClick: (identifier) => {
            this.openUpdateGroceryListModal();
          }
        },
        {
          text: "Delete",
          icon: this.enumClusterService.getIcons().Delete,
          colorClass: this.enumClusterService.getColorClasses().Delete,
          disabled: !this.isCreatedByCurrentUser,
          onClick: (identifier) => {
            this.openConfirmationDeleteModal();
          }
        }
      ]
    }

    private checkIsCreatedByCurrentUser(){
      let user = this.authService.getUserData();
      if(user == undefined || user.userId == undefined || user.userId != this.groceryList.createdBy){
        this.isCreatedByCurrentUser = false;
        return;
      }
      this.isCreatedByCurrentUser = true;
      console.log(this.isCreatedByCurrentUser);
    }
}
