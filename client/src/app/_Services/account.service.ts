import { HttpClient } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { User } from '../Models/User';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  currentUser = signal<User | null>(null);

  constructor() {
    // Load user from local storage if it exists
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      this.currentUser.set(JSON.parse(storedUser));
    }
  }

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'Account/Login', model)
      .pipe(
        map((user) => {
          if (user) {
            console.log("User logged in: ", user); // Debug log
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUser.set(user);
          }
        })
      );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'Account/Register', model)
      .pipe(
        map((user) => {
          if (user) {
            console.log("User registered: ", user); // Debug log
            localStorage.setItem('user', JSON.stringify(user));
            this.currentUser.set(user);
          }
          return user;
        })
      );
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
