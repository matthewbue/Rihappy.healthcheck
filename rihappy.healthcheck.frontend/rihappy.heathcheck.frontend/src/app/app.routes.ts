import { Routes } from '@angular/router';
import { StatusComponent } from './components/panel-status/panel-status.component';
import { PanelGcpComponent } from './components/panel-gcp/panel-gcp.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './auth.guard';

export const routes: Routes = [
	{ path: 'status', component: StatusComponent, canActivate: [AuthGuard] },
	{ path: 'gcp', component: PanelGcpComponent, canActivate: [AuthGuard] },
	{ path: 'login', component: LoginComponent },
  	{ path: '**', redirectTo: 'login' },
];		
