import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";
import { Token } from "../models/token";
import { HttpClient, HttpErrorResponse, HttpHeaders } from "@angular/common/http";
import { throwError } from "rxjs";

@Injectable({
    providedIn: "root"
})
export class BaseService {
    public _baseUrl: string = environment.baseUrl;

    private _token!: Token;

    set token(token: Token) {
        this._token = token;
        if (this._token == null) {
            localStorage.removeItem("token");
        }
        else {
            var encodedToken = btoa(JSON.stringify(this._token))
            localStorage.setItem("token", encodedToken);
        }
    }

    get token(): Token {
        var encodedToken = localStorage.getItem("token");
        if (encodedToken !== null) {
            var token = atob(encodedToken);
            this._token = JSON.parse(token);
            return this._token;
        }
        return <any>null;
    }

    get headersAnonymous(): HttpHeaders {
        return new HttpHeaders().set('content-type', 'application/json');
    }

    get headersAuth(): HttpHeaders {
        return new HttpHeaders().set('content-type', 'application/json').append('Authorization', 'Bearer ' + this.token.tokenJWT);
    }

    protected extractData(response: any) {
        return response.data || {};
    }

    protected serviceError(response: Response | any) {
        let customError: string[] = [];
        let customResponse = { error: { errors: [] }}

        if (response instanceof HttpErrorResponse) {

            if (response.statusText === "Unknown Error") {
                customError.push("Ocorreu um erro desconhecido");
                response.error.errors = customError;
            }
        }
        if (response.status === 500) {
            customError.push("Ocorreu um erro no processamento, tente novamente mais tarde ou contate o nosso suporte.");
            
            // Erros do tipo 500 não possuem uma lista de erros
            // A lista de erros do HttpErrorResponse é readonly                
            customResponse.error.errors = customError;
            return throwError(customResponse);
        }

        console.error(response);
        return throwError(response);
    }
}