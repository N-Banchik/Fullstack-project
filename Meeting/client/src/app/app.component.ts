import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './Models/user';
import { AccountService } from './Services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  title = 'Hobbyist';
  sidebarShow = false;

  
  constructor(private accountService: AccountService) {}
  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userFromLS: any = localStorage.getItem('user');
    const user: User = JSON.parse(userFromLS);
    this.accountService.setCurrentUser(user);
  }
  ToggleSidebar() {
    this.sidebarShow = !this.sidebarShow;
  }
}
