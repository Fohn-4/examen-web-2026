import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivityService } from '../../services/activity-service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-activity-form',
  imports: [ReactiveFormsModule],
  templateUrl: './activity-form.html',
  styleUrl: './activity-form.css',
})
export class ActivityForm implements OnInit {

  private fb = inject(FormBuilder);
  private activityService = inject(ActivityService);
  private router = inject(Router);
  private activatedRoute = inject(ActivatedRoute);

  uuid = this.activatedRoute.snapshot.paramMap.get('uuid');

  form = this.fb.group({
    name: ['', Validators.required],
    description: ['', Validators.required],
    isActive: [true]
  });

  ngOnInit(): void {
    if (this.uuid) {
      const activity = this.activityService.activities().find(a => a.uuid === this.uuid);
      if (activity) {
        this.form.patchValue(activity)
      }
    }
  }

  onSubmit(): void {
    if (this.form.valid) {
      if (this.uuid) {
        this.activityService.update(this.uuid!, this.form.value.name!, this.form.value.description!, this.form.value.isActive!);
      } else {
        this.activityService.create(this.form.value.name!, this.form.value.description!, this.form.value.isActive!)
      }
      this.router.navigate(['/activities']);
    }
  }
}
