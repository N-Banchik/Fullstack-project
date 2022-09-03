import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { GooglePlaceDirective } from 'ngx-google-places-autocomplete';
import { CanEdit } from 'src/app/Interfaces/can-edit';
import { AccountService } from 'src/app/Services/account.service';
import { GoogleAddressService } from 'src/app/Services/google-address.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit,CanEdit {
  @ViewChild("placesRef") placesRef : GooglePlaceDirective | undefined;
  public Form!: FormGroup
  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private router: Router,
    private addressService:GoogleAddressService,private popUp: MatSnackBar) {}

    
    ngOnInit(): void {this.InitializeForm();}
    openPopUp(message: string, status: number) {
      this.popUp.open(status+" "+message,"OK",{duration:3500});
    };

 InitializeForm() {
  this.Form=this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    country: ['', Validators.required],
    city: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    userName: ['', Validators.required],
    dateOfBirth: ['', Validators.required],
  });
 }

  Register() {
    
    this.accountService.register(this.Form.value).subscribe(
      (data) => {
        this.router.navigateByUrl('/hobbies');
      },
      (error) => {
        this.openPopUp('error', error);
      }
    );
  }

  handleAddressChange(address: any) {
    let addressFromService=this.addressService.GetAddressToString(address);
    this.Form.controls["city"].setValue(addressFromService.city);
    this.Form.controls["country"].setValue(addressFromService.country);
  }
  
  ReturnToHomepage(e:Event){
    e.stopImmediatePropagation();
    this.router.navigateByUrl("/")
  }

}


