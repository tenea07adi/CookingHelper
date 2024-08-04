import { inject, Injectable } from "@angular/core";
import { RegisterModel } from "../models/data-models/register-model";
import { LogInModel } from "../models/data-models/login-model";
import { HttpClient } from "@angular/common/http";
import { AppConfigService } from "./app-config.service";
import { Router } from "@angular/router";
import { TokenDataModel } from "../models/data-models/token-data-model";

@Injectable({providedIn: "root"})
export class AuthService{

    private httpClient = inject(HttpClient);
    private appConfigService = inject(AppConfigService);
    private router = inject(Router);

    private get restApiUrl() {
        return this.appConfigService.getRestApiUrl();
      }

    isLoggedIn(): boolean{
        let token = this.readJwtTokenFromMemory();

        if(token == undefined){
            return false;
        }

        let decodedToken = JSON.parse(window.atob(token.split('.')[1])) as TokenDataModel;

        let expDate = new Date(decodedToken.exp * 1000);

        if(new Date(expDate) < new Date()){
            this.deleteJwtTokenFromMemory();
            return false;
        }

        return true;
    }

    logIn(logInData: LogInModel){
        let url = this.restApiUrl + "/auth/login";
        this.httpClient.post<any>(url, logInData).subscribe({
            next: (data) => {
                this.writeJwtTokenToMemory(data["jwtToken"]);
                this.router.navigate(['']);
                this.reloadPage();
            }
        })
    }

    logOut(){
        this.deleteJwtTokenFromMemory();
        this.router.navigate(['/auth']);
        this.reloadPage();
    }

    register(registerData: RegisterModel){
        let url = this.restApiUrl + "/auth/register";
        this.httpClient.post<any>(url, registerData).subscribe({
            next: (data) => {
                this.logIn({"email": registerData.email, "password": registerData.password});
            }
        })
    }

    getUserData(): TokenDataModel{
        let token = this.readJwtTokenFromMemory();
        return JSON.parse(window.atob(token.split('.')[1])) as TokenDataModel;
    }

    getJwtToken(): string{
        return this.readJwtTokenFromMemory();
    }

    private readJwtTokenFromMemory() : string{
        return localStorage.getItem("AuthToken") as string;
    }

    private writeJwtTokenToMemory(token: string){
        localStorage.setItem("AuthToken", token);
    }

    private deleteJwtTokenFromMemory(){
        localStorage.removeItem("AuthToken");
    }

    private reloadPage() {
        setTimeout(()=>{
          window.location.reload();
        }, 100);
    }
}