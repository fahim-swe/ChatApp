import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { map, ReplaySubject } from 'rxjs';

import { environment } from '../../environments/environment';
import { appuser } from '../_model/appuser';



@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  private currentUserSource = new ReplaySubject<appuser | null>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private _snackBar: MatSnackBar) { }

  register(model: any) {
    console.log(model);
    return this.http.post(this.baseUrl + "Account/signup", model).pipe(
      map((response: any) => {
        const user = response as appuser;
        if (user) {
          localStorage.setItem('Appuser', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    )
  }


  login(model: any) {
    return this.http.post(this.baseUrl + "Account/login", model).pipe(
      map((response: any) => {
        const user = response as appuser;
        if (user) {
          localStorage.setItem('Appuser', JSON.stringify(user));
          this.currentUserSource.next(user);
          this._snackBar.open(user.fullName, "Welcome");
        }
      })
    )
  }



  logout() {
    localStorage.removeItem('Appuser');
    this.currentUserSource.next(null);
    this._snackBar.open("Logout", "Successfull");
  }


  setCurrentUser(user: appuser)
  {
    this.currentUserSource.next(user);
  }

}
