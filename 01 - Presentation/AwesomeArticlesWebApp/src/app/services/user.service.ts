import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { LoginResponse } from "../models/loginResponse";
import { Token } from "../models/token";
import { User } from "../models/user";
import { BaseService } from "./base.service";
import { catchError, map } from "rxjs/operators";

@Injectable({
    providedIn: "root"
})
export class UserService extends BaseService {

    public login: LoginResponse;
    private _User: User;
  
    get User(): User {
      var encodedUser = localStorage.getItem("User");
      if(encodedUser !== null){
        var User = atob(encodedUser);
        this._User = JSON.parse(User);
        return this._User;
      }
      return null;
    }
  
    set User(User: User) {
      this._User = User;
      if(this._User == null){
        localStorage.removeItem("User");
      }
      else{
        var encodedUser = btoa(JSON.stringify(this._User))
        localStorage.setItem("User", encodedUser);
      }
    }
  
    constructor(private http: HttpClient, private router: Router) {
      super();
    }
  
    public add(User: User): Observable<User> {
      return this.http
        .post<User>(this._baseUrl + "api/user/add", JSON.stringify(User),{ headers: this.headersAnonymous })
        .pipe(
          map(super.extractData),
          catchError(super.serviceError)
        );
    }
  
    public fazerLogin(User: User): Observable<LoginResponse> {
      return this.http.post<LoginResponse>(this._baseUrl + "api/user/login", JSON.stringify(User), { headers: this.headersAnonymous })
        .pipe(
          map(super.extractData),
          catchError(super.serviceError)
        );;
    }
  
    public salvarLogin(User: User, token: Token) {
      this.User = User;
      this.token = token;
    }

    public logout() {
      this.User = null;
      this.token = null;
      this.router.navigate(['/users/login']);
    }
  
    public userIsAuthenticated(): boolean {
  
      if (!this.User) {
        return false;
      }
  
      if (!this.token.dataExpiracao) {
        this.logout();
        return false;
      }
  
      var DateNumber: number = Date.now();
      var DateNow: Date = new Date(DateNumber);
  
      var DateToken: Date = new Date(this.token.dataExpiracao);
  
      if (DateToken <= DateNow) {
        this.logout();
        return false;
      }
  
      return true;
    }
  }
  