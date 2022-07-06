import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogInComponent } from './Components/Login/log-in.component';
import { RegistrationComponent } from './Components/registration/registration.component';

const routes: Routes = [
  { path: 'login', component: LogInComponent, pathMatch: 'full' },
  { path: 'registration', component: RegistrationComponent, pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}


