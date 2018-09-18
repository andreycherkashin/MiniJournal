import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { ILiteEvent, LiteEvent } from './event';
import { ArticleCreatedEvent } from './model/events/article-created';
import { ArticleDeletedEvent } from './model/events/article-deleted';
import { CommentAddedEvent } from './model/events/comment-added';
import { CommentDeletedEvent } from './model/events/comment-deleted';
import { MessageService } from './message.service';

const hubUrl = '/hubs/notifications';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  private connection: signalR.HubConnection;

  private readonly onArticleCreated = new LiteEvent<ArticleCreatedEvent>();
  private readonly onArticleDeleted = new LiteEvent<ArticleDeletedEvent>();
  private readonly onCommentAdded = new LiteEvent<CommentAddedEvent>();
  private readonly onCommentDeleted = new LiteEvent<CommentDeletedEvent>();

  constructor(private messageService: MessageService) {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(hubUrl)
      .build();

    this.connection.start().catch(error => this.messageService.add(error));

    this.subscribe();
  }

  public get articleCreated() { return this.onArticleCreated.expose(); }
  public get articleDeleted() { return this.onArticleDeleted.expose(); }
  public get commentAdded() { return this.onCommentAdded.expose(); }
  public get commentDeleted() { return this.onCommentDeleted.expose(); }

  private subscribe(): void {
    this.connection.on('ArticleCreatedEvent', (event: ArticleCreatedEvent) => {
      this.onArticleCreated.trigger(event);
    });

    this.connection.on('ArticleDeletedEvent', (event: ArticleDeletedEvent) => {
      this.onArticleDeleted.trigger(event);
    });

    this.connection.on('CommentAddedEvent', (event: CommentAddedEvent) => {
      this.onCommentAdded.trigger(event);
    });

    this.connection.on('CommentDeletedEvent', (event: CommentDeletedEvent) => {
      this.onCommentDeleted.trigger(event);
    });
  }
}
