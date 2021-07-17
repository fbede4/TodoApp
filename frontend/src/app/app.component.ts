import { PatchTodoCommand } from './clients/index';
import { FilterModel } from './models/filter.model';
import {
  TodoItem,
  PagedListOfTodoItem,
  CreateTodoCommand,
} from '@app/clients/index';
import { TodosService } from './services/todos.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  filter: FilterModel = {
    showCompletedTodos: true,
    showIncompleteTodos: true,
    searchTerm: '',
  };
  data: PagedListOfTodoItem;

  constructor(private todosService: TodosService) {}

  ngOnInit(): void {
    this.loadTodos();
  }

  loadTodos(pageIndex: number = 0, pageSize: number = 10): void {
    this.todosService
      .getTodos(this.filter, pageIndex, pageSize)
      .subscribe((todos) => {
        this.data = todos;
      });
  }

  onAddTodo(todo: CreateTodoCommand): void {
    this.todosService.createTodo(todo).subscribe(() => {
      this.loadTodos();
    });
  }

  onFilterChanged(filter: FilterModel): void {
    this.filter = filter;
    this.loadTodos();
  }

  onEditTodoDescription(todo: TodoItem): void {
    const command = new PatchTodoCommand({
      description: todo.description,
    });

    this.todosService.patchTodo(todo.id, command).subscribe(() => {
      this.loadTodos();
    });
  }

  onToggleTodoIsComplete(todo: TodoItem): void {
    const command = new PatchTodoCommand({
      isComplete: !todo.isComplete,
    });

    this.todosService.patchTodo(todo.id, command).subscribe(() => {
      this.loadTodos();
    });
  }

  onDeleteTodo(todo: TodoItem): void {
    this.todosService.deleteTodo(todo.id).subscribe(() => {
      this.loadTodos();
    });
  }
}
