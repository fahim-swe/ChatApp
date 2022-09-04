import { Component, OnInit } from '@angular/core';

import { AccountService } from '../../_service/account.service';
import { FormBuilder, Validators } from '@angular/forms';
import { Routes, Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  
  constructor(private fb: FormBuilder, 
    private http: AccountService,
    private route : Router) { }

  loginFrom = this.fb.group({
    username: ['', [Validators.required, Validators.minLength(4)]],
    password: ['', [Validators.required, Validators.minLength(4)]],

  });

  ngOnInit(): void {
  }

  login()
  {
    console.log(this.loginFrom.value);
    this.http.login(this.loginFrom.value).subscribe(res => {
      this.route.navigateByUrl('/home');
      console.log("loginSuccessfull");
    })
  }

}
