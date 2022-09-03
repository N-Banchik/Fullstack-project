import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css'],
})
export class SideBarComponent implements OnInit {
  @Output() ToggleSidebarEvent = new EventEmitter();
 
  constructor(private router:Router) {}

  ngOnInit(): void {}

  ToggleSidebar() {
    this.ToggleSidebarEvent.emit();
  }
  
}
