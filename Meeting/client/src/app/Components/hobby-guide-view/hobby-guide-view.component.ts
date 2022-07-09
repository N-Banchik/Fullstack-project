import { Component, Input, OnInit } from '@angular/core';
import { GuideView } from 'src/app/Models/guide-view';

@Component({
  selector: 'app-hobby-guide-view',
  templateUrl: './hobby-guide-view.component.html',
  styleUrls: ['./hobby-guide-view.component.css']
})
export class HobbyGuideViewComponent implements OnInit {
@Input() guideView:GuideView[] = [];

  constructor() { }

  ngOnInit(): void {
  }

}
