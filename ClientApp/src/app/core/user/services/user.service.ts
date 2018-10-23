
import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { User, UserInfo } from "../models/user";
import { HttpClient } from "@angular/common/http";


@Injectable()
export class UserService {

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    const url = `api/UserRole`;

    return this.http.get<User[]>(url);
  }

  getUserByPegawaiId(id: string = ''): Observable<User> {
    const url = `api/UserRole/${id}`;
    return this.http.get<User>(url);
  }

  postUserRoles(user: User): Observable<User> {
    const url = `api/UserRole`;
    return this.http.post<User>(url, user);
  }

  getUserInfo(): Observable<User> {
    const url = `Index/UserInfo`;
    return this.http.get<User>(url);
  }

  getPegawaiByNip(nip: string): Observable<any> {
    const url = `api/Pegawai/${nip}`;
    return this.http.get(url);
  }

  postNewUser(model: any): Observable<any> {
    const url = 'Account/AddUserKemenkeu';
    return this.http.post(url, model);
  }

}
