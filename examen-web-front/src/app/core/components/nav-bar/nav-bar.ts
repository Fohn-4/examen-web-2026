import { Component, inject, Inject} from '@angular/core';
import { AuthService } from '../../services/auth-service';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-nav-bar',
  imports: [RouterLink],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.css',
})
export class NavBar {
  private authService = inject(AuthService);

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  onLogOut(): void{
    this.authService.logOut();
  }
}
