import { Component, inject, input, output } from '@angular/core';
import { PreparationStepModel } from 'src/app/models/data-models/preparation-step-model';
import { DataModelsMapper } from 'src/app/models/ModelMappers/data-models-mapper';
import { DataSourceService } from 'src/app/services/data-source.service';

@Component({
  selector: 'app-preparation-step-preview',
  templateUrl: './preparation-step-preview.component.html',
  styleUrl: './preparation-step-preview.component.css'
})
export class PreparationStepPreviewComponent {
  private dataSourceService = inject(DataSourceService);
  
  preparationStep = input.required<PreparationStepModel>();

  onChanged = output<number>();

  displayRemoveModal: boolean = false;
  displayMoveUpModal: boolean = false;
  displayMoveDownModal: boolean = false;

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
}
