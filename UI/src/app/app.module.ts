import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MaterialsModule } from './Materials/materials/materials.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { MemberListComponent } from './Members/member-list/member-list.component';
import { MemberChatboxComponent } from './Members/member-chatbox/member-chatbox.component';
import { DialogComponent } from './Members/dialog/dialog.component';
import { HomeComponent } from './home/home.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms'


import { RegisterComponent } from './account/register/register.component';
import { LoginComponent } from './account/login/login.component';
import { ChatComponent } from './Members/chat/chat.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './_interceptors/jwt.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    MemberListComponent,
    MemberChatboxComponent,
    DialogComponent,
    HomeComponent,
    RegisterComponent,
    LoginComponent,
    ChatComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,

    HttpClientModule,
    
    FormsModule,
    ReactiveFormsModule,
    MaterialsModule,
    BrowserAnimationsModule,
  ],
  providers: [

    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
