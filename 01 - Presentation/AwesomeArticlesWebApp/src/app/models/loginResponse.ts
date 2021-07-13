import { Token } from "./token";
import { User } from "./user";

export interface LoginResponse {
    user: User;
    token: Token;
}