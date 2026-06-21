import { Routes } from '@angular/router';
import { Register } from './core/components/register/register';
import { Login } from './core/components/login/login';
import { EventComponent } from './core/components/event/event';
import { Home } from './core/components/home/home';


export const routes: Routes = [
    {path: '', component: Home},
    {path: 'auth/register', component: Register},
    {path: 'auth/login', component: Login},
    {path: 'events', component: EventComponent}
];
