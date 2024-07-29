import { Component, input } from '@angular/core';

@Component({
  selector: 'app-loading-screen-component',
  templateUrl: './loading-screen-component.component.html',
  styleUrl: './loading-screen-component.component.css'
})
export class LoadingScreenComponentComponent {
  loaded = input<boolean>()
}
