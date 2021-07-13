import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddArticleComponent } from './components/articles/add-article/add-article.component';
import { ArticleComponent } from './components/articles/article/article.component';
import { ArticlesListComponent } from './components/articles/articles-list/articles-list.component';
import { LoginComponent } from './components/users/login/login.component';
import { SignupComponent } from './components/users/signup/signup.component';
import { RouteGuard } from './utils/route-guard';

const routes: Routes = [
  { path: '', redirectTo: '/articles', pathMatch: 'full'},
  { path: 'login', component: LoginComponent },
  { path: 'signup', component: SignupComponent },
  { path: 'articles', component: ArticlesListComponent, canActivate: [RouteGuard] },
  { path: 'articles/:id', component: ArticleComponent, canActivate: [RouteGuard] },
  { path: 'add-article', component: AddArticleComponent, canActivate: [RouteGuard] },
  { path: '**', redirectTo: '/articles' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
