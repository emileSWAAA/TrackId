import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { TrackDetailsComponent } from './track/track-details/track-details.component';
import { TrackOverviewComponent } from './track/track-overview/track-overview.component';
import { TrackUploadComponent } from './track/track-upload/track-upload.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      { path: '', component: TrackOverviewComponent },
      { path: 'track', component: TrackOverviewComponent },
      { path: 'track/upload', component: TrackUploadComponent },
      { path: 'track/:id', component: TrackDetailsComponent }
    ]
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
