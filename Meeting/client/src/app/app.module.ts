import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { StyleModule } from './Modules/style/style.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { JWTInterceptor } from './Interceptors/jwt.interceptor';
import { RequestsInterceptor } from './Interceptors/requests.interceptor';
import { ExceptionInterceptor } from './Interceptors/exception.interceptor';
import { AppRoutingModule } from './app-routing.module';
import { NavBarComponent } from './Components/Navigation/nav-bar/nav-bar.component';
import { SideBarComponent } from './Components/Navigation/side-bar/side-bar.component';
import { LogInComponent } from './Components/Login/log-in.component';
import {  FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegistrationComponent } from './Components/registration/registration.component';
import { GooglePlaceModule } from "ngx-google-places-autocomplete";


@NgModule({
  declarations: [AppComponent, NavBarComponent, SideBarComponent, LogInComponent, RegistrationComponent],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    StyleModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    GooglePlaceModule 
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
