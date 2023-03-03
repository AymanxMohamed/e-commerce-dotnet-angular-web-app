import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import { RouterModule } from '@angular/router';

@NgModule({
  declarations: [PagingHeaderComponent, PagerComponent],
  imports: [CommonModule, PaginationModule.forRoot(), RouterModule],
  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    RouterModule,
  ],
})
export class SharedModule {}
