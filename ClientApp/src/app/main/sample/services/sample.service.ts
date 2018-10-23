import { Injectable } from '@angular/core';
import { Observable } from "rxjs/Observable";
import { Quote } from "../models/quote";
import { HttpClient } from '@angular/common/http';

@Injectable()
export class SampleService {
    apiUrl: string = "api/SampleData";
    constructor(private http: HttpClient) { }

    getAll(): Observable<Quote[]> {
        return this.http.get<Quote[]>(this.apiUrl);
    }

    getById(id: string = ""): Observable<Quote> {
      return this.http.get<Quote>(this.apiUrl + `/${id}`);
    }

    add(item: Quote): Observable<any> {
        return this.http.post(this.apiUrl, item);
    }

    update(item: Quote): Observable<Quote> {
        return this.http.put<Quote>(this.apiUrl + `/${item.QuoteId}`, item);
    }

    delete(item: Quote): Observable<Quote> {
      return this.http.delete<Quote>(this.apiUrl + `/${item.QuoteId}`);
    }
}
