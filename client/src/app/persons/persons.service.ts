import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination, PaginatedResult } from '../shared/models/pagination';
import { IPerson } from '../shared/models/person';
import { PersonParams } from '../shared/models/personParams';
import { map } from 'rxjs/operators';
import { IRelatedPersonIdentificator } from '../shared/models/relatedPersonIdentificator';
import { RelatedPerson } from '../shared/models/relatedPerson';
import { IRelatedPersonModel } from '../shared/models/relatedPersonModel';


const baseUrl = 'http://localhost:5000/api/persons/';

@Injectable({
  providedIn: 'root'
})
export class PersonsService {


  constructor(private http: HttpClient) { }


  getPersons(personParams: PersonParams) {

    let params = this.getPaginationHeaders(personParams.pageNumber, personParams.pageSize);

    params = params.append('privateNumber', personParams.privateNumber);
    params = params.append('firstNameGeo', personParams.firstNameGeo);
    params = params.append('firstNameEn', personParams.firstNameEn);
    params = params.append('lastNameGeo', personParams.lastNameGeo);
    params = params.append('lastNameEn', personParams.lastNameEn);
    params = params.append('address', personParams.address);
    params = params.append('phone', personParams.phone);
    params = params.append('email', personParams.email);
    params = params.append('birthdate', personParams.birthdate.toLocaleString());
    return this.getPaginatedResult<IPerson[]>(`${baseUrl}`, params);
  }



  private getPaginatedResult<T>(url, params) {
    const paginatedResult: PaginatedResult<T> = new PaginatedResult<T>();

    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return params;
  }



  updatePerson(person: any, id: string) {
    return this.http.put(`${baseUrl}` + id, person);
  }





  createPerson(person: IPerson){
    return this.http.post(`${baseUrl}`, person);
  }

  deletePerson(id: any){
    return this.http.delete(`${baseUrl}` + id);
  }

  getPerson(id:any){
    return this.http.get<IPerson>(`${baseUrl}` + id);
  }

  getRelatedPersons(id:any){
    return this.http.get<IRelatedPersonModel>(`${baseUrl}` + id + '/related');
  }

  addRelatedPerson(id:any, relatedPerson: IRelatedPersonIdentificator){
    return this.http.post(`${baseUrl}` + id, relatedPerson);
  }

  deleteRelatedPerson(id:any, relatedPerson:RelatedPerson){
    return this.http.post(`${baseUrl}`+id+"/dlr", relatedPerson);
  }
}
