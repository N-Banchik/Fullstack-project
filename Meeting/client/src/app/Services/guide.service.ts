import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Guide } from '../Models/guide';
import { GuideView } from '../Models/guide-view';
import { PaginatedResult } from '../Models/pagination';
import { GuideParams } from '../Models/SearchParams/guide-params';
import { User } from '../Models/user';
import { AccountService } from './account.service';
import { Observable, of, pipe, take, tap } from 'rxjs';
import { getPaginatedResult, getPaginationHeaders } from './pagination.service';
@Injectable({
  providedIn: 'root'
})
export class GuideService {
  baseUrl = environment.apiUrl + 'Guides/';
  user: User | any;
  guides = new Map<number, Guide>();
  guideViewCache = new Map<string, PaginatedResult<GuideView[]>>();
  guideParams: GuideParams;

  constructor(private http: HttpClient, accountService: AccountService) {
    accountService.currentUser$.pipe(take(1)).subscribe((user: User | null) => {
      this.user = user as User;
    });
    this.guideParams = new GuideParams();
  }

  public get GuideParams(): GuideParams {
    return this.guideParams;
  }
  public set GuideParams(EventParams: GuideParams) {
    this.guideParams = EventParams;
  }

  resetParams() {
    this.guideParams = new GuideParams();

    return this.guideParams;
  }

  getGuidesForHobby(
    guideParams: GuideParams
  ): Observable<PaginatedResult<GuideView[]>> {
    let key = Object.values(guideParams).join('-');
    const response = this.guideViewCache.get(key);

    if (response) {
      return of(response);
    }

    return getPaginatedResult<GuideView[]>(
      `${this.baseUrl}Hobbies`,
      this.appendParams(guideParams),
      this.http
    ).pipe(tap((response) => this.guideViewCache.set(key, response)));
  }

  GetGuidesForUser(
    guideParams: GuideParams
  ): Observable<PaginatedResult<GuideView[]>> {
    let key = Object.values(guideParams).join('-');
    const response = this.guideViewCache.get(key);

    if (response) {
      return of(response);
    }

    return getPaginatedResult<GuideView[]>(
      `${this.baseUrl}User`,
      this.appendParams(guideParams),
      this.http
    ).pipe(tap((response) => this.guideViewCache.set(key, response)));
  }

  getGuide(id: number): Observable<Guide> {
    if (this.guides.has(id)) {
      let guide = this.guides.get(id);
      return of(guide as Guide);}
    return this.http.get<Guide>(`${this.baseUrl}${id}`).pipe(tap((guide)=>this.guides.set(guide.id,guide)));}

    CreateGuide(guide:Guide,hobbyId:number)
    {
      return this.http.post<Guide>(`${this.baseUrl}`,{Title:guide.title,Content:guide.content,HobbyId:hobbyId});
    }

    UpdateGuide(guide:Guide)
    { return this.http.put<Guide>(`${this.baseUrl}`,{Id:guide.id,Title:guide.title,Content:guide.content});}












  appendParams(guideParams: GuideParams) {
    let params = getPaginationHeaders(
      guideParams.pageNumber,
      guideParams.pageSize
    );
    
    params = params.append('Id', guideParams.Id.toString());

    return params;
  }
}
