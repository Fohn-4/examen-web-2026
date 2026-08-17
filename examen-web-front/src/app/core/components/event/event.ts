import { Component, inject, OnInit } from '@angular/core';
import { EventService } from '../../services/event-service';
import { AuthService } from '../../services/auth-service';

import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-event',
  imports: [CommonModule],
  templateUrl: './event.html',
  styleUrl: './event.css',
})
export class EventComponent implements OnInit {

  private eventService = inject(EventService)
  private authService = inject(AuthService)
  events = this.eventService.events;

  ngOnInit(): void {
    this.eventService.load()
  }

  delete(uuid : string): void {
    this.eventService.delete(uuid);
  }

  isAdmin(): boolean {
   return this.authService.isAdmin();
  }
}
  

