import { Injectable } from '@angular/core';
import { ReplaySubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { User } from '../Models/user';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  private currentUserSource$ = new ReplaySubject<User | null>(1);
  currentUser$ = this.currentUserSource$.asObservable();

  constructor(private http: HttpClient,
    private route:Router) {}

  login(model: any) {
    return this.http.post<User>(this.baseUrl + 'Login', model).pipe(
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user);
        }
      })
    );
  }

  register(model: any) {
    return this.http.post<User>(this.baseUrl + 'Registration', model).pipe(
      map((user: User) => {
        if (user) {
          this.setCurrentUser(user);
          this.route.navigateByUrl('/hobbies');
        }
        return user;
      })
    );
  }

  setCurrentUser(user: User) {
    user.roles = [];
    const roles = this.getDecodedToken(user.token).role; 
    Array.isArray(roles) ? (user.roles = roles) : user.roles.push(roles);
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource$.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource$.next(null);
  }

  getDecodedToken(token: string) {
    const tokenParts = token.split('.');
    const payload = tokenParts[1];
    const decodedPayload = atob(payload);
    return JSON.parse(decodedPayload);
  }
}
