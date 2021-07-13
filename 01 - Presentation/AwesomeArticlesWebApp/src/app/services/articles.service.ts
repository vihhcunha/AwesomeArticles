import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable } from "rxjs";
import { catchError, map } from "rxjs/operators";
import { Article } from "../models/article";
import { BaseService } from "./base.service";

@Injectable({
    providedIn: "root"
})
export class ArticlesService extends BaseService {

    constructor(private http: HttpClient) {
        super();
    }

    public add(article: Article): Observable<Article> {
        return this.http
            .post<Article>(this._baseUrl + "api/articles/add", JSON.stringify(article), { headers: this.headersAuth })
            .pipe(
                map(super.extractData),
                catchError(super.serviceError)
            );
    }

    public addArticleLike(idArticle: string): Observable<Article> {
        return this.http.post<Article>(this._baseUrl + "api/articles/" + idArticle + "/add-like", null, { headers: this.headersAuth })
            .pipe(
                map(super.extractData),
                catchError(super.serviceError)
            );;
    }

    public removeArticleLike(idArticle: string): Observable<Article> {
        return this.http.delete<Article>(this._baseUrl + "api/articles/" + idArticle + "/remove-like", { headers: this.headersAuth })
            .pipe(
                map(super.extractData),
                catchError(super.serviceError)
            );;
    }

    public getAllArticles(): Observable<Article[]> {
        return this.http.get<Article[]>(this._baseUrl + "api/articles/all", { headers: this.headersAuth })
            .pipe(
                map(super.extractData),
                catchError(super.serviceError)
            );;
    }

    public getArticle(idArticle: string): Observable<Article> {
        return this.http.get<Article>(this._baseUrl + "api/articles/" + idArticle, { headers: this.headersAuth })
            .pipe(
                map(super.extractData),
                catchError(super.serviceError)
            );;
    }

}
