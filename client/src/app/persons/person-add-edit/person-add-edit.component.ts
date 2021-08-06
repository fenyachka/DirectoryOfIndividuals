import { formatDate } from '@angular/common';
import { Component, EventEmitter, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { IPerson } from 'src/app/shared/models/person';

@Component({
  selector: 'app-person-add-edit',
  templateUrl: './person-add-edit.component.html',
  styleUrls: ['./person-add-edit.component.scss']
})
export class PersonAddEditComponent implements OnInit {
  itemform

  closeBtnName: string;
  btnTitle: string;
  title;
  data: IPerson;

  public event: EventEmitter<any> = new EventEmitter();

  constructor(public modalRef: BsModalRef, private formBuilder: FormBuilder) {
  }



  ngOnInit(): void {
    if (this.data) {
      this.itemform = this.formBuilder.group({
        firstNameGeo: [this.data.firstNameGeo, [Validators.required]],
        firstNameEn: [this.data.firstNameEn, [Validators.required]],
        lastNameGeo: [this.data.lastNameGeo, [Validators.required]],
        lastNameEn: [this.data.lastNameEn, [Validators.required]],
        privateNumber: [this.data.privateNumber, [Validators.required]],
        birthdate: [formatDate(this.data.birthdate, 'yyyy-MM-dd', 'en'), [Validators.required]],
        address: [this.data.address, [Validators.required]],
        phone: [this.data.phone, [Validators.required]],
        email: [this.data.email, [Validators.required]],
      });
    } else {
      this.itemform = this.formBuilder.group({
        firstNameGeo: [null, [Validators.required]],
        firstNameEn: [null, [Validators.required]],
        lastNameGeo: [null, [Validators.required]],
        lastNameEn: [null, [Validators.required]],
        privateNumber: [null, [Validators.required]],
        birthdate: [null, [Validators.required]],
        address: [null, [Validators.required]],
        phone: [null, [Validators.required]],
        email: [null, [Validators.required]],
      });
    }
  }

  addAndSavePerson(form) {
    if (form.value) {
      this.triggerEvent(form.value);
      this.modalRef.hide();
    }
  }

  triggerEvent(values: any) {
    this.event.emit(values);
  }
}
