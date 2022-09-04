import { Component, Input, OnInit } from '@angular/core';
import { MatTabChangeEvent } from '@angular/material/tabs';
import { member } from 'src/app/_model/member';
import { usermessage } from 'src/app/_model/message';
import { MessageService } from '../../_service/message.service';

@Component({
  selector: 'app-member-chatbox',
  templateUrl: './member-chatbox.component.html',
  styleUrls: ['./member-chatbox.component.css']
})
export class MemberChatboxComponent implements OnInit {

  @Input() member!: member;


  usermessage: usermessage | undefined;
  recipientUsername ="";
  pageNumber = 1;
  pageSize = 7;
  totalItem = 10;
  welcome = true;

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
    if(this.member){
      this.welcome = false;
    }
  }

  tabChanged(tabChangeEvent: MatTabChangeEvent): void {
    console.log('tabChangeEvent => ', tabChangeEvent);
    console.log('index => ', tabChangeEvent.index);
    this.messageService.loadUsermessage(this.pageNumber, this.pageSize, this.member.userName).subscribe(response => {
      this.usermessage = response;
      console.log(this.usermessage);
    })

    if(tabChangeEvent.index == 0)
    {
      this.usermessage = undefined;
      console.log(this.recipientUsername);
      this.messageService.loadUsermessage(this.pageNumber, this.pageSize, this.member.userName).subscribe(response => {
        this.usermessage = response;
        console.log(this.usermessage);
      })
    }
  }

}
