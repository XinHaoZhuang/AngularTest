import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Menu } from './menu';
import { Observable } from '../../node_modules/rxjs';
@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor(
    private http: HttpClient
  ) { }
  // private menuUrl = 'localhost/SCZM/Ashx/WX/WX_GetLoginInfo.ashx';
  getMenues(): Observable<Menu[]> {
    return this.http.get<Menu[]>('/api/Ashx/WX/WX_GetLoginInfo.ashx?action=getMenu');
  }
}
