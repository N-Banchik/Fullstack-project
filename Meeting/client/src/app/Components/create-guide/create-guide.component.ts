import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Guide } from 'src/app/Models/guide';
import { GuideCreate } from 'src/app/Models/guide-create';
import { GuideService } from 'src/app/Services/guide.service';

@Component({
  selector: 'app-create-guide',
  templateUrl: './create-guide.component.html',
  styleUrls: ['./create-guide.component.css']
})
export class CreateGuideComponent implements OnInit {
  hobbyId!: number;
  public Form!: FormGroup;
  constructor(private formBuilder: FormBuilder,private router: Router,private guideService:GuideService) 
  {  let url = this.router.url;
    this.hobbyId = +url.split('/')[2];
  }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.Form = this.formBuilder.group({
      guideTitle: ['', Validators.required],
      guideContent: ['', Validators.required],
    });
  }

  submitGuide(){
    let guide: GuideCreate ={
      title:this.Form.value.guideTitle,
      content:this.Form.value.guideContent
    }
this.guideService.CreateGuide(guide,this.hobbyId).subscribe(()=>this.router.navigateByUrl(`/hobby/${this.hobbyId}`));
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
