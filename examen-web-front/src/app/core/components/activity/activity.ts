import { Component, inject, OnInit, computed, signal } from '@angular/core';
import { ActivityService } from '../../services/activity-service';
import { AuthService } from '../../services/auth-service';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-activity',
  imports: [CommonModule, RouterLink],
  templateUrl: './activity.html',
  styleUrl: './activity.css',
})
export class Activity implements OnInit {
  private activityService = inject(ActivityService)
  private authService = inject(AuthService)

  activities = this.activityService.activities;

  searchTerm = signal('');

  filtredActivities = computed( () => this.activities().filter( a => a.name.toLowerCase().includes(this.searchTerm().toLowerCase()) ))

  ngOnInit(): void {
    this.activityService.load();
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  delete(uuid : string): void {
    this.activityService.delete(uuid);
  }
}
