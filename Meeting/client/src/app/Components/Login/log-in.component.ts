import { Component, OnInit } from '@angular/core';
import { FormBuilder,FormGroup,NgForm,Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { CanEdit } from 'src/app/Interfaces/can-edit';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit,CanEdit {
    public Form = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
  });
  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router ,private popUp: MatSnackBar) {}

    
    
    ngOnInit(): void {}
    
    openPopUp(message: string, status: number) {
      this.popUp.open(status+" "+message,"OK",{duration:3500});
    };
  logIn() {
  
    this.accountService.login(this.Form.value).subscribe(
      (data) => {
        this.router.navigateByUrl('hobbies');
      },
      (error) => {
       this.openPopUp('error', error);
      }
    );
  }

}
