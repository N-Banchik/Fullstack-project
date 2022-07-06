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
    this.Spinner.show(
      undefined,
      {
        // there are about 50 spinner types
        type: 'ball-clip-rotate-multiple',
        color: '#FF0000',
        bdColor: 'rgba(0, 0, 0, 0.8)',
        fullScreen: true,
      }
    );
  }
  idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.Spinner.hide();
    }
  }
}
