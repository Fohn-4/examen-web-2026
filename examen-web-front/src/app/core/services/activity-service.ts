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

  private getAuthHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return token ? new HttpHeaders({ Authorization : `Bearer ${token}`}) : new HttpHeaders();
  }

  getAll(): Observable<ActivityModel[]> {
    const headers = this.getAuthHeaders();
    return this.http.get<ActivityModel[]>(`${this.apiUrl}/api/activities`, { headers });
  }

  load(): void {
    this.getAll().subscribe(data => this._activities.set(data));
  }

  delete(uuid: string): void {
    const headers = this.getAuthHeaders();
    this.http.delete(`${this.apiUrl}/api/activities/${uuid}`, { headers })
      .subscribe({
        next: () => this.load(),
        error: () => alert(`An error occured : delete failed`)
      });
  }

  create(name: string, description: string, isActive: boolean): void{
    const headers = this.getAuthHeaders();
    this.http.post( `${this.apiUrl}/api/activities` , { name, description, isActive }, {headers} ).subscribe({
      next : () => this.load(),
      error: () => alert("Error occured: failed to create")
    });
  }

  update(uuid: string, name: string, description: string, isActive: boolean): void {
    const headers = this.getAuthHeaders();
    this.http.put(`${this.apiUrl}/api/activities/${uuid}`, {name, description, isActive}, {headers}).subscribe({
      next: () => this.load(),
      error: () => alert('Error occured: failed to update')
    });
  }
}
