import { Component, OnDestroy, OnInit } from '@angular/core';
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
export class StatusComponent implements OnInit, OnDestroy{
  platformStatus: string = 'VTEX';
  platformStatusDescription: string = 'Os sistemas estão em pleno funcionamento 😃';
  private intervalId: any;
  components: Group[] = [];
  ongoingIncidents: any[] = [];
  showIncidentHistory = false;
   dataAtual = new Date();
  datenow = this.dataAtual.toLocaleDateString('pt-BR');;
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
    this.fetchStatus(); //faz a chamada inicial
    this.startAutoRefresh(); //inicia o intervalo de atualização
  }

  ngOnDestroy(): void{
    clearInterval(this.intervalId); // Limpa o intervalo ao destruir o componente 
  }

  fetchStatus(): void {
    this.statusService.getHealthStatus().subscribe(
        (data: HealthStatusResponse) => {
            this.components = data.components;
            this.platformStatus = data.categoryName;

            const hasDegradedComponents = data.components.some(group => 
                group.components.some(component => component.status === 'Degraded')
            );

            this.ongoingIncidents = data.components
                .flatMap(group => group.components)
                .filter(component => component.status !== 'Operational')
                .map(component => ({
                    name: component.name,
                    status: component.status,
                    lastUpdate: new Date(),
                    description: component.description || 'Descrição não disponível',
                    showTooltip: false
                }));

            this.platformStatusDescription = hasDegradedComponents
                ? 'Alguns serviços estão apresentando problemas ⚠️'
                : 'Os sistemas estão em pleno funcionamento 😃';

            this.addOngoingIncidentsToHistory();
        },
        (error) => {
            console.error('Erro ao buscar status:', error);
            this.platformStatusDescription = 'Não foi possível verificar o status dos sistemas.';
        }
    );
}

// Inicia o intervalo de atualização automática
startAutoRefresh(): void {
  this.intervalId = setInterval(() => {
    this.fetchStatus(); // Faz a chamada à API
  }, 30000); // 30 segundos
}

  // Verifica se há problemas nos componentes
  private checkForIssues(data: HealthStatusResponse): boolean {
    return data.components.some(group =>
      group.components.some(component => component.status === 'Degraded')
    );
  }

  // Método `trackBy` para otimizar a renderização do loop *ngFor
  trackByGroup(index: number, group: any): string {
    return group.groupName; // Identifica cada grupo de forma única
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
  hasDegradedComponents(): boolean {
    return this.components.some(group => 
        group.components.some(component => component.status === 'Degraded')
    );
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
