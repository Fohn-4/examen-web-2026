import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { EventModel } from '../models/event-model';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class EventService {
  apiUrl = environment.apiUrl

  constructor(private http: HttpClient) { }

  private _events = signal<EventModel[]>([]);
  events = this._events.asReadonly();

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return token ? new HttpHeaders({Authorization : `Bearer ${token}`}) : new HttpHeaders();
  }

  getAll(): Observable<EventModel[]> {
    const headers = this.getAuthHeaders();
    return this.http.get<EventModel[]>(`${this.apiUrl}/api/events`, { headers });
  }

  load(): void {
    this.getAll().subscribe(data => this._events.set(data));
  }

}
