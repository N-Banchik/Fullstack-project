import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { finalize, Observable } from 'rxjs';
import { SpinnerService } from '../Services/spinner.service';

@Injectable()
export class RequestsInterceptor implements HttpInterceptor {

  constructor(private spinnerService: SpinnerService ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.spinnerService.busy();
    return next.handle(request).pipe(finalize(() => this.spinnerService.idle()));
  }
}
