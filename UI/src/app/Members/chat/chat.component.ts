import { Component, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { sendmessage } from '../../_model/sendmessge';
import { MessageService } from '../../_service/message.service';
import { usermessage, message } from '../../_model/message';
import { empty } from 'rxjs';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy {

 
  @Input() recipientusername!: string;
  @Input() usermessage: usermessage | undefined;

  messgeContent!: string;
  pageNumber = 1;
  pageSize = 7;
  totalItem = 10;

 

  constructor(private messageService: MessageService) { }


  ngOnDestroy(): void {
    
  }

  sendmessageFrom :any;

  ngOnInit(): void {
    
  }


  sendMessage()
  {
    console.log(this.messgeContent);
    this.messageService.sendMessage(this.recipientusername, this.messgeContent).subscribe(response => {
      this.usermessage?.data.push(response as message);
      this.messgeContent="";
    })
  }
}
