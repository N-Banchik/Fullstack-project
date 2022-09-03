import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { HobbyView } from 'src/app/Models/hobby-view';
import { Pagination } from 'src/app/Models/pagination';
import { HobbyParams } from 'src/app/Models/SearchParams/hobby-params';
import { HobbiesService } from 'src/app/Services/hobbies.service';

@Component({
  selector: 'app-hobby-show',
  templateUrl: './hobby-show.component.html',
  styleUrls: ['./hobby-show.component.css'],
})
export class HobbyShowComponent implements OnInit {
  categoryId!: number;
  hobbies: HobbyView[] = [];
  filterForm: FormGroup | undefined;
  hobbyParams: HobbyParams;
  pagination!: Pagination;
  constructor(
    public hobbyService: HobbiesService,
    private formBuilder: FormBuilder,
    private router: Router
  ) {
    this.hobbyParams = this.hobbyService.hobbyParams;
    this.hobbyParams.categoryId = this.categoryId;
  }

  ngOnInit(): void {
   let url= this.router.url
      this.categoryId = +url.split('/')[2];
      this.hobbyParams.categoryId = this.categoryId;
      this.initFilterForm();
      this.loadHobbies();
      
    }
  

  initFilterForm() {
    this.filterForm = this.formBuilder.group({
      keys: [''],
      itemsPerPage: [this.hobbyParams.pageSize],
    });
    
  }

  setItems() {
    this.pagination.currentPage = 1;
  }

  loadHobbies(){
    this.hobbyService.hobbyParams = this.hobbyParams;
    this.hobbyService.getHobbiesByCategory(this.hobbyParams).subscribe((res) => { res
      this.hobbies = res?.result!;
      this.pagination = res.pagination!;
    });
  }

  pageChanged({ page }: any) {
    this.hobbyParams.pageNumber = page;
    this.hobbyService.hobbyParams = this.hobbyParams;
    this.loadHobbies();
  }


}
