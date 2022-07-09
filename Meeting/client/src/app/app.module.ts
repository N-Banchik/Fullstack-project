import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StyleModule } from './Modules/style/style.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JWTInterceptor } from './Interceptors/jwt.interceptor';
import { RequestsInterceptor } from './Interceptors/requests.interceptor';
import { ExceptionInterceptor } from './Interceptors/exception.interceptor';
import { NavBarComponent } from './Components/Navigation/nav-bar/nav-bar.component';
import { SideBarComponent } from './Components/Navigation/side-bar/side-bar.component';
import { LogInComponent } from './Components/Login/log-in.component';

import { RegistrationComponent } from './Components/registration/registration.component';

import { LandingPageComponent } from './Components/landing-page/landing-page.component';
import { NotFoundComponent } from './Components/ErrorHandeling/not-found/not-found.component';
import { ServerErrorComponent } from './Components/ErrorHandeling/server-error/server-error.component';
import { PhotoUploadComponent } from './Components/photo-upload/photo-upload.component';
import { CategoriesPageComponent } from './Components/categories-page/categories-page.component';
import { CategoryShowComponent } from './Components/category-show/category-show.component';
import { HobbyShowComponent } from './Components/hobby-show/hobby-show.component';
import { HobbyPageComponent } from './Components/hobby-page/hobby-page.component';
import { EventShowComponent } from './Components/event-show/event-show.component';
import { HobbyEventsShowComponent } from './Components/hobby-events-show/hobby-events-show.component';
import { HobbyGuideViewComponent } from './Components/hobby-guide-view/hobby-guide-view.component';
import { GuideShowComponent } from './Components/guide-show/guide-show.component';
import { PostShowComponent } from './Components/post-show/post-show.component';
import { CreateEventComponent } from './Components/create-event/create-event.component';



@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    SideBarComponent,
    LogInComponent,
    RegistrationComponent,
    LandingPageComponent,
    NotFoundComponent,
    ServerErrorComponent,
    CategoriesPageComponent,
    CategoryShowComponent,
    HobbyShowComponent,
    HobbyPageComponent,
    EventShowComponent,
    HobbyEventsShowComponent,
    HobbyGuideViewComponent,
    GuideShowComponent,
    PostShowComponent,
    CreateEventComponent
    
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    StyleModule,
    AppRoutingModule,
    HttpClientModule,
    
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JWTInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: RequestsInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ExceptionInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
