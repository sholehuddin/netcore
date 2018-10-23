import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { MenuItemModel } from "../../../shared/models/menu-item";
import { ResultModel } from "../../../shared/models/result-model";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class MenuService {
  constructor(private http: HttpClient) {}

  getMenus(): Observable<MenuItemModel[]> {
    const url = "api/Menu/";
    return this.http.get<MenuItemModel[]>(url);
  }

  getAllMenus(): Observable<MenuItemModel[]> {
    const url = "api/Menu/-1";
    return this.http.get<MenuItemModel[]>(url);
  }

  getSysMenuById(id: number): Observable<MenuItemModel> {
    const url = "api/Menu/" + id;
    return this.http.get<MenuItemModel>(url);
  }

  putSysMenu(sMenu: MenuItemModel): Observable<ResultModel> {
    const url = "api/Menu/" + sMenu.MenuId;
    return this.http.put<ResultModel>(url, sMenu);
  }

  postSysMenu(sMenu: MenuItemModel): Observable<ResultModel> {
    const url = "api/Menu";
    return this.http.post<ResultModel>(url, sMenu);
  }

  deleteSysMenu(sMenu: MenuItemModel): Observable<MenuItemModel> {
      const url = "api/Menu/" + sMenu.MenuId;
    return this.http.delete<MenuItemModel>(url);
  }
}
