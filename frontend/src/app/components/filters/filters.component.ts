import { FilterModel } from '@app/models/filter.model';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { debounce } from '@app/utils/debounce.decorator';

@Component({
  selector: 'app-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.scss'],
})
export class FiltersComponent {
  @Input()
  filter: FilterModel;

  @Output()
  filterChanged: EventEmitter<FilterModel> = new EventEmitter();

  toggleshowCompletedTodos(): void {
    this.filter.showCompletedTodos = !this.filter.showCompletedTodos;
    this.emitFilterChanged();
  }

  toggleshowIncompleteTodos(): void {
    this.filter.showIncompleteTodos = !this.filter.showIncompleteTodos;
    this.emitFilterChanged();
  }

  @debounce(500)
  onSearchTermChanged(newValue: string): void {
    this.filter.searchTerm = newValue;
    this.emitFilterChanged();
  }

  emitFilterChanged(): void {
    this.filterChanged.emit(this.filter);
  }
}
