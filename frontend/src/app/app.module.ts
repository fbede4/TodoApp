import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogModule } from '@angular/material/dialog';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatInputModule } from '@angular/material/input';

import { AppRoutingModule } from '@app/app-routing.module';
import { AppComponent } from '@app/app.component';
import { TodoListComponent } from '@app/components/todo-list/todo-list.component';
import { API_BASE_URL } from '@app/clients';

import { environment } from '../environments/environment.prod';
import { HttpClientModule } from '@angular/common/http';
import { TodoListHeaderComponent } from './components/todo-list-header/todo-list-header.component';
import { TodoListFooterComponent } from './components/todo-list-footer/todo-list-footer.component';
import { TodoListItemComponent } from './components/todo-list-item/todo-list-item.component';
import { NewTodoComponent } from './components/new-todo/new-todo.component';
import { FiltersComponent } from './components/filters/filters.component';

@NgModule({
  declarations: [
    AppComponent,
    TodoListComponent,
    TodoListHeaderComponent,
    TodoListFooterComponent,
    TodoListItemComponent,
    NewTodoComponent,
    FiltersComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    MatDialogModule,
    HttpClientModule,
    MatPaginatorModule,
    MatCheckboxModule,
    MatInputModule
  ],
  providers: [
    {
      provide: API_BASE_URL,
      useValue: environment.apiBaseUrl,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
