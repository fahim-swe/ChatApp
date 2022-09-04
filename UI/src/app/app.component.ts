import { Component, OnInit } from '@angular/core';
import { appuser } from './_model/appuser';
import { AccountService } from './_service/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  
  title = 'UI';
  users: any;

  constructor(private accountService: AccountService)
  {

  }

  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser()
  {
    const user  = localStorage.getItem('Appuser');
    if(user)
    {
      console.log(JSON.parse(user));
      const appuser : appuser = JSON.parse(user);
      this.accountService.setCurrentUser(appuser);
    }
  }
  
}
