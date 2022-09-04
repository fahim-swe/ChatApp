import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { DialogComponent } from '../Members/dialog/dialog.component';
import { AccountService } from '../_service/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  constructor(public dialog: MatDialog, private account: AccountService) { }

  ngOnInit(): void {
  }

  openDialog() {
    this.dialog.open(DialogComponent, {
      disableClose: true,
      width: '90vw',
      height: '80vh',
      data: { name: "fahim", animal: "myname" }
    });
  }


  logout()
  {
    this.account.logout();
    this.dialog.open(DialogComponent, {
      disableClose: true,
      data: {user: this.account.currentUser$}
    });

  }

}
