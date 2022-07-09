import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, take, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Member } from '../Models/member';
import { User } from '../Models/user';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  baseUrl = environment.apiUrl;
  user!: User | null;
  member: Member | null = null;
  constructor(
    private http: HttpClient,
    private accountService: AccountService
  ) {
    accountService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.user = user;
    });
  }

  getMember(): Observable<Member> {
    if (!this.member) {
      return this.http
        .get<Member>(this.baseUrl + `members`)
        .pipe(tap((member) => (this.member = member)));
    } else {
      return of(this.member);
    }
  }

  updateMember(member: Member) {
    this.member = member;
    return this.http.put(this.baseUrl + 'members', {
      country: member.country,
      city: member.city,
    });
  }

  updatePassword(CurrentPassword: string, NewPassword: string) {
    return this.http.put(this.baseUrl + 'members/password', {
      CurrentPassword,
      NewPassword,
    });
  }
  
}
