import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  private fb = inject(FormBuilder)
  private authService = inject(AuthService)
  private router = inject(Router);
  public errorMessage = signal('');

  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
  });

  onSubmit(): void {
    if (this.form.valid && !this.passwordMismatch()) {
      this.authService.register(
        this.form.value.email!,
        this.form.value.password!,
        this.form.value.firstName!,
        this.form.value.lastName!).subscribe({
          next: () => this.router.navigate(['/events']),
          error: () => this.errorMessage.set('An error has occured')
        });
    } else {
      this.form.markAllAsTouched();
    }
  }

  passwordMismatch(): boolean {
    return this.form.value.password !== this.form.value.confirmPassword;
  }
}
