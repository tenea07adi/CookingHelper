import { inject, Injectable } from "@angular/core";
import { CanActivate, CanActivateChild, CanLoad, Router } from "@angular/router";
import { AuthService } from "../services/auth.service";

@Injectable({providedIn: 'root'})
export class AuthGuard implements CanActivate, CanActivateChild, CanLoad {

    private authService  = inject(AuthService);
    private router  = inject(Router);

    canActivate(): boolean {
        return this.checkAuth();
      }
    
    canActivateChild(): boolean {
        return this.checkAuth();
      }
    
    canLoad(): boolean {
        return this.checkAuth();
      }
    
    private checkAuth(): boolean {
        if (this.authService.isLoggedIn()) {
            return true;
        } else {
            // Redirect to the login page if the user is not authenticated
            this.router.navigate(['/auth']);
            return false;
        }
    }
}