import { Component, OnInit } from '@angular/core';
import { Article } from '../model/article';
import { Comment } from '../model/comment';
import { User } from '../model/user';
import { ArticleService } from '../article.service';
import { NotificationService } from '../notification.service';

@Component({
  selector: 'app-articles-list',
  templateUrl: './articles-list.component.html',
  styleUrls: ['./articles-list.component.css']
})
export class ArticlesListComponent implements OnInit {

  articles: Article[];

  constructor(
    private articleService: ArticleService,
    private notificationService: NotificationService) {
  }

  ngOnInit() {
    this.getArticles();

    this.notificationService.articleCreated.on(event => {
      this.articleService.getArticle(event.articleId)
        .subscribe(article => this.articles.push(article));
    });

    this.notificationService.articleDeleted.on(event => {
      this.articles = this.articles.filter(x => x.id !== event.articleId);
    });
  }

  getArticles(): void {
    this.articleService.getArticles()
      .subscribe(articles => this.articles = articles);
  }

  truncateText(text: string, length: number): string {
    if (text && text.length > length) {
      return text.substr(0, length) + '...';
    }

    return text;
  }
}
