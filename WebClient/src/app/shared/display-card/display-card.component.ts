import { Component, inject, input } from '@angular/core';
import { CardButtonModel } from './model/card-button.model';
import { EnumClusterService } from 'src/app/services/enum-cluster.service';

@Component({
  selector: 'app-display-card',
  templateUrl: './display-card.component.html',
  styleUrl: './display-card.component.css'
})
export class DisplayCardComponent {
  public enumClusterService = inject(EnumClusterService);

  elementIdentifier = input.required<string>();

  isCollapsable = input<boolean>(false);
  isDefaultCollapsed = input<boolean>(false);

  titleText = input.required<string>();
  titleClasses = input<string[]>([]);

  buttons = input<CardButtonModel[]>([]);
  fullSizeButtonsGrid = input<boolean>(false);

  currentCollapseState : boolean = false;

  ngOnInit(){
    this.currentCollapseState = this.isDefaultCollapsed();
  }

  get currentStateClass() : string {
    return this.currentCollapseState ? "collapsedBody" : "notCollapsedBody";
  }

  switchState(){
    this.currentCollapseState = !this.currentCollapseState;
  }
}
