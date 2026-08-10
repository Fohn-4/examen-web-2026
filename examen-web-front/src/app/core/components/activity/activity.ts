import { Component, inject, OnInit } from '@angular/core';
import { ActivityService } from '../../services/activity-service';
import { AuthService } from '../../services/auth-service';
import { CommonModule } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-activity',
  imports: [CommonModule],
  templateUrl: './activity.html',
  styleUrl: './activity.css',
})
export class Activity implements OnInit {
  private activityService = inject(ActivityService)
  private authService = inject(AuthService)

  activities = this.activityService.activities;

  ngOnInit(): void {
    this.activityService.load();
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }
}
