import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Member } from 'src/app/Models/member';
import { Event } from 'src/app/Models/event';
import { EventsService } from 'src/app/Services/events.service';
import { tap, take, Observable, map } from 'rxjs';
import { User } from 'src/app/Models/user';
import { AccountService } from 'src/app/Services/account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Post } from 'src/app/Models/post';

@Component({
  selector: 'app-event-show',
  templateUrl: './event-show.component.html',
  styleUrls: ['./event-show.component.css'],
})
export class EventShowComponent implements OnInit {
  eventId: number;
  hobbyId: number;
  event: Event | null = null;
  arriving: boolean = false;
  user!: User;
  members: Observable<Member[]> | undefined;
  form!: FormGroup;
  post: boolean = false;
  constructor(
    private router: Router,
    private eventService: EventsService,
    private accountService: AccountService,
    private formBuilder: FormBuilder
  ) {
    let url = this.router.url;
    this.eventId = +url.split('/')[4];
    this.hobbyId = +url.split('/')[2];
    accountService.currentUser$.pipe(take(1)).subscribe((user: User | null) => {
      this.user = user as User;
    });
  }

  ngOnInit(): void {
    this.eventService.getEvent(this.eventId).subscribe((event) => {
      this.event = event;
      this.members = this.eventService
        .GetMembersForEvent(this.eventId)
        .pipe(tap((members) => this.checkArriving(members)));
    });
  }

  checkArriving(m:Member[]) {
    

          if (m.length>0)
          {
            for (let index = 0; index < m.length; index++) {
              if(m[index].userName===this.user.username){
              this.arriving=true;
              return;
              }
            }

          }
          this.arriving=false;
        
      
    
  }
  joinEvent() {
    this.eventService.registerForEvent(this.eventId).subscribe((event) => {this.members = this.eventService
      .GetMembersForEvent(this.eventId)
      .pipe(tap((members) => this.checkArriving(members)));});
    
  }

  leaveEvent() {
    this.eventService
      .ChangeAttendingToEvent(this.eventId)
      .subscribe((event) => {this.members = this.eventService
        .GetMembersForEvent(this.eventId)
        .pipe(tap((members) => this.checkArriving(members)));});
    
  }

  addPost() {
    this.form = this.formBuilder.group({
      content: ['', [Validators.required]],
    });
    this.post = !this.post;
  }
  submitPost() {
    this.eventService
      .CreateComment(this.form.value.content, this.eventId)
      .subscribe((event) => {});
    this.eventService
      .GetCommentsForEvent(this.eventId)
      .subscribe((posts: Post[]) => {
        this.event!.posts = posts;
      });
    this.post = !this.post;
  }
}
