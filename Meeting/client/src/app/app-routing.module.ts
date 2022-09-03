import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CategoriesPageComponent } from './Components/categories-page/categories-page.component';
import { CreateEventComponent } from './Components/create-event/create-event.component';
import { CreateGuideComponent } from './Components/create-guide/create-guide.component';
import { NotFoundComponent } from './Components/ErrorHandeling/not-found/not-found.component';
import { ServerErrorComponent } from './Components/ErrorHandeling/server-error/server-error.component';
import { EventShowComponent } from './Components/event-show/event-show.component';
import { GuideShowComponent } from './Components/guide-show/guide-show.component';
import { HobbyPageComponent } from './Components/hobby-page/hobby-page.component';
import { HobbyShowComponent } from './Components/hobby-show/hobby-show.component';
import { LandingPageComponent } from './Components/landing-page/landing-page.component';
import { LogInComponent } from './Components/Login/log-in.component';
import { RegistrationComponent } from './Components/registration/registration.component';
import { AuthorizationGuard } from './Guards/authorization.guard';
import { PreventUnsavedChangesGuard } from './Guards/prevent-unsaved-changes.guard';


const routes: Routes = [
  {
    path: '',
    component: LandingPageComponent,
    pathMatch: 'full',
  },
  {
    path: '',
    canActivate: [AuthorizationGuard],
    runGuardsAndResolvers: 'always',
    children: [
      {
        path: 'hobbies',
        loadChildren: () =>
          import('../app/Modules/hobby/hobby.module').then((m) => m.HobbyModule),
      },
      { path: 'categories', component: CategoriesPageComponent, pathMatch: 'full' },
      {path:'category/:categoryId', component: HobbyShowComponent},
      { path: 'hobby/:hobbyId', component: HobbyPageComponent },
      {path:"hobby/:hobbyId/event/create", component:CreateEventComponent},
      {path:"hobby/:hobbyId/guide/create", component:CreateGuideComponent},
      {path:"hobby/:hobbyId/event/:eventId", component:EventShowComponent},
      {path:"hobby/:hobbyId/guides/:guideId", component:GuideShowComponent},
    ],
  },
  {
    path: 'login',
    component: LogInComponent,
    pathMatch: 'full',
    
  },
  {
    path: 'registration',
    component: RegistrationComponent,
    pathMatch: 'full',
  },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'server-error', component: ServerErrorComponent },
  {
    path: '**',
    pathMatch: 'full',
    component: NotFoundComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
