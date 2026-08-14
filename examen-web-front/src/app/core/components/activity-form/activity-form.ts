import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivityService } from '../../services/activity-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-activity-form',
  imports: [ReactiveFormsModule],
  templateUrl: './activity-form.html',
  styleUrl: './activity-form.css',
})
export class ActivityForm {
  private fb = inject(FormBuilder);
  private activityService = inject(ActivityService)
  private router = inject(Router);

  form = this.fb.group({
    name : ['', Validators.required],
    description : ['', Validators.required],
    isActive : [true]
  });

  onSubmit(): void {
    if(this.form.valid){
      this.activityService.create(this.form.value.name!, this.form.value.description!, this.form.value.isActive!);
      this.router.navigate(['/activities']);
    }
  }
}
