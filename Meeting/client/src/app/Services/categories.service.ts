import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, tap } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Category } from '../Models/category';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService {
  categories: Category[] = [];
  baseUrl: string = environment.apiUrl + 'Categories';
  constructor(private http: HttpClient) {}

  getCategories(): Observable<Category[]> {
    if (this.categories.length > 0) {
      return of(this.categories);
    }
    return this.http
      .get<Category[]>(this.baseUrl)
      .pipe(tap((cat) => {this.categories = cat;}));
  }

  getCategory(id: number) {
    return this.http.get(this.baseUrl + '/' + id).pipe(
      tap((category) => {
        if (this.categories.includes(category as Category)) {
          this.categories[this.categories.indexOf(category as Category)] =
            category as Category;
        } else {
          this.categories.push(category as Category);
        }
      })
    );
  }

  addCategory(categoryName:string, description:string) {
    return this.http.post(this.baseUrl, {categoryName, description}).pipe(
      tap((category) => {
        this.categories.push(category as Category);
      })
    );
  }

  updateCategory(category: Category) {
    return this.http.put(this.baseUrl + '/' + category.id, category).pipe(
      tap((category) => {
        this.categories[this.categories.indexOf(category as Category)] =
          category as Category;
      })
    );
  }

}
