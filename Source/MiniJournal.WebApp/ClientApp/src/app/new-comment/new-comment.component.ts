import { Component, OnInit } from '@angular/core';
import { ArticleService } from '../article.service';
import { FormControl, FormGroupDirective, NgForm, Validators, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-new-comment',
  templateUrl: './new-comment.component.html',
  styleUrls: ['./new-comment.component.css']
})
export class NewCommentComponent implements OnInit {

  articleId: number;

  commentForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    commentText: new FormControl('', [Validators.required])
  });

  constructor(
    private articlesService: ArticleService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.articleId = +this.route.snapshot.paramMap.get('id');
  }

  submit(): void {
    if (this.commentForm.valid) {
      const user = this.commentForm.value.userName;
      const text = this.commentForm.value.commentText;
      this.articlesService.addComment(this.articleId, user, text)
        .subscribe(_ => this.resetForm());
    }
  }

  resetForm(): void {
    this.commentForm.reset();
  }
}
