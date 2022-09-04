import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { member } from '../_model/member';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  member!: member;
  welcome = true;
  
  constructor(private fb: FormBuilder) {}
  ngOnInit(): void {
    if(this.member){
      this.welcome = false;
    }    
  }

  
  addItem(newItem: member) {
    this.member= newItem;
    this.welcome = false;
  }

}
