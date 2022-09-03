import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TimeagoModule } from 'ngx-timeago';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { MatSliderModule } from '@angular/material/slider';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatSidenavModule} from '@angular/material/sidenav'
import { MatButtonModule } from '@angular/material/button';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatIconModule} from '@angular/material/icon';
import {MatCardModule} from '@angular/material/card';
import {MatInputModule} from '@angular/material/input';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GooglePlaceModule } from 'ngx-google-places-autocomplete';
import { FileUploadModule } from 'ng2-file-upload';
import { PhotoUploadComponent } from 'src/app/Components/photo-upload/photo-upload.component';
import {MatTableModule} from '@angular/material/table';
import { NgxSpinnerModule } from 'ngx-spinner';
import {MatDividerModule} from '@angular/material/divider';
import { FlexLayoutModule } from '@angular/flex-layout';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import {MatTabsModule} from '@angular/material/tabs';
import {MatPaginatorModule} from '@angular/material/paginator';
import { ScrollingModule } from '@angular/cdk/scrolling';
import {MatExpansionModule} from '@angular/material/expansion';
import { NgxMatDatetimePickerModule, NgxMatTimepickerModule } from '@angular-material-components/datetime-picker';
import { NgxMatMomentModule } from '@angular-material-components/moment-adapter';
import {MatTooltipModule} from '@angular/material/tooltip';
import { AngularEditorModule } from '@kolkov/angular-editor';


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    GooglePlaceModule,
    NgxGalleryModule,
    NgxSpinnerModule,
    TimeagoModule.forRoot(),
    FontAwesomeModule,
    MatSliderModule,
    MatSnackBarModule,
    MatSidenavModule,
    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    FormsModule,
    ReactiveFormsModule,
    FileUploadModule,
    MatTableModule,
    MatDividerModule,
    FlexLayoutModule,
    PaginationModule.forRoot(),
    MatTabsModule,
    MatPaginatorModule,
    ScrollingModule,
    MatExpansionModule,
    NgxMatDatetimePickerModule,
    NgxMatTimepickerModule,
    NgxMatMomentModule,
    MatTooltipModule,
    AngularEditorModule
    
    
  ],
  exports:[
    FormsModule,
    ReactiveFormsModule,
    GooglePlaceModule,
    TimeagoModule,
    NgxGalleryModule,
    FontAwesomeModule,
    MatSliderModule,
    MatSnackBarModule,
    MatSidenavModule,
    MatButtonModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    FileUploadModule,
    MatTableModule,
    NgxSpinnerModule,
    MatDividerModule,
    FlexLayoutModule,
    PaginationModule,
    MatTabsModule,
    MatPaginatorModule,
    ScrollingModule,
    MatExpansionModule,
    NgxMatDatetimePickerModule,
    NgxMatTimepickerModule,
    NgxMatMomentModule,
    MatTooltipModule,
    AngularEditorModule
    
    
  ]
})
export class StyleModule { }
