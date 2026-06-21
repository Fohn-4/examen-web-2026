import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { inject } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {


  apiUrl = environment.apiUrl
  private router = inject(Router);
  constructor(private http: HttpClient) { }


  register(email: string, password: string, firstName: string, lastName: string): Observable<{ token: string }> {
    return this.http.post<{ token: string }>(`${this.apiUrl}/api/users/register`, { email, password, firstName, lastName })
      .pipe(
        tap(response => localStorage.setItem('token', response.token))
      );
  }

  login(email: string, password: string): Observable<{token: string}>{
    return this.http.post<{ token: string }>(`${this.apiUrl}/api/users/auth`, { email, password})
      .pipe(
        tap(response => localStorage.setItem('token', response.token))
      );
  }

  isLoggedIn(): boolean {
    return localStorage.getItem('token') !== null
  }

  logOut(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/']);
  }

}
