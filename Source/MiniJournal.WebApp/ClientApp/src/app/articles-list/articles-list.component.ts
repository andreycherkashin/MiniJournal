import { Component, OnInit } from '@angular/core';
import { Article } from '../model/article'
import { Comment } from '../model/comment'
import { User } from '../model/user'
import { ArticleService } from "../article.service"

@Component({
  selector: 'app-articles-list',
  templateUrl: './articles-list.component.html',
  styleUrls: ['./articles-list.component.css']
})
export class ArticlesListComponent implements OnInit {  

  private articles: Article[];

  constructor(private articleService: ArticleService ) {    
  }

  ngOnInit() {
    this.getArticles();
  }

  getArticles(): void {
    this.articleService.getArticles()
      .subscribe(articles => this.articles = articles);
  }
}
