import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StyleModule } from '../style/style.module';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from 'src/app/Components/home/home.component';
import { UserPageComponent } from 'src/app/Components/user-page/user-page.component';
import { PhotoUploadComponent } from 'src/app/Components/photo-upload/photo-upload.component';
import { CategoriesPageComponent } from 'src/app/Components/categories-page/categories-page.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  {
    path: ':username',
    component: UserPageComponent,
  },
  
 
];

@NgModule({
  declarations: [
    HomeComponent,
    UserPageComponent,
    PhotoUploadComponent,
    
  ],
  imports: [CommonModule, StyleModule, RouterModule.forChild(routes)],
  exports: [
    RouterModule,
    HomeComponent,
    UserPageComponent,
    PhotoUploadComponent,
   
  ],
})
export class HobbyModule {}
