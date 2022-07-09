import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { User } from 'src/app/Models/user';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css'],
})
export class NavBarComponent implements OnInit {
  @Output() ToggleSidebarEvent = new EventEmitter();
  currentUser$!: Observable<User | null>;
  constructor(private accountService: AccountService, private router: Router) {
    this.currentUser$ = this.accountService.currentUser$;
  }

  ngOnInit(): void {}
  logout() {
    this.accountService.logout();
    this.router.navigate(['/']);
  }
  ToggleSidebar() {
    this.ToggleSidebarEvent.emit();
  }
}
