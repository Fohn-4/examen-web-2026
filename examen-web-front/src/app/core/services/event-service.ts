import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EventModel } from '../models/event-model';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class EventService {
  apiUrl = environment.apiUrl

  constructor(private http: HttpClient){}

getAll(): Observable<EventModel[]> {
  const token = localStorage.getItem('token');
  const headers = token ? new HttpHeaders({ Authorization: `Bearer ${token}` }) : new HttpHeaders();
  return this.http.get<EventModel[]>(`${this.apiUrl}/api/events`, { headers });
}

}
