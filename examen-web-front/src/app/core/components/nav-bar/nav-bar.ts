import { Component, inject} from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { RouterLink } from '@angular/router';
import { DarkModeService } from '../../services/dark-mode-service';


@Component({
  selector: 'app-nav-bar',
  imports: [RouterLink],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.css',
})
export class NavBar {
  private authService = inject(AuthService);
  private darkModeService = inject(DarkModeService);

  toggleTheme(): void {
    this.darkModeService.toggle();
  }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  onLogOut(): void{
    this.authService.logOut();
  }
}
