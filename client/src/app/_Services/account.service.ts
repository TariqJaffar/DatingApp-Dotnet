import { HttpClient } from '@angular/common/http';
import { Injectable,inject, signal } from '@angular/core';
import { User } from '../Models/User';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  
private http =inject(HttpClient);
baseUrl=environment.apiUrl;
currentUser= signal<User | null>(null);

login(model:any){
  return this.http.post<User>(this.baseUrl+'Account/Login',model).pipe(
    map((User =>{
      if(User){
        localStorage.setItem('user',JSON.stringify(User));
        this.currentUser.set(User);
      }
    })
  ))

}
register(model:any){
  return this.http.post<User>(this.baseUrl+'Account/Register',model).pipe(
    map((User =>{
      if(User){
        localStorage.setItem('user',JSON.stringify(User));
        this.currentUser.set(User);
      }
      return User;
    })
    
  ))

}
 logout(){
  localStorage.removeItem('user');
  this.currentUser.set(null);
 }
}
