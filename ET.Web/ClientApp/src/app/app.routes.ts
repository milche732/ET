import { UserListComponent } from './user-list/user-list.component';
import { UserFormComponent } from './user-form/user-form.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';

import { Routes, RouterModule } from '@angular/router';

export const routes: Routes = [
    { path: '', redirectTo: 'list', pathMatch:'full' },
    { path: 'new', component: UserFormComponent },
    { path: 'edit/:id', component: UserFormComponent },
    { path: 'fetch-data', component: FetchDataComponent },
    { path: 'list', component: UserListComponent } ,
    { path: 'list/:term', component: UserListComponent } 
];
