import { Routes } from '@angular/router';
import { StatusComponent } from './components/panel-status/panel-status.component';
import { PanelGcpComponent } from './components/panel-gcp/panel-gcp.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth.guard';

export const routes: Routes = [
	{ path: 'status', component: StatusComponent },
	{ path: 'gcp', component: PanelGcpComponent },
	{ path: 'login', component: LoginComponent },
	{ path: 'health-check', component: StatusComponent, canActivate: [AuthGuard] },
  	{ path: 'health-check', component: PanelGcpComponent, canActivate: [AuthGuard] },
  	{ path: '**', redirectTo: 'login' },
];		
