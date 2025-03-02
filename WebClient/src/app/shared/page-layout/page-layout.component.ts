import { Component, inject, input } from '@angular/core';
import { RouteOptionModel } from './models/route-option.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-page-layout',
  templateUrl: './page-layout.component.html',
  styleUrl: './page-layout.component.css'
})
export class PageLayoutComponent {
  private router = inject(Router);

  pageTitle = input.required<string>();
  pageDetails = input<string>();

  routeOptions = input<RouteOptionModel[]>([]);

  get routes() : RouteOptionModel[]{
    let result = [];

    result.push({
      name: "Home",
      route: ""
    });

    result = [... result, ... this.routeOptions()];

    return result;
  }

  navigate(route: RouteOptionModel) {
    if(route == undefined){
      return;
    }

    this.router.navigate([route.route]);
  }
}
