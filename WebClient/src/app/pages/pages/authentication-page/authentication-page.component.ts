import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';
import { LogInModel } from 'src/app/models/data-models/login-model';
import { RegisterModel } from 'src/app/models/data-models/register-model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-authentication-page',
  templateUrl: './authentication-page.component.html',
  styleUrl: './authentication-page.component.css'
})
export class AuthenticationPageComponent {

  private authService = inject(AuthService);
  private router = inject(Router);
  
  newAccount: boolean = false;

  logInData: LogInModel = {} as LogInModel;
  registerData: RegisterModel = {} as RegisterModel;

  ngOnInit(){
    if(this.authService.isLoggedIn()){
      this.router.navigate(['']);
    }
  }

  onLogIn(){
    this.authService.logIn(this.logInData);
  }

  onRegister(){
    this.authService.register(this.registerData);
  }
}
