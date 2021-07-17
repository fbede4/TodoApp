import { PagedListOfTodoItem } from '@app/clients/index';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PagedRequest } from '@app/models/paging.models';

@Component({
  selector: 'app-todo-list-footer',
  templateUrl: './todo-list-footer.component.html',
  styleUrls: ['./todo-list-footer.component.scss'],
})
export class TodoListFooterComponent {
  @Input()
  data: PagedListOfTodoItem;

  @Output()
  reloadTable: EventEmitter<PagedRequest> = new EventEmitter();

  loadData(pageIndex: number, pageSize: number): void {
    this.reloadTable.emit({
      pageIndex,
      pageSize,
    });
  }
}
