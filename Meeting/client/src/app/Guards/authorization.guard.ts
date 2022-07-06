import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { map, Observable } from 'rxjs';
import { AccountService } from '../Services/account.service';

@Injectable({
  providedIn: 'root',
})
export class AuthorizationGuard implements CanActivate {
  constructor(
    private accountService: AccountService,
    private popUp: MatSnackBar
  ) {}

  openPopUp(message: string) {
    this.popUp.open(message, 'OK', { duration: 3500 });
  }

  canActivate(): Observable<boolean> {
    return this.accountService.currentUser$.pipe(
      map((user) => {
        if (user) return true;
        this.openPopUp('You are not authorized to access this page');
        return false;
      })
    );
  }
}
