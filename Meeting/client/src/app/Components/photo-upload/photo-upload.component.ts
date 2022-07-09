import { Component, Input, OnInit } from '@angular/core';
import { FileUploader, FileUploaderOptions } from 'ng2-file-upload';
import { take } from 'rxjs';
import { Member } from 'src/app/Models/member';
import { User } from 'src/app/Models/user';
import { AccountService } from 'src/app/Services/account.service';
import { MemberService } from 'src/app/Services/member.service';
import { environment } from 'src/environments/environment';


@Component({
  selector: 'app-photo-upload',
  templateUrl: './photo-upload.component.html',
  styleUrls: ['./photo-upload.component.css']
})
export class PhotoUploadComponent implements OnInit {
  @Input() member!: Member;

  uploader!: FileUploader ;
  hasBaseDropZoneOver: boolean | undefined;

  baseurl = environment.apiUrl;

  user: User|null=null;
  constructor(
    private accountService: AccountService,
    private memberService: MemberService
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.user = user as User;
    });
  }

  ngOnInit(): void {
    this.initializeUploader();
  }

  initializeUploader() {
    const options: FileUploaderOptions = {
      url: `${this.baseurl}members/photo`,
      authToken: `Bearer ${this.user!.token}`,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
      itemAlias:"Test description"
    };
    this.uploader = new FileUploader(options);

    
    
    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo = JSON.parse(response);
        this.member.photo=photo;

        if (photo.isMain) {
          this.user!.photoUrl = photo.url;
          this.member.photoUrl = photo.url;
          this.accountService.setCurrentUser(this.user!);
        }
      }
    };
  }

  fileOverBase(e: any) {
    this.hasBaseDropZoneOver = e;
  }
}
