import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
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
  hobbyId:number
  user!: User;
  guide: Guide | null = null;
  Form!: FormGroup;
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
    this.hobbyId = +url.split('/')[2];
    accountService.currentUser$.pipe(take(1)).subscribe((user: User | null) => {
      this.user = user as User;
    });
  }

  ngOnInit(): void {
    this.guideService.getGuide(this.guideId).subscribe((guide: Guide) => {
      this.guide = guide;
      this.canEdit = this.user.username == this.guide!.creatorUserName;
      this.Form=this.formBuilder.group({
        guideTitle: [guide.title,[ Validators.required]],
        guideContent: [guide.content,[ Validators.required]],
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
    if (this.Form!.valid) {
     this.guide!.content!= this.Form!.value.content
     this.guide?.title!= this.Form!.value.title
      this.guideService
        .UpdateGuide(this.guide!)
        .subscribe((guide: Guide) => {
          this.guide = guide;
          this.editMode = false;
        }
        );
    }
  }

  editorConfig: AngularEditorConfig = {
    editable: true,
      spellcheck: true,
      height: 'auto',
      minHeight: '0',
      maxHeight: 'auto',
      width: 'auto',
      minWidth: '0',
      translate: 'yes',
      enableToolbar: true,
      showToolbar: true,
      placeholder: 'Enter text here...', 
      toolbarHiddenButtons:[['insertVideo']]
};
}
