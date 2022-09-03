import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Router, NavigationExtras } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class ExceptionInterceptor implements HttpInterceptor {
  constructor(private router: Router, private popUp: MatSnackBar) {}

  openPopUp(message: string, status: number) {
    this.popUp.open(status+" "+message,"OK",{duration:3500});
  };

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((err) => {
        switch (err.status) {
          case 400:
            if (err.error?.errors) {
              const modelStateErrors: string[][] = [];
              for (const key in err.error.errors) {
                if (err.error.errors[key]) {
                  modelStateErrors.push(err.error.errors[key]);
                }
              }
              throw modelStateErrors.flat();
            } else {
              this.openPopUp(
                (err.statusText === 'OK' ? 'Bad Request' : err.statusText) +
                  `: ${err.error}`,
                err.status
              );
            }
            break;
          case 401:
            this.openPopUp(
              (err.statusText === 'OK' ? 'Unauthorized' : err.statusText) +
                `: ${err.error}`,
              err.status
            );
            break;
          case 404:
            this.router.navigateByUrl('/not-found');
            break;
          case 500:
            const navigationExtras: NavigationExtras = {
              state: { error: err.error },
            };
            this.router.navigateByUrl('/server-error', navigationExtras);
            break;

          default:
            this.openPopUp('Something unexpected want wrong', err.status);
            break;
        }
        throw err;
      })
    );
  }
}
