import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ArticlesListComponent } from "./articles-list/articles-list.component"
import { NewArticleComponent } from "./new-article/new-article.component"
import { ArticleComponent } from "./article/article.component"
import { CommentComponent } from "./comment/comment.component"

const routes: Routes = [
  { path: '', redirectTo: '/articles', pathMatch: 'full' },
  { path: 'articles', component: ArticlesListComponent },
  { path: 'article', component: NewArticleComponent },
  { path: 'article/:id', component: ArticleComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
