import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Article } from 'src/app/models/article';
import { ArticlesService } from 'src/app/services/articles.service';

@Component({
  selector: 'app-articles-list',
  templateUrl: './articles-list.component.html'
})
export class ArticlesListComponent implements OnInit {

  public articles: Article[];

  constructor(private articleService: ArticlesService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.obterArtigos();
  }

  obterArtigos() {
    this.articleService.getAllArticles()
      .subscribe(
        json => this.articles = json,
        e => {
          this.toastr.error(e.error.message);
        }
      );
  }

  visualizarArtigo(article: Article){
    this.router.navigate(['/articles/' + article.idArticle]);
  }

}
