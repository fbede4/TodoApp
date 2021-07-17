import { Component, EventEmitter, Output } from '@angular/core';
import { CreateTodoCommand } from '@app/clients';

@Component({
  selector: 'app-new-todo',
  templateUrl: './new-todo.component.html',
  styleUrls: ['./new-todo.component.scss'],
})
export class NewTodoComponent {
  newTodo: CreateTodoCommand = new CreateTodoCommand();

  @Output()
  add: EventEmitter<CreateTodoCommand> = new EventEmitter();

  addTodo(): void {
    if (this.newTodo.description) {
      this.add.emit(this.newTodo);
      this.newTodo = new CreateTodoCommand();
    }
  }
}
