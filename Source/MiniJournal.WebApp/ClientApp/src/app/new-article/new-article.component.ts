import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroupDirective, NgForm, Validators, FormGroup } from '@angular/forms';
import { Article } from '../model/article';
import { User } from '../model/user';
import { ArticleService } from '../article.service';

@Component({
  selector: 'app-new-article',
  templateUrl: './new-article.component.html',
  styleUrls: ['./new-article.component.css']
})
export class NewArticleComponent implements OnInit {

  articleForm = new FormGroup({
    userName: new FormControl('', [Validators.required]),
    articleText: new FormControl('', [Validators.required])
  });

  constructor(private articlesService: ArticleService) {
  }

  ngOnInit() {
  }

  submit(): void {
    if (this.articleForm.valid) {
      const user = this.articleForm.value.userName;
      const text = this.articleForm.value.articleText;
      this.articlesService.createArticle(text, user)
        .subscribe(_ => this.resetForm());
    }
  }

  resetForm(): void {
    this.articleForm.reset();
  }
}
