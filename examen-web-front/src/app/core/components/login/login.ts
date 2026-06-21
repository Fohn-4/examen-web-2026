import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private fb = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router);
  public errorMessage = signal('');
  

  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
  });


  onSubmit(): void {
    if (this.form.valid) {
      this.authService.login(
        this.form.value.email!,
        this.form.value.password!
      ).subscribe({
        next: () => this.router.navigate(['/events']),
        error:  () => this.errorMessage.set('Email ou mot de passe incorrect')
      })};
  }

}
