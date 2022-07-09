import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, tap } from 'rxjs';
import { Hobby } from 'src/app/Models/hobby';
import { Member } from 'src/app/Models/member';
import { HobbiesService } from 'src/app/Services/hobbies.service';

@Component({
  selector: 'app-hobby-page',
  templateUrl: './hobby-page.component.html',
  styleUrls: ['./hobby-page.component.css'],
})
export class HobbyPageComponent implements OnInit {
  hobbyId!: number;
  hobby!: Hobby;
  members: Member[] = [];

  asyncTabs?: Observable<Member[]>;
  constructor(private router: Router, private hobbyService: HobbiesService) {
    let url = this.router.url;
    this.hobbyId = +url.split('/')[2];
  }

  ngOnInit(): void {
    this.hobbyService.getHobby(this.hobbyId).subscribe((hobby) => {
      this.hobby = hobby;
      console.log(this.hobby);
    });

    this.hobbyService
      .getHobbyMembers(this.hobbyId)
      .subscribe((members) => {this.members = members
        console.log(this.members);
      });
  }
}
