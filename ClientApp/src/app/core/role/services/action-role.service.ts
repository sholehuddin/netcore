import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { RoleAction } from "../../../shared/models/action-model";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class ActionRoleService {
  constructor(private http: HttpClient) { }

    getAll(): Observable<any> {
        const url = "api/ActionRole";
        return this.http.get(url);
    }

    post(aRole: RoleAction): Observable<any> {
        const url = "api/ActionRole";
        return this.http.post(url, aRole);
    }

    getSystemController(): Observable<any> {
        const url = "api/ActionRole";
        return this.http.get(url + "/1");
    }
}
