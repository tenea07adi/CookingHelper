import { Component, inject } from '@angular/core';
import { TokenDataModel } from './models/data-models/token-data-model';
import { AuthService } from './services/auth.service';
import { DataSourceService } from './services/data-source.service';
import { AppInfoModel } from './models/data-models/app-info-model';
import { EnumClusterService } from './services/enum-cluster.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public enumClusterService = inject(EnumClusterService);
  
  private authService = inject(AuthService);

  private appInfoService = inject(DataSourceService);

  title = 'WebClient';

  displayNavigation: boolean = true;

  public appInfo : AppInfoModel | undefined;

  isLoggedIn: boolean = false;
  currentTokenData: TokenDataModel = {} as TokenDataModel;

  ngOnInit(){
    this.checkIfIsUserLoggedIn();

    if(this.isLoggedIn){
      this.loadUserData();
    }

    this.getAppInfo();
  }

  checkIfIsUserLoggedIn(){
    this.isLoggedIn = this.authService.isLoggedIn();
  }

  loadUserData(){
    this.currentTokenData = this.authService.getUserData();
  }

  logOut(){
    this.authService.logOut();
  }

  getAppInfo(){
    this.appInfoService.getAppInfo().subscribe({next: (data) => {
      this.appInfo = data;
    }})
  }

  toggleNavigation(){
    this.displayNavigation = !this.displayNavigation;
  }
}
