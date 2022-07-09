import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root',
})
export class SpinnerService {
  busyRequestCount = 0;
  constructor(private Spinner: NgxSpinnerService) {}

  busy() {
    this.busyRequestCount++;
    this.Spinner.show();
  }
  idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.Spinner.hide();
    }
  }
}
