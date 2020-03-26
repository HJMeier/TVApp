import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { ParticipantComponent } from './components/participant/participant.component';
import { DataService } from './services/data-service';
import { ParticipantService } from './services/participant.service';
import { AppErrorHandler } from './common/app-error-handler';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ParticipantComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'participants', component: ParticipantComponent },
    ])
  ],
  providers: [
    ParticipantService,
    DataService,
    { provide: ErrorHandler, useClass: AppErrorHandler } // replaces globally ErrorHandler by AppErrorHandler
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
