import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { ArticleService } from '../article.service';
import { NotificationService } from '../notification.service';
import { Comment } from '../model/comment';

@Component({
  selector: 'app-comments-list',
  templateUrl: './comments-list.component.html',
  styleUrls: ['./comments-list.component.css']
})
export class CommentsListComponent implements OnInit {
  articleId: number;
  comments: Comment[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private location: Location,
    private articleService: ArticleService,
    private notificationService: NotificationService) { }

  ngOnInit() {
    this.articleId = +this.route.snapshot.paramMap.get('articleId');
    if (!this.articleId) {
      this.articleId = +this.route.snapshot.paramMap.get('id');
    }
    this.getComments();

    this.notificationService.articleDeleted.on(event => {
      if (this.articleId === event.articleId) {
        this.goBack();
      }
    });

    this.notificationService.commentAdded.on(event => {
      if (this.articleId === event.articleId) {
        this.articleService.getComment(event.articleId, event.commentId)
          .subscribe(comment => {
            this.comments.push(comment);
          });
      }
    });

    this.notificationService.commentDeleted.on(event => {
      if (this.articleId === event.articleId) {
        this.comments = this.comments.filter(x => x.id !== event.commentId);
      }
    });
  }

  getComments() {
    this.articleService.getComments(this.articleId)
      .subscribe(comments => this.comments = comments);
  }

  goBack() {
    this.router.navigateByUrl('/');
  }
}
