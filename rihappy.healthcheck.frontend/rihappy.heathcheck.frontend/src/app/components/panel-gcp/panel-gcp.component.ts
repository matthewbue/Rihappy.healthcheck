import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalComponent } from '../../modal/modal.component';
import { HealthStatusService, Group, HealthStatusResponse } from '../../services/health-status.service';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-panel-gcp',
  standalone: true,
  templateUrl: './panel-gcp.component.html',
  styleUrls: ['./panel-gcp.component.css'],
  imports: [CommonModule, ModalComponent, HttpClientModule]
})
export class PanelGcpComponent {
  platformStatus: string = 'VTEX';
  platformStatusDescription: string = 'Os sistemas estão em pleno funcionamento 😃';
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
    this.fetchStatus();
  }

  fetchStatus(): void {
    // Dados mockados em vez da chamada ao backend
    this.components = [
        {
            groupName: 'Checkout',
            components: [
                { name: 'mongo', status: 'Operational', description: 'Banco de dados MongoDB' },
                { name: 'Vtex', status: 'Operational', description: 'Plataforma VTEX' },
                { name: 'viacep', status: 'Operational', description: 'Serviço de CEP via CEP' }
            ]
        },
        {
            groupName: 'Account',
            components: [
                { name: 'mongo', status: 'Operational', description: 'Banco de dados MongoDB' },
                { name: 'Vtex', status: 'Operational', description: 'Plataforma VTEX' },
                { name: 'viacep', status: 'Operational', description: 'Serviço de CEP via CEP' }
            ]
        },
        {
            groupName: 'Catalog',
            components: [
                { name: 'mongo', status: 'Operational', description: 'Banco de dados MongoDB' },
                { name: 'Vtex', status: 'Operational', description: 'Plataforma VTEX' },
                { name: 'viacep', status: 'Operational', description: 'Serviço de CEP via CEP' }
            ]
        }
    ];

    // Definindo a variável `platformStatus`
    this.platformStatus = 'Google Cloud'; // Ou qualquer outro nome de plataforma que desejar

    // Verificando se há componentes degradados (simulação)
    const hasDegradedComponents = this.components.some(group =>
        group.components.some(component => component.status === 'Degraded')
    );

    // Simulando a lista de incidentes em andamento
    this.ongoingIncidents = this.components
        .flatMap(group => group.components)
        .filter(component => component.status !== 'Operational')
        .map(component => ({
            name: component.name,
            status: component.status,
            lastUpdate: new Date(),
            description: component.description || 'Descrição não disponível',
            showTooltip: false
        }));

    // Descrição do status da plataforma
    this.platformStatusDescription = hasDegradedComponents
        ? 'Alguns serviços estão apresentando problemas ⚠️'
        : 'Os sistemas estão em pleno funcionamento 😃';

    // Simulação de histórico de incidentes (opcional)
    this.addOngoingIncidentsToHistory();
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