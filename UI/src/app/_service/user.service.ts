import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { paged } from '../_model/member';


@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUri = environment.apiUrl;

  constructor(private http: HttpClient) { }


  getMembers(pageNumber: number, pageSize: number)
  {
    
    let params = new HttpParams();
    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());

    return this.http.get<paged>(this.baseUri+ "Users", {params},);
  }
}
