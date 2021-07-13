import { ArticleLike } from "./articleLike";

export interface Article {
    idArticle: string;
    title: string;
    content: string;
    registrationDate: string;
    totalLikes: number;
    userLiked: boolean;
    likes: ArticleLike[];
}