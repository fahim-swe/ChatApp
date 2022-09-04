import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { UserService } from '../../_service/user.service';
import { member } from '../../_model/member';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
})
export class MemberListComponent implements OnInit {

  @Output() newItemEvent = new EventEmitter<member>()

  constructor(private userService: UserService) { }

  pageNumber = 1;
  pageSize = 7;
  totalItem = 10;

  members : member[] = [];

  ngOnInit(): void {
    this.userService.getMembers(this.pageNumber, this.pageSize).subscribe(res => {
    
      this.members = res.data;
      this.totalItem = res.totalRecords;
    })
    
  }



  hidden = false;

  handleClick(member: member) {
    console.log(member);
    this.newItemEvent.emit(member);
  }

  toggleBadgeVisibility() {
    this.hidden = !this.hidden;
  }

  getServerData(page: any)
  {
    this.pageNumber = page.pageIndex+1;
    this.userService.getMembers(this.pageNumber, this.pageSize).subscribe(res => {
      console.log(res);
      this.members = res.data;
      this.totalItem = res.totalRecords;
    })

    console.log(page);
    console.log(this.pageNumber);
  }
}
