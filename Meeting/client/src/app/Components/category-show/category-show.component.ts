import { Component, Input, OnInit } from '@angular/core';
import { Category } from 'src/app/Models/category';

@Component({
  selector: 'app-category-show',
  templateUrl: './category-show.component.html',
  styleUrls: ['./category-show.component.css']
})
export class CategoryShowComponent implements OnInit {
  @Input() categoryId!: number;
  constructor() { }

  ngOnInit(): void {
  }

}
