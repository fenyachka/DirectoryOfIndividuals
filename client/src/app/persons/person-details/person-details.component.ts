import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IPerson } from 'src/app/shared/models/person';
import { PersonsService } from '../persons.service';


@Component({
  selector: 'app-person-details',
  templateUrl: './person-details.component.html',
  styleUrls: ['./person-details.component.scss']
})
export class PersonDetailsComponent implements OnInit {
 person: IPerson;
 myDate: Date = new Date();

  constructor(private personsService: PersonsService, private activatedRoute: ActivatedRoute) { }

  ngOnInit() {
    this.loadPerson();
  }

  loadPerson(){
    this.personsService.getPerson(this.activatedRoute.snapshot.paramMap.get('id')).subscribe(person =>{
      this.person = person;
    },error => {
      console.log(error);
    });
  }
}
