import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Hobby } from '../Models/hobby';
import { HobbyView } from '../Models/hobby-view';
import { PaginatedResult } from '../Models/pagination';
import { HobbyParams } from '../Models/SearchParams/hobby-params';
import { User } from '../Models/user';
import { AccountService } from './account.service';
import { Observable, of, pipe, take, tap } from 'rxjs';
import { getPaginatedResult, getPaginationHeaders } from './pagination.service';
import { Member } from '../Models/member';

@Injectable({
  providedIn: 'root',
})
export class HobbiesService {
  baseUrl = environment.apiUrl + 'hobby/';
  user: User | any;
  hobbies = new Map<number, Hobby>();
  hobbyViewCache = new Map<string, PaginatedResult<HobbyView[]>>();
  hobbyParams: HobbyParams;

  constructor(private http: HttpClient, accountService: AccountService) {
    accountService.currentUser$.pipe(take(1)).subscribe((user: User | null) => {
      this.user = user as User;
    });
    this.hobbyParams = new HobbyParams();
  }

  public get HobbyParams(): HobbyParams {
    return this.hobbyParams;
  }
  public set HobbyParams(HobbyParams: HobbyParams) {
    this.hobbyParams = HobbyParams;
  }

  resetParams() {
    this.hobbyParams = new HobbyParams();

    return this.hobbyParams;
  }

  getAllHobbies(
    hobbyParams: HobbyParams
  ): Observable<PaginatedResult<HobbyView[]>> {
    let key = Object.values(hobbyParams).join('-');
    const response = this.hobbyViewCache.get(key);

    if (response) {
      return of(response);
    }

    return getPaginatedResult<HobbyView[]>(
      `${this.baseUrl}`,
      this.appendParams(hobbyParams),
      this.http
    ).pipe(tap((response) => this.hobbyViewCache.set(key, response)));
  }

  getHobby(id: number): Observable<Hobby> {
    if (this.hobbies.has(id)) {
      let hobby = this.hobbies.get(id);
      return of(hobby as Hobby);
    }

    return this.http
      .get<Hobby>(`${this.baseUrl}${id}`)
      .pipe(tap((hobby) => this.hobbies.set(hobby.id, hobby)));
  }

  getHobbyMembers(id: number): Observable<Member[]> {
    return this.http.get<Member[]>(`${this.baseUrl}${id}/members`);
  }

  getMemberFollowedHobbies(
    hobbyParams: HobbyParams
  ): Observable<PaginatedResult<HobbyView[]>> {
    this.hobbyParams.userId = this.user.id;
    let key = Object.values(hobbyParams).join('-');
    const response = this.hobbyViewCache.get(key);

    if (response) {
      return of(response);
    }

    return getPaginatedResult<HobbyView[]>(
      `${this.baseUrl}followed`,
      this.appendParams(hobbyParams),
      this.http
    ).pipe(tap((response) => this.hobbyViewCache.set(key, response)));
  }

  followHobby(id: number): Observable<any> {
    return this.http.post(`${this.baseUrl}Follow/${id}`, {});
  }

  unFollowHobby(id: number): Observable<any> {
    return this.http.post(`${this.baseUrl}UnFollow/${id}`, {});
  }

  getHobbiesByCategory(
    hobbyParams: HobbyParams
  ): Observable<PaginatedResult<HobbyView[]>> {
    let key = Object.values(hobbyParams).join('-');
    const response = this.hobbyViewCache.get(key);

    if (response) {
      return of(response);
    }

    return getPaginatedResult<HobbyView[]>(
      `${this.baseUrl}Category`,
      this.appendParams(hobbyParams),
      this.http
    ).pipe(tap((response) => this.hobbyViewCache.set(key, response)));
  }

  GetHobbiesByKeys(
    hobbyParams: HobbyParams
  ): Observable<PaginatedResult<HobbyView[]>> {
    let key = Object.values(hobbyParams).join('-');
    const response = this.hobbyViewCache.get(key);

    if (response) {
      return of(response);
    }

    return getPaginatedResult<HobbyView[]>(
      `${this.baseUrl}FindByFitures`,
      this.appendParams(hobbyParams),
      this.http
    ).pipe(tap((response) => this.hobbyViewCache.set(key, response)));
  }

  UpdateCategories(hobbyId: number, category: number): Observable<any> {
    return this.http.post(`${this.baseUrl}UpdateCategory`, {
      HobbyId: hobbyId,
      CategoryId: category,
    });
  }
  UpdateHobby(hobby: Hobby): Observable<any> {
    this.hobbies.set(hobby.id, hobby);
    return this.http.post(`${this.baseUrl}UpdateHobby`, {
      Id: hobby.id,
      HobbyName: hobby.hobbyName,
      Description: hobby.description,
      Rules: hobby.rules,
      keyFeatures: hobby.keyFeatures,
    });
  }

  CreateHobby(hobby: Hobby,categoryId:number): Observable<any> {
    return this.http.post(`${this.baseUrl}`, {HobbyName: hobby.hobbyName,
      Description: hobby.description,
      Rules: hobby.rules,
      keyFeatures: hobby.keyFeatures,categoryId:categoryId})}

  appendParams(hobbyParams: HobbyParams) {
    let params = getPaginationHeaders(
      hobbyParams.pageNumber,
      hobbyParams.pageSize
    );
    params = params.append('hobbyId', hobbyParams.hobbyId.toString());
    params = params.append('userId', hobbyParams.userId.toString());
    params = params.append('keyFeatures', hobbyParams.keyFeatures);
    params = params.append('categoryId', hobbyParams.categoryId.toString());

    return params;
  }
}
