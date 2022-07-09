import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { take } from 'rxjs';
import { Guide } from 'src/app/Models/guide';
import { User } from 'src/app/Models/user';
import { AccountService } from 'src/app/Services/account.service';

import { GuideService } from 'src/app/Services/guide.service';

@Component({
  selector: 'app-guide-show',
  templateUrl: './guide-show.component.html',
  styleUrls: ['./guide-show.component.css'],
})
export class GuideShowComponent implements OnInit {
  guideId: number;
  user!: User;
  guide: Guide | null = null;
  form!: FormGroup;
  editMode: boolean = false;
  canEdit: boolean = false;
  constructor(
    private router: Router,
    private guideService: GuideService,
    private accountService: AccountService,
    private formBuilder: FormBuilder
  ) {
    let url = this.router.url;
    this.guideId = +url.split('/')[4];
    accountService.currentUser$.pipe(take(1)).subscribe((user: User | null) => {
      this.user = user as User;
    });
  }

  ngOnInit(): void {
    this.guideService.getGuide(this.guideId).subscribe((guide: Guide) => {
      this.guide = guide;
      this.canEdit = this.user.username == this.guide!.creatorUserName;
      this.form=this.formBuilder.group({
        title: [guide.title,[ Validators.required]],
        content: [guide.content,[ Validators.required]],
      });
      
    }
    );
    
  }

  edit() {
    this.editMode = true;
    
  }

  cancelEdit() {
    this.editMode = false;
  }

  submitGuide() {
    if (this.form!.valid) {
     this.guide!.content!= this.form!.value.content
     this.guide?.title!= this.form!.value.title
      this.guideService
        .UpdateGuide(this.guide!)
        .subscribe((guide: Guide) => {
          this.guide = guide;
          this.editMode = false;
        }
        );
    }
  }
}
