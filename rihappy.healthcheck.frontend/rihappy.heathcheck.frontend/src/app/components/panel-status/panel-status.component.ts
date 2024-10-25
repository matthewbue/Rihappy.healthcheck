
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; 

@Component({
  selector: 'app-status',
  standalone: true, 
  templateUrl: './panel-status.component.html',
  styleUrls: ['./panel-status.component.css'],
  imports: [CommonModule] 
})
export class StatusComponent {
  platformStatus: string = 'Fully Operational';
  platformStatusDescription: string = 'All systems are running smoothly.';

  components = [
    { name: 'Storefront', status: 'Operational' },
    { name: 'Checkout', status: 'Operational' },
    { name: 'Admin', status: 'Operational' },
    { name: 'Developer Tools', status: 'Operational' },
  ];

  ongoingIncidents = [
    { name: 'Payment Gateway Issue', status: 'Degraded Performance', lastUpdate: '2024-10-24 14:03' },
    { name: 'Order Placement Delays', status: 'Monitoring', lastUpdate: '2024-10-24 15:27' }
  ];

  constructor() { }
}
