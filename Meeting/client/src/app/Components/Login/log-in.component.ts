import { Component, OnInit } from '@angular/core';
import { FormBuilder,Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {
    public loginForm = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
  });
  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,) {}
  

  ngOnInit(): void {}

  logIn() {
    console.log(this.loginForm.value);
    this.accountService.login(this.loginForm.value).subscribe(
      (data) => {
        console.log('response', data);
        this.router.navigateByUrl('/');
      },
      (error) => {
        console.log('error', error);
      }
    );
  }

}
