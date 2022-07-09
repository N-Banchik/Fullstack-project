import { Component, Input, OnInit } from '@angular/core';
import { EventView } from 'src/app/Models/event-view';

@Component({
  selector: 'app-hobby-events-show',
  templateUrl: './hobby-events-show.component.html',
  styleUrls: ['./hobby-events-show.component.css']
})
export class HobbyEventsShowComponent  {
@Input() eventView: EventView[] = [];
  constructor() { }

}


