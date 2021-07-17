import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { TodoItem } from '@app/clients';

@Component({
  selector: 'app-todo-list-item',
  templateUrl: './todo-list-item.component.html',
  styleUrls: ['./todo-list-item.component.scss'],
})
export class TodoListItemComponent implements OnInit {
  isEditing: boolean;
  form: FormGroup;

  @Input() todo: TodoItem;

  @Output()
  delete: EventEmitter<TodoItem> = new EventEmitter();

  @Output()
  edit: EventEmitter<TodoItem> = new EventEmitter();

  @Output()
  toggleIsComplete: EventEmitter<TodoItem> = new EventEmitter();

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      description: [this.todo.description, [Validators.required]],
    });
  }

  onToggleIsComplete(): void {
    this.toggleIsComplete.emit(this.todo);
  }

  onEditTodoDescription(): void {
    if (!this.form.controls.description.errors?.required) {
      this.todo.description = this.form.controls.description.value;
      this.toggleIsEditing();
      this.edit.emit(this.todo);
    }
  }

  deleteTodo(): void {
    this.delete.emit(this.todo);
  }

  toggleIsEditing(): void {
    this.isEditing = !this.isEditing;
  }
}
