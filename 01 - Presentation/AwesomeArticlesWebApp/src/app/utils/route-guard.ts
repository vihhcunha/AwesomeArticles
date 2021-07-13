import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { UserService } from "../services/user.service";

@Injectable({
    providedIn: 'root'
  })
  export class RouteGuard implements CanActivate {
  
    constructor(private router: Router, private userService: UserService) {
  
    }
  
    canActivate(route: import("@angular/router").ActivatedRouteSnapshot, state: import("@angular/router").RouterStateSnapshot): boolean {
  
      if (this.userService.userIsAuthenticated()) {
        return true;
      }
  
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      return false;
    }
  }