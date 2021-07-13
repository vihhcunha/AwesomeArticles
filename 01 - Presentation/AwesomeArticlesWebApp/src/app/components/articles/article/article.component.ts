import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Article } from 'src/app/models/article';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html'
})
export class ArticleComponent implements OnInit {

  public article: Article;
  constructor(private articleService: ArticlesService, private toastr: ToastrService, private router: Router, private route: ActivatedRoute,) { }

  ngOnInit(): void {
    this.route.params.subscribe(param => this.loadArticle(param.id));
  }

  loadArticle(id: string): void {
    this.articleService.getArticle(id)
      .subscribe(
        json => this.article = json,
        e => {
          this.toastr.error(e.error.message);
        }
      );
  }

  like() {
    this.articleService.addArticleLike(this.article.idArticle)
      .subscribe(
        json => this.article = json,
        e => {
          this.toastr.error(e.error.message);
        }
      );
  }

  removeLike() {
    this.articleService.removeArticleLike(this.article.idArticle)
      .subscribe(
        json => this.article = json,
        e => {
          this.toastr.error(e.error.message);
        }
      );
  }

}
