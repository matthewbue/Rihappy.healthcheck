import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ModalComponent } from '../../modal/modal.component';

@Component({
  selector: 'app-panel-gcp',
  standalone: true,
  templateUrl: './panel-gcp.component.html',
  styleUrls: ['./panel-gcp.component.css'],
  imports: [CommonModule, ModalComponent]
})
export class PanelGcpComponent {
  platformStatus: string = 'GCP';
  platformStatusDescription: string = 'Todos os sistemas GCP estão em Pleno funcionamento.';
  
  gcpComponents = [
    { name: 'Checkout', status: 'Operational' },
    { name: 'Account', status: 'Operational' },
    { name: 'Catalog', status: 'Operational' }
  ];

  isModalVisible: boolean = false;
  selectedGroupName: string = '';
  selectedComponents: any[] = [];

  ongoingIncidents: any[] = []; // Aqui você pode preencher com incidentes em andamento
  showIncidentHistory = false; // Controla a visibilidade do histórico de incidentes

  // Histórico de incidentes (exemplo)
  incidentHistory = [
    { date: '2024-10-25', name: 'Problema de rede GCP', status: 'Resolved', description: 'Ocorreu um problema de rede que afetou vários serviços, resolvido após 15 minutos.' },
    { date: '2024-10-24', name: 'Falha no BigQuery', status: 'Resolved', description: 'O BigQuery teve uma interrupção temporária. O problema foi resolvido em 20 minutos.' },
    { date: '2024-10-18', name: 'Erro no Compute Engine', status: 'Resolved', description: 'Erro de instância no Compute Engine resolvido após investigação.' }
  ];

  openModal(component: any) {
    this.selectedComponents = [component];
    this.selectedGroupName = component.name;
    this.isModalVisible = true;
  }

  closeModal() {
    this.isModalVisible = false;
  }

  // Alterna a visibilidade do histórico
  toggleIncidentHistory() {
    this.showIncidentHistory = !this.showIncidentHistory;
  }
}
