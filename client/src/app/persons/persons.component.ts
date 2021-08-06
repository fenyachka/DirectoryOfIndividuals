import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import { IPerson } from '../shared/models/person';
import { PersonParams } from '../shared/models/personParams';
import { PersonsService } from './persons.service';

import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PersonAddEditComponent } from './person-add-edit/person-add-edit.component';

@Component({
  selector: 'app-persons',
  templateUrl: './persons.component.html',
  styleUrls: ['./persons.component.scss']
})
export class PersonsComponent implements OnInit {

  @ViewChild('searchByFirstNameGeo', { static: true }) searchByFirstNameGeoTerm: ElementRef;
  @ViewChild('searchByFirstNameEn', { static: true }) searchByFirstNameEnTerm: ElementRef;
  @ViewChild('searchByLastNameGeo', { static: true }) searchByLastnameGeoTerm: ElementRef;
  @ViewChild('searchByLastNameEn', { static: true }) searchByLastnameEnTerm: ElementRef;
  @ViewChild('searchByPrivateNumber', { static: true }) searchByPrivateNumberTerm: ElementRef;
  @ViewChild('searchByBirthDate', { static: true }) searchByBirthDateTerm: ElementRef;
  @ViewChild('searchByAddress', { static: true }) searchByAddressTerm: ElementRef;
  @ViewChild('searchByPhone', { static: true }) searchByPhoneTerm: ElementRef;
  @ViewChild('searchByEmail', { static: true }) searchByEmailTerm: ElementRef;

  persons: IPerson[];
  pagination: IPagination;
  personParams: PersonParams;
  modalRef: BsModalRef;

  constructor(private personsService: PersonsService, private modalService: BsModalService) {
    this.personParams = new PersonParams();
   }

  ngOnInit() {
    this.loadPersons();
  }

  loadPersons(){
    this.personsService.getPersons(this.personParams).subscribe(response=>{
      this.persons=response.result;
      this.pagination=response.pagination;
    })
  }

  onPageChanged(event: any) {
    if (this.personParams.pageNumber !== event) {
      this.personParams.pageNumber = event;
      this.loadPersons();
    }
  }

  openAddModal(){
    this.modalRef = this.modalService.show(PersonAddEditComponent, {
      initialState: {
        title: 'Add Person',
        btnTitle: 'Add',
      }
    });
    this.modalRef.content.closeBtnName = 'Close';
    this.modalRef.content.event.subscribe(res => {
      this.personsService.createPerson(res).subscribe(response => {
        this.loadPersons();
      }, error => {
        console.log(error);
      });
    });
  }

  openEditModal(person: IPerson){
    this.modalRef = this.modalService.show(PersonAddEditComponent, {
      initialState: {
        title: 'Edit Person',
        btnTitle: 'Edit',
        data: person
      }
    });
    this.modalRef.content.closeBtnName = 'Close';
    this.modalRef.content.event.subscribe(res => {
    this.personsService.updatePerson(res, person.id).subscribe(response => {
      console.log(response)
        this.loadPersons();
      }, error => {
        console.log(error);
      });
    });
  }


  deletePerson(id:any){
    this.personsService.deletePerson(id).subscribe((response: any) =>{
      this.loadPersons();
    },error =>{
      console.log(error);
    });
  }

  onSearch(){
    this.personParams.firstNameGeo = this.searchByFirstNameGeoTerm.nativeElement.value;
    this.personParams.firstNameEn = this.searchByFirstNameEnTerm.nativeElement.value;
    this.personParams.lastNameGeo = this.searchByLastnameGeoTerm.nativeElement.value;
    this.personParams.lastNameEn = this.searchByLastnameEnTerm.nativeElement.value;
    this.personParams.privateNumber = this.searchByPrivateNumberTerm.nativeElement.value;
    this.personParams.birthdate = this.searchByBirthDateTerm.nativeElement.value;
    this.personParams.address = this.searchByAddressTerm.nativeElement.value;
    this.personParams.phone = this.searchByPhoneTerm.nativeElement.value;
    this.personParams.email = this.searchByEmailTerm.nativeElement.value;


    this.personParams.pageNumber = 1;
    this.loadPersons();
  }

  onReset(){
    this.searchByFirstNameGeoTerm.nativeElement.value = '';
    this.searchByFirstNameEnTerm.nativeElement.value = '';
    this.searchByLastnameGeoTerm.nativeElement.value = '';
    this.searchByLastnameEnTerm.nativeElement.value = '';
    this.searchByPrivateNumberTerm.nativeElement.value = '';
    this.searchByBirthDateTerm.nativeElement.value = '';
    this.searchByAddressTerm.nativeElement.value = '';
    this.searchByPhoneTerm.nativeElement.value = '';
    this.searchByEmailTerm.nativeElement.value = '';

    this.personParams = new PersonParams();
    this.loadPersons();
  }
}
