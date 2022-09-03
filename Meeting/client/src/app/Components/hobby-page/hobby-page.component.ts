import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, take, tap } from 'rxjs';
import { Hobby } from 'src/app/Models/hobby';
import { Member } from 'src/app/Models/member';
import { User } from 'src/app/Models/user';
import { AccountService } from 'src/app/Services/account.service';
import { HobbiesService } from 'src/app/Services/hobbies.service';

@Component({
  selector: 'app-hobby-page',
  templateUrl: './hobby-page.component.html',
  styleUrls: ['./hobby-page.component.css'],
})
export class HobbyPageComponent implements OnInit {
  hobbyId!: number;
  hobby: Hobby | undefined;
  members: Member[] = [];
  following: Boolean = false;
  user!: User;

  asyncTabs?: Observable<Member[]>;
  constructor(
    private router: Router,
    private accountService: AccountService,
    public hobbyService: HobbiesService
  ) {
    let url = this.router.url;
    this.hobbyId = +url.split('/')[2];
    accountService.currentUser$.pipe(take(1)).subscribe((user: User | null) => {
      this.user = user as User;
    });
  }

  ngOnInit(): void {
    this.hobbyService.getHobby(this.hobbyId).subscribe((hobby) => {
      this.hobby = hobby;
    });

    this.hobbyService.getHobbyMembers(this.hobbyId).subscribe((members) => {
      this.members = members;
      this.checkFollow();
    });
  }
  checkFollow() {
    if (this.members.find((m) => m.userName === this.user.username)) {
      this.following = true;
    } else {
      this.following = false;
    }
  }
  FollowHobby() {
    this.hobbyService.followHobby(this.hobby!.id).subscribe((members) => {this.members =[];
      this.hobbyService.getHobbyMembers(this.hobby!.id).subscribe((members)=>{this.members = members;this.checkFollow();});
    });
  }
  unFollowHobby() {
    this.hobbyService.unFollowHobby(this.hobby!.id).subscribe((members) => {this.members =[];
     this.hobbyService.getHobbyMembers(this.hobby!.id).subscribe((members)=>{this.members = members;this.checkFollow();});
    });
  }
}
