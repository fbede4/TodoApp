import { Component, Input, Output, EventEmitter } from '@angular/core';
import { TodoItem } from '@app/clients';

@Component({
  selector: 'app-todo-list-item',
  templateUrl: './todo-list-item.component.html',
  styleUrls: ['./todo-list-item.component.scss'],
})
export class TodoListItemComponent {
  isEditing: boolean;

  @Input() todo: TodoItem;

  @Output()
  delete: EventEmitter<TodoItem> = new EventEmitter();

  @Output()
  edit: EventEmitter<TodoItem> = new EventEmitter();

  @Output()
  toggleIsComplete: EventEmitter<TodoItem> = new EventEmitter();

  onToggleIsComplete(): void {
    this.toggleIsComplete.emit(this.todo);
  }

  onEditTodoDescription(): void {
    this.toggleIsEditing();
    this.edit.emit(this.todo);
  }

  deleteTodo(): void {
    this.delete.emit(this.todo);
  }

  toggleIsEditing(): void {
    this.isEditing = !this.isEditing;
  }
}
