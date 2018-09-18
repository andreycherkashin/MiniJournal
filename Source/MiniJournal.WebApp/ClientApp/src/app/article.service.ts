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

  deleteArticle(articleId: number): void {
  }

  createArticle(text: string, user: string): void {
    const article = {
      text: text,
      userName: user
    };

    const url = `https://localhost:5001/${articlesUrl}`;

    this.http.post(articlesUrl, article, httpOptions).pipe(
      tap(() => this.log(`added article`)),
      catchError(this.handleError('createArticle'))
    )
      .subscribe(response => this.log(`add article response: ${response}`));
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

  addComment(articleId: number, user: string, commentText: string): void {
  }

  deleteComment(articleId: number, commentId: number): void {
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
