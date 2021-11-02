import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { RegisterComponent } from './components/register/register.component';
import { WeatherFormComponent } from './components/weather-form/weather-form.component';
import { AuthGuardService } from './services/auth-guard.service';

const routes: Routes = [
  { path : '', pathMatch:'full', component: WeatherFormComponent, canActivate:[AuthGuardService] },
  { path : 'register', component : RegisterComponent },
  { path : 'login', component : LoginComponent },
  { path :  '**', pathMatch: 'full', component:NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
