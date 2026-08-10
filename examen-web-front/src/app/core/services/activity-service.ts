import { Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ActivityModel } from '../models/activity-model';

@Injectable({
  providedIn: 'root',
})
export class ActivityService {
  apiUrl = environment.apiUrl

  constructor(private http: HttpClient) { }

  private _activities = signal<ActivityModel[]>([]);
  activities = this._activities.asReadonly();

  getAll(): Observable<ActivityModel[]> {
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({ Authorization: `Bearer ${token}` }) : new HttpHeaders();
    return this.http.get<ActivityModel[]>(`${this.apiUrl}/api/activities`, { headers });
  }

  load(): void {
    this.getAll().subscribe(data => this._activities.set(data));
  }
}
