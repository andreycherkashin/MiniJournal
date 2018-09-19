import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { ArticleService } from '../article.service';
import { Article } from '../model/article';
import { NotificationService } from '../notification.service';
import { User } from '../model/user';

@Component({
  selector: 'app-article',
  templateUrl: './article.component.html',
  styleUrls: ['./article.component.css']
})
export class ArticleComponent implements OnInit {

  article: Article;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private location: Location,
    private articleService: ArticleService,
    private notificationService: NotificationService) {
    this.article = new Article();
    this.article.user = new User();
  }

  ngOnInit() {
    this.getArticle();

    this.notificationService.articleDeleted.on(event => {
      if (this.article.id === event.articleId) {
        this.goBack();
      }
    });

    this.notificationService.commentAdded.on(event => {
      if (this.article.id === event.articleId) {
        this.articleService.getComment(event.articleId, event.commentId)
          .subscribe(comment => {
            this.article.comments.push(comment);
          });
      }
    });

    this.notificationService.commentDeleted.on(event => {
      if (this.article.id === event.articleId) {
        this.article.comments = this.article.comments.filter(x => x.id !== event.commentId);
      }
    });
  }

  getArticle(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.articleService.getArticle(id)
      .subscribe(article => this.article = article);
  }

  delete(): void {
    this.articleService.deleteArticle(this.article.id)
      .subscribe(_ => this.goBack());
  }

  goBack() {
    this.router.navigateByUrl('/');
  }
}
