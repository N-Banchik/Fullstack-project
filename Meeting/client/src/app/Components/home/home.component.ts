import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EventView } from 'src/app/Models/event-view';
import { GuideView } from 'src/app/Models/guide-view';
import { HobbyView } from 'src/app/Models/hobby-view';
import { User } from 'src/app/Models/user';
import { AccountService } from 'src/app/Services/account.service';
import { EventsService } from 'src/app/Services/events.service';
import { GuideService } from 'src/app/Services/guide.service';
import { HobbiesService } from 'src/app/Services/hobbies.service';
import { Observable, take, tap } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { Pagination } from 'src/app/Models/pagination';
import { HobbyParams } from 'src/app/Models/SearchParams/hobby-params';
import { GuideParams } from 'src/app/Models/SearchParams/guide-params';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  user: User | undefined;
  hobbies: HobbyView[] = [];
  guides: GuideView[] = [];
  events: EventView[] = [];
  filterForm: FormGroup | undefined;
  hobbyParams!: HobbyParams;
  pagination!: Pagination;
  constructor(
    private accountService: AccountService,
    private hobbyService: HobbiesService,
    private eventService: EventsService,
    private guideService: GuideService,
    private router: Router
  ) {
    accountService.currentUser$.pipe(take(1)).subscribe((user: User | null) => {
      this.user = user as User;
      this.hobbyParams = this.hobbyService.hobbyParams;
    });
  }

  ngOnInit(): void {
    this.loadHobbies();
    this.loadEvents();
    this.loadGuides();
  }
  loadEvents() {
    this.eventService.getEventsForMember().subscribe((res)=>{this.events = res?.result!;
      this.pagination = res.pagination!;})
  }
  loadGuides(){
    this.guideService.GetGuidesForUser(new GuideParams).subscribe((res)=>{this.guides = res?.result!;
      this.pagination = res.pagination!;})
  }
  loadHobbies() {
    this.hobbyService.hobbyParams = this.hobbyParams;
    this.hobbyService
      .getMemberFollowedHobbies(this.hobbyParams)
      .subscribe((res) => {
        this.hobbies = res?.result!;
        this.pagination = res.pagination!;
      });
  }
  pageChanged({ page }: any) {
    this.hobbyParams.pageNumber = page;
    this.hobbyService.hobbyParams = this.hobbyParams;
    this.loadHobbies();
  }
}
