import { FilterModel } from '@app/models/filter.model';
import { Injectable } from '@angular/core';
import {
  PagedListOfTodoItem,
  CreateTodoCommand,
  TodosClient,
  PatchTodoCommand,
} from '@app/clients';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TodosService {
  baseUrl: string;

  constructor(private todosClient: TodosClient) {}

  getTodos(
    filter: FilterModel,
    pageIndex: number,
    pageSize: number
  ): Observable<PagedListOfTodoItem> {
    return this.todosClient.getTodos(
      filter.searchTerm,
      filter.showCompletedTodos,
      filter.showIncompleteTodos,
      pageIndex,
      pageSize
    );
  }

  createTodo(todo: CreateTodoCommand): Observable<number> {
    return this.todosClient.createTodo(todo);
  }

  patchTodo(id: number, todo: PatchTodoCommand): Observable<void> {
    return this.todosClient.patchTodo(id, todo);
  }

  deleteTodo(id: number): Observable<void> {
    return this.todosClient.deleteTodo(id);
  }
}
