import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';

import { PersonsComponent } from './persons.component';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PersonAddEditComponent } from './person-add-edit/person-add-edit.component';
import { PersonDetailsComponent } from './person-details/person-details.component';
import { RouterModule } from '@angular/router';
import { RelatedPersonsListComponent } from './related-persons/related-persons-list/related-persons-list.component';



@NgModule({
  declarations: [
    PersonsComponent,
    PersonAddEditComponent,
    PersonDetailsComponent,
    RelatedPersonsListComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    ModalModule.forRoot(),
    RouterModule
  ],
  exports:[
    PersonsComponent
  ]
})
export class PersonsModule { }
