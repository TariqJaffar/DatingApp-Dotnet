import { CommonModule, NgFor } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {

  title = 'DatingApp';
  http=inject(HttpClient);
  users:any;
 // constructor(private httpclient:HttpClient){}
 ngOnInit(): void {
this.http.get('http://localhost:5001/api/User').subscribe({
next:Response=>this.users=Response,
error:error=>console.log(error),
complete:()=>console.log('Requested has completed')

})
}
}
