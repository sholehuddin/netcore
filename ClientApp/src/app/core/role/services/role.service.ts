import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Role } from "../../user/models/role";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class RoleService {
  constructor(private http: HttpClient) {}

  getAll(): Observable<any> {
    const url = "api/Role";
    return this.http.get(url);
  }

  postRole(role: Role): Observable<any> {
    const url = "api/Role";
    return this.http.post(url, role);
  }

  putRole(role: Role): Observable<any> {
      const url = 'api/Role';
      return this.http.put(url, role);
  }

  deleteRole(role: Role): Observable<any> {
    const url = `api/Role/${role.RoleId}`;

    return this.http.delete(url);
  }

  getById(id: string = ""): Observable<any> {
    const url = `api/Role/${id}`;
    return this.http.get(url);
  }
}
