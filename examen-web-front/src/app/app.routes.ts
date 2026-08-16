import { Routes } from '@angular/router';
import { Register } from './core/components/register/register';
import { Login } from './core/components/login/login';
import { EventComponent } from './core/components/event/event';
import { Home } from './core/components/home/home';
import { Activity } from './core/components/activity/activity';
import { ActivityForm } from './core/components/activity-form/activity-form';
import { adminGuard } from './core/guards/admin-guard';


export const routes: Routes = [
    {path: '', component: Home},
    {path: 'auth/register', component: Register},
    {path: 'auth/login', component: Login},
    {path: 'events', component: EventComponent},
    {path: 'activities', component: Activity},
    {path: 'activities/new', component: ActivityForm, canActivate: [adminGuard]},
    {path: 'activities/:uuid/edit', component: ActivityForm, canActivate: [adminGuard]}
];
