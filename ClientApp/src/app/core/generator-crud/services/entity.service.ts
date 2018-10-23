import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseCrudService } from '../../../shared/services/base-crud.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EntityService extends BaseCrudService {
  constructor(public http: HttpClient) {
    super()
    this.urlApi = 'api/generatorCrud'
   }

   CheckModuleExist(model: string): Observable<any>{
     return this.http.get(`${this.urlApi}/CheckModule/${model}`)
   }
}
