import { PagedListOfTodoItem } from '@app/clients/index';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { TodoItem } from '@app/clients';

@Component({
  selector: 'app-todo-list',
  templateUrl: './todo-list.component.html',
  styleUrls: ['./todo-list.component.scss'],
})
export class TodoListComponent {
  @Input()
  data: PagedListOfTodoItem;

  @Output()
  delete: EventEmitter<TodoItem> = new EventEmitter();

  @Output()
  editTodoDescription: EventEmitter<TodoItem> = new EventEmitter();

  @Output()
  toggleIsComplete: EventEmitter<TodoItem> = new EventEmitter();

  onToggleIsComplete(todo: TodoItem): void {
    this.toggleIsComplete.emit(todo);
  }

  onEditTodoDescription(todo: TodoItem): void {
    this.editTodoDescription.emit(todo);
  }

  onDeleteTodo(todo: TodoItem): void {
    this.delete.emit(todo);
  }
}
