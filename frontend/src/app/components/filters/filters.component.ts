import { FilterModel } from '@app/models/filter.model';
import { Component, Output, EventEmitter } from '@angular/core';
import { debounce } from '@app/utils/debounce.decorator';

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.scss'],
})
export class FiltersComponent {
  filter: FilterModel = new FilterModel();

  @Output()
  searchTermChanged: EventEmitter<string> = new EventEmitter();

  @Output()
  toggleShowCompletedTodos: EventEmitter<void> = new EventEmitter();

  @Output()
  toggleShowIncompleteTodos: EventEmitter<void> = new EventEmitter();

  onToggleShowCompletedTodos(): void {
    this.toggleShowCompletedTodos.emit();
  }

  onToggleShowIncompleteTodos(): void {
    this.toggleShowIncompleteTodos.emit();
  }

  @debounce(500)
  onSearchTermChanged(newValue: string): void {
    this.filter.searchTerm = newValue;
    this.searchTermChanged.emit(this.filter.searchTerm);
  }
}
