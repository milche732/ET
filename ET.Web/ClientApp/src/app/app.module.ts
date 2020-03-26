import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { ReactiveFormsModule } from '@angular/forms';

import { AlertComponent } from './alert/alert.component';

import { UserListComponent } from './user-list/user-list.component';
import { UserFormComponent } from './user-form/user-form.component';
import { AlertService } from './_services/alert.service'
import { UserService } from './_services/user.service';
import { GroupService } from './_services/group.service';


import { GenderPipe } from './pipes/gender.pipe';

import { routes } from './app.routes'; 
@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    UserListComponent,
    UserFormComponent,
    GenderPipe,
    AlertComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes)
  ],
  providers: [UserService, GroupService, AlertService],
  bootstrap: [AppComponent]
})
export class AppModule { }
