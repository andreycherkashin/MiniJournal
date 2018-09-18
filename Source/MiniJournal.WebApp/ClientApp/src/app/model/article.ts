import { User } from './user';
import { Comment } from './comment';

export class Article {
  id: number;
  text: string;
  imageId: string;
  user: User;
  comments: Comment[];
}
