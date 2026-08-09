import { Component, inject } from '@angular/core';
import { ActivityService } from '../../services/activity-service';
import { CommonModule } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-activity',
  imports: [CommonModule],
  templateUrl: './activity.html',
  styleUrl: './activity.css',
})
export class Activity {
  private activityService = inject(ActivityService)

  activities = toSignal(this.activityService.getAll(), { initialValue: [] });
}
