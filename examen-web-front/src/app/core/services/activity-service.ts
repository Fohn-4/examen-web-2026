import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ActivityModel } from '../models/activity-model';

@Injectable({
  providedIn: 'root',
})
export class ActivityService {
  apiUrl = environment.apiUrl

  constructor(private http: HttpClient){}

  getAll(): Observable<ActivityModel[]> {
    const token = localStorage.getItem('token');
    const headers = token ? new HttpHeaders({ Authorization : `Bearer ${token}`}): new HttpHeaders();
    return this.http.get<ActivityModel[]>(`${this.apiUrl}/api/activities`, { headers });
  }

}
