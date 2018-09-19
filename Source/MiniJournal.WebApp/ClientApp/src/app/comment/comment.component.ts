import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Location } from '@angular/common';
import { ArticleService } from '../article.service';
import { NotificationService } from '../notification.service';
import { Comment } from '../model/comment';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  articleId: number;
  commentId: number;
  comment: Comment;

  constructor(private route: ActivatedRoute,
    private router: Router,
    private location: Location,
    private articleService: ArticleService,
    private notificationService: NotificationService) { }

  ngOnInit() {
    this.articleId = +this.route.snapshot.paramMap.get('articleId');
    this.commentId = +this.route.snapshot.paramMap.get('id');

    this.getComment();

    this.notificationService.commentDeleted.on(event => {
      if (this.articleId === event.articleId && this.commentId === event.commentId) {
        this.goBack();
      }
    });

    this.notificationService.articleDeleted.on(event => {
      if (this.articleId === event.articleId) {
        this.goBack();
      }
    });
  }

  getComment() {
    this.articleService.getComment(this.articleId, this.commentId)
      .subscribe(comment => this.comment = comment);
  }

  delete() {
    this.articleService.deleteComment(this.articleId, this.comment.id)
      .subscribe(_ => this.goBack());
  }

  goBack() {
    this.router.navigateByUrl('/');
  }
}
