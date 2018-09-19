import { Injectable } from '@angular/core';

import { Article } from './model/article';
import { Comment } from './model/comment';
import { User } from './model/user';

import { Observable, of } from 'rxjs';
import { MessageService } from './message.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, map, tap } from 'rxjs/operators';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

const articlesUrl = 'api/articles';
const commentsUrl = 'comments';


@Injectable({
  providedIn: 'root'
})
export class ArticleService {
  constructor(
    private http: HttpClient,
    private messageService: MessageService) { }

  getArticles(): Observable<Article[]> {
    return this.http.get<Article[]>(articlesUrl).pipe(
      tap(articles => this.log('fetched articles')),
      catchError(this.handleError('getArticles', []))
    );
  }

  getArticle(id: number): Observable<Article> {
    const url = `${articlesUrl}/${id}`;
    return this.http.get<Article>(url).pipe(
      tap(article => this.log(`fetched article id=${id}`)),
      catchError(this.handleError<Article>(`gerArticle id=${id}`))
    );
  }

  deleteArticle(id: number): Observable<any> {
    const url = `${articlesUrl}/${id}`;
    return this.http.delete(url, httpOptions).pipe(
      tap(() => this.log(`deleted article`)),
      catchError(this.handleError('deleteArticle'))
    );
  }

  createArticle(text: string, user: string): Observable<any> {
    const article = {
      text: text,
      userName: user
    };

    return this.http.post(articlesUrl, article, httpOptions).pipe(
      tap(() => this.log(`added article`)),
      catchError(this.handleError('createArticle'))
    );
  }

  getComments(articleId: number): Observable<Comment[]> {
    const url = `${articlesUrl}/${articleId}/${commentsUrl}`;
    return this.http.get<Comment[]>(url).pipe(
      tap(comments => this.log(`fetched comments articleId=${articleId}`)),
      catchError(this.handleError('getComments', []))
    );
  }

  getComment(articleId: number, commentId: number): Observable<Comment> {
    const url = `${articlesUrl}/${articleId}/${commentsUrl}/${commentId}`;
    return this.http.get<Comment>(url).pipe(
      tap(comment => this.log(`fetched comment id=${commentId}`)),
      catchError(this.handleError<Comment>(`getComment  id=${commentId}`))
    );
  }

  addComment(articleId: number, user: string, commentText: string): Observable<any> {
    const comment = {
      text: commentText,
      userName: user
    };

    const url = `${articlesUrl}/${articleId}/${commentsUrl}`;

    return this.http.post(url, comment, httpOptions).pipe(
      tap(() => this.log(`added comment`)),
      catchError(this.handleError('addComment'))
    );
  }

  deleteComment(articleId: number, commentId: number): Observable<any> {
    const url = `${articlesUrl}/${articleId}/${commentsUrl}/${commentId}`;

    return this.http.delete(url, httpOptions).pipe(
      tap(() => this.log(`deleted comment`)),
      catchError(this.handleError('deleteComment'))
    );
  }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  private log(message: string) {
    this.messageService.add('ArticleService: ' + message);
  }
}
