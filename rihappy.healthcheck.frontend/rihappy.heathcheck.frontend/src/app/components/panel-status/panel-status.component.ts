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
  platformStatusDescription: string = 'Todos os sistemas VTEX estão funcionando normalmente.';
  components: Group[] = [];
  ongoingIncidents: any[] = [];
  showIncidentHistory = false;

  // Modal variables
  isModalVisible: boolean = false;
  selectedGroupName: string = '';
  selectedComponents: any[] = [];

  // Sample incident history
  incidentHistory = [
    { date: '2024-10-25', name: 'Cloud provider issue causing elevated 5xx errors', status: 'Resolved', description: 'This incident was caused by a cloud provider issue affecting stores in Argentina. It was resolved after 26 minutes.' },
    // Additional historical incidents...
  ];

  constructor(private statusService: HealthStatusService) {}

  ngOnInit(): void {
    this.fetchStatus();
  }

  fetchStatus(): void {
    this.statusService.getHealthStatus().subscribe(
      (data: HealthStatusResponse) => {
        this.components = data.components;
        this.platformStatus = data.categoryName;

        this.ongoingIncidents = data.components
          .flatMap(group => group.components)
          .filter(component => component.status !== 'Operational')
          .map(component => ({
            name: component.name,
            status: component.status,
            lastUpdate: new Date(),
            description: component.description || 'Descrição não disponível',
            showTooltip: false // Estado inicial do tooltip
          }));

        this.platformStatusDescription = this.ongoingIncidents.length > 0 
          ? 'Alguns serviços VTEX estão apresentando problemas. Confira abaixo.' 
          : 'Todos os sistemas VTEX estão funcionando normalmente.';

        this.addOngoingIncidentsToHistory();
      },
      (error) => {
        console.error('Erro ao buscar status:', error);
        this.platformStatusDescription = 'Não foi possível verificar o status dos sistemas.';
      }
    );
  }

  addOngoingIncidentsToHistory(): void {
    this.ongoingIncidents.forEach(incident => {
      const existsInHistory = this.incidentHistory.some(
        history => history.name === incident.name && history.status === incident.status
      );

      if (!existsInHistory) {
        this.incidentHistory.push({
          date: new Date().toLocaleDateString(),
          name: incident.name,
          status: incident.status,
          description: incident.description
        });
      }
    });
  }

  // Alterna o estado do tooltip para exibir/esconder
  toggleTooltip(incident: any): void {
    incident.showTooltip = !incident.showTooltip;
  }
  getGroupStatus(group: Group): string {
    const allOperational = group.components.every(comp => comp.status === 'Operational');
    return allOperational ? 'Operational' : 'Degraded';
  }

  openModal(group: Group): void {
    this.selectedGroupName = group.groupName;
    this.selectedComponents = group.components;
    this.isModalVisible = true;
  }

  closeModal(): void {
    this.isModalVisible = false;
  }

  toggleIncidentHistory() {
    this.showIncidentHistory = !this.showIncidentHistory;
  }
}
