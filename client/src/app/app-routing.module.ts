import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PersonDetailsComponent } from './persons/person-details/person-details.component';
import { PersonsComponent } from './persons/persons.component';

const routes: Routes = [
  {path: '', component:PersonsComponent},
  {path: 'persons', component:PersonsComponent},
  {path: 'persons/:id', component:PersonDetailsComponent},
  {path: '**', redirectTo:'',pathMatch:'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
