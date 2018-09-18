import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppMaterialModule } from "./app-material.module";
import { FlexLayoutModule, BREAKPOINT } from "@angular/flex-layout";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { CommentComponent } from './comment/comment.component';
import { ArticleComponent } from './article/article.component';
import { ArticlesListComponent } from './articles-list/articles-list.component';
import { NewArticleComponent } from './new-article/new-article.component';
import { NewCommentComponent } from './new-comment/new-comment.component';
import { AppRoutingModule } from './/app-routing.module';
import { MessagesComponent } from './messages/messages.component';
import { CommentsListComponent } from './comments-list/comments-list.component';

const PRINT_BREAKPOINTS = [{
  alias: 'xs.print',
  suffix: 'XsPrint',
  mediaQuery: 'print and (max-width: 297px)',
  overlapping: false
}];

@NgModule({
  declarations: [
    AppComponent,
    CommentComponent,
    ArticleComponent,
    ArticlesListComponent,
    NewArticleComponent,
    NewCommentComponent,
    MessagesComponent,
    CommentsListComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    AppMaterialModule,
    FlexLayoutModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [{ provide: BREAKPOINT, useValue: PRINT_BREAKPOINTS, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
