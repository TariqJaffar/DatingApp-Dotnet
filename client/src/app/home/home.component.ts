import { Component, OnInit, inject } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  http=inject(HttpClient);
  registerMode=false;
  users:any;

ngOnInit():void{
  this.getUser();
}

registerToggle(){
  this.registerMode=!this.registerMode;

}
cancelRegisterMode(event:boolean){
  this.registerMode=event;
}

getUser(){

  this.http.get('https://localhost:5001/api/User').subscribe({
    next:Response=>this.users=Response,
    error:error=>console.log(error),
    complete:()=>console.log('Requested has completed')
    

  })
}

}
