import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Event } from '../Models/event';
import { EventView } from '../Models/event-view';
import { PaginatedResult } from '../Models/pagination';
import { User } from '../Models/user';
import { AccountService } from './account.service';
import { Observable, of, pipe, take, tap } from 'rxjs';
import { EventParams } from '../Models/SearchParams/event-params';
import { getPaginatedResult, getPaginationHeaders } from './pagination.service';
import { Member } from '../Models/member';
import { Post } from '../Models/post';
import { EventCreate } from '../Models/event-create';
@Injectable({
  providedIn: 'root',
})
export class EventsService {
  baseUrl = environment.apiUrl + 'Events/';
  user: User | any;
  events = new Map<number, Event>();
  eventViewCache = new Map<string, PaginatedResult<EventView[]>>();
  eventParams: EventParams;

  constructor(private http: HttpClient, accountService: AccountService) {
    accountService.currentUser$.pipe(take(1)).subscribe((user: User | null) => {
      this.user = user as User;
    });
    this.eventParams = new EventParams();
  }

  public get EventParams(): EventParams {
    return this.eventParams;
  }
  public set EventParams(EventParams: EventParams) {
    this.eventParams = EventParams;
  }

  resetParams() {
    this.eventParams = new EventParams();

    return this.eventParams;
  }
  getAllEvents(
    eventParams: EventParams
  ): Observable<PaginatedResult<EventView[]>> {
    let key = Object.values(eventParams).join('-');
    const response = this.eventViewCache.get(key);

    if (response) {
      return of(response);
    }

    return getPaginatedResult<EventView[]>(
      `${this.baseUrl}`,
      this.appendParams(eventParams),
      this.http
    ).pipe(tap((response) => this.eventViewCache.set(key, response)));
  }
  getEvent(id: number): Observable<Event> {
    if (this.events.has(id)) {
      let event = this.events.get(id);
      return of(event as Event);
    }

    return this.http
      .get<Event>(`${this.baseUrl}${id}`)
      .pipe(tap((event) => this.events.set(event.id, event)));
  }
  getEventsForHobby(
    eventParams: EventParams
  ): Observable<PaginatedResult<EventView[]>> {
    let key = Object.values(eventParams).join('-');
    const response = this.eventViewCache.get(key);

    if (response) {
      return of(response);
    }

    return getPaginatedResult<EventView[]>(
      `${this.baseUrl}hobby`,
      this.appendParams(eventParams),
      this.http
    ).pipe(tap((response) => this.eventViewCache.set(key, response)));
  }
  getEventsByName(
    eventParams: EventParams
  ): Observable<PaginatedResult<EventView[]>> {
    let key = Object.values(eventParams).join('-');
    const response = this.eventViewCache.get(key);

    if (response) {
      return of(response);
    }

    return getPaginatedResult<EventView[]>(
      `${this.baseUrl}EventName`,
      this.appendParams(eventParams),
      this.http
    ).pipe(tap((response) => this.eventViewCache.set(key, response)));
  }
  getEventsByUser(
    
  ): Observable<PaginatedResult<EventView[]>> {
    this.EventParams.userId = this.user.id;
    let key = Object.values(this.EventParams).join('-');
    const response = this.eventViewCache.get(key);

    if (response) {
      return of(response);
    }

    return getPaginatedResult<EventView[]>(
      `${this.baseUrl}Creator`,
      this.appendParams(this.EventParams),
      this.http
    ).pipe(tap((response) => this.eventViewCache.set(key, response)));
  }
  GetMembersForEvent(eventId: number) {
    return this.http.get<Member[]>(`${this.baseUrl}${eventId}/Members`);
  }
  getEventsForMember(
    
  ): Observable<PaginatedResult<EventView[]>> {
    this.EventParams.userId = this.user.id;
    let key = Object.values(this.EventParams).join('-');
    const response = this.eventViewCache.get(key);

    if (response) {
      return of(response);
    }

    return getPaginatedResult<EventView[]>(
      `${this.baseUrl}Member/Events`,
      this.appendParams(this.EventParams),
      this.http
    ).pipe(tap((response) => this.eventViewCache.set(key, response)));
  }
  CancelEvent(id: number) {
    return this.http.delete(`${this.baseUrl}Cancel/${id}`);
  }
  EditEvent(event: Event) {
    return this.http.put(`${this.baseUrl}`, {
      Id: event.id,
      EventTitle: event.eventTitle,
      EventDescription: event.eventDescription,
      EventDate: event.eventDate,
      EventLocation: event.eventLocation,
      EventRules: event.eventRules,
      Canceled: event.canceled,
    });
  }

  CreateEvent(event: EventCreate) {
    return this.http.post(`${this.baseUrl}`, {
      EventTitle: event.eventTitle,
      EventDescription: event.eventDescription,
      EventDate: event.eventDate,
      EventLocation: event.eventLocation,
      EventRules: event.eventRules,
      HobbyID: event.hobbyId,
    });
  }

  ChangeAttendingToEvent(eventId: number) {
    return this.http.post(`${this.baseUrl}Attend/${eventId}`, {});
  }
  registerForEvent(eventId: number) {
    return this.http.post(`${this.baseUrl}RSVP/${eventId}`, {});
  }

  CreateComment(comment: string, eventId: number) {
    return this.http.post(`${this.baseUrl}Comment`, {
      PostContent: comment,
      EventId: eventId,
    });
  }

  UpdateComment(comment: string, commentId: number) {
    return this.http.put(`${this.baseUrl}Comment`, {
      Comment: comment,
      Id: commentId,
    });
  }

  DeleteComment(commentId: number) {
    return this.http.delete(`${this.baseUrl}Comment/${commentId}`);
  }

  GetCommentsForEvent(eventId: number) {
    return this.http.get<Post[]>(`${this.baseUrl}Comments/${eventId}`);
  }
  appendParams(eventParams: EventParams) {
    let params = getPaginationHeaders(
      eventParams.pageNumber,
      eventParams.pageSize
    );
    params = params.append('hobbyId', eventParams.hobbyId.toString());
    params = params.append('userId', eventParams.userId.toString());
    params = params.append('date', eventParams.date.toString());
    params = params.append('eventId', eventParams.eventId.toString());
    params = params.append('categoryId', eventParams.categoryId.toString());

    return params;
  }
}
