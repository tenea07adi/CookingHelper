import { Component, inject, input, output, ViewChild } from '@angular/core';
import { PreparationStepModel } from 'src/app/models/data-models/preparation-step-model';
import { DataModelsMapper } from 'src/app/models/ModelMappers/data-models-mapper';
import { DataSourceService } from 'src/app/services/data-source.service';
import { EnumClusterService } from 'src/app/services/enum-cluster.service';
import { ConfirmationModalComponent } from 'src/app/shared/confirmation-modal/confirmation-modal.component';

@Component({
  selector: 'app-preparation-step-preview',
  templateUrl: './preparation-step-preview.component.html',
  styleUrl: './preparation-step-preview.component.css'
})
export class PreparationStepPreviewComponent {
  private dataSourceService = inject(DataSourceService);

  public enumClusterService = inject(EnumClusterService);
  
  preparationStep = input.required<PreparationStepModel>();

  onChanged = output<number>();

  @ViewChild("confirmationRemoveModal") confirmationRemoveModal! : ConfirmationModalComponent;
  @ViewChild("confirmationMoveUpModal") confirmationMoveUpModal! : ConfirmationModalComponent;
  @ViewChild("confirmationMoveDownModal") confirmationMoveDownModal! : ConfirmationModalComponent;
  

  onRemove(){
    this.dataSourceService.deleteRecord(this.preparationStep().id, DataModelsMapper.PreparationSteps).subscribe({
      next: (data) => {
        this.onChanged.emit(this.preparationStep().id);
      }
    })
  }

  onMoveUp(){
    this.dataSourceService.moveUpPreparationSteps(this.preparationStep().id).subscribe({
      next: (data) => {
        this.onChanged.emit(this.preparationStep().id);
      }
    })
  }

  onMoveDown(){
    this.dataSourceService.moveDownPreparationSteps(this.preparationStep().id).subscribe({
      next: (data) => {
        this.onChanged.emit(this.preparationStep().id);
      }
    })
  }

  openConfirmationRemoveModal(){
    this.confirmationRemoveModal.openModal();
  }

  openConfirmationMoveUpModal(){
    this.confirmationMoveUpModal.openModal();
  }

  openConfirmationMoveDownModal(){
    this.confirmationMoveDownModal.openModal();
  }
}
