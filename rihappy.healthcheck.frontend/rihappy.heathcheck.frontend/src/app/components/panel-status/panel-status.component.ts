import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalComponent } from '../../modal/modal.component';
import { HealthStatusService, Group, HealthStatusResponse } from '../../services/health-status.service';
import { HttpClientModule } from '@angular/common/http';


@Component({
  selector: 'app-status',
  standalone: true,
  templateUrl: './panel-status.component.html',
  styleUrls: ['./panel-status.component.css'],
  imports: [CommonModule, ModalComponent, HttpClientModule]
})
export class StatusComponent {
  platformStatus: string = 'VTEX';
  platformStatusDescription: string = 'Todos os sistemas Vtex estao em Pleno funcionamento.';
  components: Group[] = [];
  ongoingIncidents: any[] = []; // Incidentes em andamento, pode ser preenchido posteriormente
  showIncidentHistory = false; // Controla a visibilidade do histórico de incidentes

  // Variáveis para o modal
  isModalVisible: boolean = false;
  selectedGroupName: string = '';
  selectedComponents: any[] = [];

incidentHistory = [
  { date: '2024-10-25', name: 'Cloud provider issue causing elevated 5xx errors', status: 'Resolved', description: 'This incident was caused by a cloud provider issue affecting stores in Argentina. It was resolved after 26 minutes.' },
  { date: '2024-10-24', name: 'Degraded performance for order management', status: 'Resolved', description: 'An issue with the OMS and order-related modules caused degraded performance. The problem was resolved.' },
  { date: '2024-10-18', name: 'Issue with VTEX ID login services', status: 'Resolved', description: 'Users experienced error 503 when trying to log in. The issue was resolved after 30 minutes.' },
  { date: '2024-10-15', name: 'Checkout Delays', status: 'Resolved', description: 'Checkout delays were observed in multiple regions and resolved after monitoring.' },
  { date: '2024-09-30', name: 'Payment Gateway Outage', status: 'Resolved', description: 'A temporary issue with the payment gateway was resolved in under an hour.' },
  { date: '2024-09-25', name: 'Shipping API Issue', status: 'Resolved', description: 'Shipping rate calculation issues were resolved within 45 minutes.' },
  { date: '2024-09-18', name: 'OMS Delays', status: 'Resolved', description: 'Order Management System experienced delays, but the issue was mitigated quickly.' },
  { date: '2024-08-22', name: 'API Downtime', status: 'Resolved', description: 'Interruption in API services affecting multiple systems. It was resolved after 30 minutes.' },
  { date: '2024-08-10', name: 'Order Placement Failures', status: 'Resolved', description: 'Order placement failures affected several merchants but were resolved after investigation.' },
  { date: '2024-07-30', name: 'Shipping Calculation Delays', status: 'Resolved', description: 'Inconsistencies in shipping rate calculations were fixed after an hour of monitoring.' }
];
  constructor(private statusService: HealthStatusService) {}

  ngOnInit(): void {
    this.fetchStatus();
  }

  fetchStatus(): void {
    this.statusService.getHealthStatus().subscribe(
      (data: HealthStatusResponse) => {
        this.components = data.components;
        this.platformStatus = data.categoryName; // Ajusta o nome da categoria
      },
      (error) => {
        console.error('Erro ao buscar status:', error);
      }
    );
  }

  // Função para determinar o status do grupo com base nos componentes filhos
  getGroupStatus(group: Group): string {
    const allOperational = group.components.every(comp => comp.status === 'Operational');
    return allOperational ? 'Operational' : 'Degraded';
  }

  // Método para abrir o modal com os componentes do grupo
  openModal(group: Group): void {
    this.selectedGroupName = group.groupName;
    this.selectedComponents = group.components;
    this.isModalVisible = true;
  }

  // Método para fechar o modal
  closeModal(): void {
    this.isModalVisible = false;
  }

  // Alterna a visibilidade do histórico
  toggleIncidentHistory() {
    this.showIncidentHistory = !this.showIncidentHistory;
  }
}
