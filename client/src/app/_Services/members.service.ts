import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../Models/members';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
private http=inject(HttpClient);
baseUrl=environment.apiUrl;
getMembers(){
  return this.http.get<Member[]>(this.baseUrl+'User');
}

getMember(userName:string){
  return this.http.get<Member>(this.baseUrl+'User/'+userName,);
}



}
