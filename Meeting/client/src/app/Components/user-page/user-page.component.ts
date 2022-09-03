import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { GooglePlaceDirective } from 'ngx-google-places-autocomplete';
import { Member } from 'src/app/Models/member';
import { AccountService } from 'src/app/Services/account.service';
import { GoogleAddressService } from 'src/app/Services/google-address.service';
import { MemberService } from 'src/app/Services/member.service';

@Component({
  selector: 'app-user-page',
  templateUrl: './user-page.component.html',
  styleUrls: ['./user-page.component.css'],
})
export class UserPageComponent implements OnInit {
  member!: Member;
  location: boolean = false;
  photo: boolean = false;
  password: boolean = false;
  public Form!: FormGroup
  @ViewChild("placesRef") placesRef : GooglePlaceDirective | undefined;
  constructor(
    private route: Router,
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private memberService: MemberService,
    private addressService:GoogleAddressService
  ) {}

  ngOnInit(): void {
    this.memberService.getMember().subscribe((member) => {
      this.member = member;
     
    });
  }
  openUpdateLocation()
  {
    this.location=!this.location
    this.password=false;
    this.photo=false;
    this.Form=this.formBuilder.group({
      country: [this.member.country, Validators.required],
      city: [this.member.city, Validators.required],
    });
  }
  updateLocation(){
    this.memberService.updateMember(this.member)
    this.Form = new FormGroup({});
    this.location=false;
  }

  handleAddressChange(address: any) {
    let addressFromService=this.addressService.GetAddressToString(address);
    this.Form.controls["city"].setValue(addressFromService.city);
    this.Form.controls["country"].setValue(addressFromService.country);
  }

  openPassword(){
    this.password=!this.password;
    this.location=false;
    this.photo=false;
    this.Form=this.formBuilder.group({
      currentPassword: ['', [Validators.required, Validators.minLength(6)]],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  updatePassword(){
    this.memberService.updatePassword(this.Form.value.currentPassword, this.Form.value.newPassword)
    this.Form = new FormGroup({});
    this.password=false;
  }

  openPhoto(){
    this.photo=!this.photo;
    this.location=false;
    this.password=false;
  }
}

