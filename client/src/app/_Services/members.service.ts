import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../Models/members';
import { of, tap } from 'rxjs';
import { Photo } from '../Models/Photo';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
private http=inject(HttpClient);
baseUrl=environment.apiUrl;
members=signal<Member[]>([]);
getMembers(){
  return this.http.get<Member[]>(this.baseUrl+'User').subscribe({

    next:members=>this.members.set(members)
  });
}

getMember(userName:string){
  const member =this.members().find(x=>x.userName===userName);
  if(member !== undefined) return of(member);
  return this.http.get<Member>(this.baseUrl+'User/'+userName,);
}

updatemember(member:Member){
  return this.http.put(this.baseUrl+'User',member).pipe(
    tap(()=>{
      this.members.update(members=>members.map(m=>m.userName===member.userName
        ?member:m))

    })
  );
}
setMainPhoto(photo:Photo){
  return this.http.put(this.baseUrl+'users/set-main-photo/'+photo.id,{}).
  pipe(
    tap(()=>{
      this.members.update(members=>members.map(m=>{
        if(m.photos.includes(photo)){
          m.photoUrl=photo.url
        }
        return m;
      })

      )
    })
  )
}

deletephoto(photo:Photo){
  return this.http.delete(this.baseUrl+'User/delete-photo/'+photo.id).pipe(
    tap(()=>{
      this.members.update(members=>members.map(m=>{
        if(m.photos.includes(photo)){
          m.photos=m.photos.filter(x=>x.id!== photo.id)
        }
        return m
      }))
    })
  );
}

}
