
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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

  
  constructor(private accountService: AccountService,
    private route:Router) {}
  ngOnInit(): void {
    this.setCurrentUser();
  }

  setCurrentUser() {
    const userFromLS: any = localStorage.getItem('user');
    const user: User = JSON.parse(userFromLS);
    if (user) {
      this.accountService.setCurrentUser(user);
      this.route.navigateByUrl('/hobbies');
    }

    
  }
  ToggleSidebar() {
    this.sidebarShow = !this.sidebarShow;
  }
}
