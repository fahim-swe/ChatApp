import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Token } from '@angular/compiler';
import { paged } from '../_model/member';
import { usermessage } from '../_model/message';

@Injectable({
  providedIn: 'root'
})




export class MessageService {

  baseUrl = environment.apiUrl;
  constructor(private http: HttpClient) { }
  

  sendMessage(recipientusername: string, content: string)
  {
    return this.http.post(this.baseUrl + "Message", {
      recipientUsername: recipientusername,
      content
    });
  }

  loadUsermessage(pageNumber: number, pageSize: number, recipient: string)
  {
    let params = new HttpParams();
    params = params.append('PageNumber', pageNumber.toString());
    params = params.append('PageSize', pageSize.toString());
    params = params.append('RecipientUsername', recipient);

    return this.http.get<usermessage>(this.baseUrl+ "Message", {params});
  }
}
