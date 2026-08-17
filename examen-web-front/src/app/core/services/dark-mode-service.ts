import { Injectable, signal } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DarkModeService {
  isDark = signal(false);

  toggle(): void {
    this.isDark.update(a => !a);
    document.documentElement.setAttribute('data-theme', this.isDark() ? 'dark' : 'light');
  }
}
