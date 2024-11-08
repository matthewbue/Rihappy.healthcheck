import { Routes } from '@angular/router';
import { StatusComponent } from './components/panel-status/panel-status.component';
import { PanelGcpComponent } from './components/panel-gcp/panel-gcp.component';

export const routes: Routes = [
	{ path: 'status', component: StatusComponent },
	{ path: 'gcp', component: PanelGcpComponent },
	{ path: '', redirectTo: '/status', pathMatch: 'full' },
	{ path: '**', redirectTo: '/status' },
];
