import { CommonModule, NgFor } from '@angular/common';
import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavComponent } from "./nav/nav.component";
import { AccountService } from './_Services/account.service';
import { HomeComponent } from "./home/home.component";
import { NgxSpinnerComponent } from 'ngx-spinner';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, NavComponent, HomeComponent,NgxSpinnerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {


 
  private accountService=inject(AccountService);

 // constructor(private httpclient:HttpClient){}
 ngOnInit(): void {

this.setCurrentUser();
}
setCurrentUser(){
  const userString =localStorage.getItem('user');
  if(!userString) return;
  const user= JSON.parse(userString);
  this.accountService.currentUser.set(user);
}

}
