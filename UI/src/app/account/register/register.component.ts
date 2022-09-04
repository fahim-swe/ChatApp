import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { Route, Router, Routes } from '@angular/router';
import { AccountService } from '../../_service/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  hide = true;
  
  constructor(private fb: FormBuilder, 
    private account:AccountService,
    private route: Router) {}

  signUpFrom = this.fb.group({
      fullname: ['', [Validators.required]],
      username: ['', [Validators.required, Validators.minLength(4)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4)]],
      confirmpassword: ['', [Validators.required, ]]
  });

  ngOnInit(): void {
    
  }



  signup()
  {
    console.log(this.signUpFrom.value);
    if(this.signUpFrom.valid)
    {
      this.account.register(this.signUpFrom.value).subscribe(()=>{
        this.route.navigateByUrl('/home');
        console.log("successfully");
      })
    }
  }

  
  getErrorPassword()
  {
    if (this.signUpFrom.hasError('required')) {
      return 'You must enter a value';
    }
    return this.signUpFrom.hasError('password') ? 'Not Valid' : 'Password must be at least 4 characters';
  }
  getErrorUsername()
  {
    if (this.signUpFrom.hasError('required')) {
      return 'You must enter a value';
    }
    return this.signUpFrom.hasError('username') ? 'Not Valid' : 'Username must be at least 4 characters';
  }
  
  getErrorFullname()
  {
    if (this.signUpFrom.hasError('required')) {
      return 'You must enter a value';
    }
    return this.signUpFrom.hasError('fullname') ? 'Not Valid' : 'Enter your full name';
  }

  getErrorMessage() {
    if (this.signUpFrom.hasError('required')) {
      return 'You must enter a value';
    }
    return this.signUpFrom.hasError('email') ? 'Not a valid email' : 'Enter a valid Email Address';
  }

}
