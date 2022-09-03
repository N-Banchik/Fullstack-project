import { Component, Input, OnInit } from '@angular/core';
import { Post } from 'src/app/Models/post';

@Component({
  selector: 'app-post-show',
  templateUrl: './post-show.component.html',
  styleUrls: ['./post-show.component.css']
})
export class PostShowComponent implements OnInit {
@Input() posts:Post[]=[];
  constructor() { }

  ngOnInit(): void {
   
  }

}
