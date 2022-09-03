import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GooglePlaceDirective } from 'ngx-google-places-autocomplete';
import { AccountService } from 'src/app/Services/account.service';
import { GoogleAddressService } from 'src/app/Services/google-address.service';
import { Event } from 'src/app/Models/event';
import { EventsService } from 'src/app/Services/events.service';
import { EventCreate } from 'src/app/Models/event-create';
import {
  NgxMatDatetimeContent,
  NgxMatDatetimePicker,
} from '@angular-material-components/datetime-picker';

@Component({
  selector: 'app-create-event',
  templateUrl: './create-event.component.html',
  styleUrls: ['./create-event.component.css'],
})
export class CreateEventComponent implements OnInit {
  hobbyId!: number;
  public Form!: FormGroup;
  eventLocation!: string;
  @ViewChild('picker') picker!: NgxMatDatetimePicker<any>;
  @ViewChild('placesRef') placesRef: GooglePlaceDirective | undefined;
  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private addressService: GoogleAddressService,
    private eventService: EventsService
  ) {
    let url = this.router.url;
    this.hobbyId = +url.split('/')[2];
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  handleAddressChange(address: any) {
    this.eventLocation = this.addressService.GetAddressToEvent(address);
  }

  initializeForm() {
    this.Form = this.formBuilder.group({
      eventTitle: ['', Validators.required],
      eventDescription: ['', Validators.required],
      eventDate: ['', Validators.required],
      eventLocation: ['', Validators.required],
      eventRules: ['', Validators.required],
    });
  }

  createEvent() {
    let eventCreate: EventCreate = {
      eventTitle: this.Form.value.eventTitle,
      eventDescription: this.Form.value.eventDescription,
      eventDate: this.Form.value.eventDate,
      eventLocation: this.eventLocation,
      eventRules: this.Form.value.eventRules,
      hobbyId: this.hobbyId,
    };
    this.eventService.CreateEvent(eventCreate).subscribe(
      (data) => {
        this.router.navigateByUrl('/hobbies');
      }
    );
    
  }
  
}
