import { Article } from "./article";
import { User } from "./user";

export interface ArticleLike {
    idArticlesLike: string;
    idArticle: string;
    idUser: string;
    registrationDate: string;
    article: Article;
    user: User;
}