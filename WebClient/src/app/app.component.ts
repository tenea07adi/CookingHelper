import { Component, inject } from '@angular/core';
import { TokenDataModel } from './models/data-models/token-data-model';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  private authService = inject(AuthService);

  title = 'WebClient';

  isLoggedIn: boolean = false;
  currentTokenData: TokenDataModel = {} as TokenDataModel;

  ngOnInit(){
    this.checkIfIsUserLoggedIn();

    if(this.isLoggedIn){
      this.loadUserData();
    }
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
}
