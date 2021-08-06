import { Component,EventEmitter, OnInit } from '@angular/core';
import {FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import {   RelatedPerson } from 'src/app/shared/models/relatedPerson';
import { IRelatedPersonIdentificator } from 'src/app/shared/models/relatedPersonIdentificator';
import { IRelatedPersonModel } from 'src/app/shared/models/relatedPersonModel';

import { PersonsService } from '../../persons.service';

@Component({
  selector: 'app-related-persons-list',
  templateUrl: './related-persons-list.component.html',
  styleUrls: ['./related-persons-list.component.scss']
})
export class RelatedPersonsListComponent implements OnInit {
  itemform
  personId
  relationshipId:number;
  relationshipsOptions = [
    { relationship: 'თანამშრომელი', value: 1 },
    { relationship: 'ნათესავი', value: 2 },
    { relationship: 'ნაცნობი', value: 3 },
    { relationship: 'სხვა', value: 4 },
  ];

  relatedPerson:IRelatedPersonIdentificator;
  relatedPersons: IRelatedPersonModel;
  relId:RelatedPerson;

  public event: EventEmitter<any> = new EventEmitter();

  constructor(private personsService: PersonsService, private activatedRoute: ActivatedRoute, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.itemform = this.formBuilder.group({
      privateNumber: [null, [Validators.required]],
      relationshipId:[null,[Validators.required]]
    });
    this.personId = this.activatedRoute.snapshot.paramMap.get('id');
    this.loadRelatedPeople();
  }

  loadRelatedPeople(){
    this.personsService.getRelatedPersons(this.personId).subscribe(res=>{
      this.relatedPersons = res;
    }, error =>{
      console.log(error)
    })
  }


  onSRelationshipSelected(relationshipId: number) {
    this.itemform.patchValue({
      relationshipId: relationshipId
   });
  }
  addRelatedPerson(form) {
    this.relatedPerson = form.value
    this.personsService.addRelatedPerson(this.personId,this.relatedPerson).subscribe(res=>{
      this.loadRelatedPeople();
      this.resetFields();
    },error =>{
      console.log(error)
    });

  }

  deleteRelatedPerson(id){
    this.relId=new RelatedPerson;
    this.relId.relatedPersonId=id;
    this.personsService.deleteRelatedPerson(this.personId, this.relId).subscribe(res =>{
      this.loadRelatedPeople()
    },error=>{
      console.log(error)
    })
  }

  resetFields(){
    this.itemform.reset();
  }
}


