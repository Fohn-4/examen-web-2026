import { Component, inject } from '@angular/core';
import { EventService } from '../../services/event-service';

import { CommonModule } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-event',
  imports: [CommonModule],
  templateUrl: './event.html',
  styleUrl: './event.css',
})
export class EventComponent {

  private eventService = inject(EventService)

  events = toSignal(this.eventService.getAll(), { initialValue: [] });
}
  

