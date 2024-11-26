import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { StatusComponent } from "./components/panel-status/panel-status.component";
import { PanelGcpComponent } from './components/panel-gcp/panel-gcp.component';
import { CommonModule } from '@angular/common';
import { MenuComponent } from "./components/menu/menu.component";
import { ModalComponent } from './modal/modal.component';
import { LoginComponent } from './login/login.component';

@Component({
	selector: 'app-root',
	standalone: true,
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css'],
	imports: [RouterOutlet, CommonModule, MenuComponent] 
})
export class AppComponent {
	title = 'rihappy.heathcheck.frontend';
	isMenuVisible = true;
	IsMenuCollapsed = false;

	constructor(private router: Router) {
		// Observa mudanças na rota e decide a visibilidade do menu
		this.router.events.subscribe(() => {
		  this.isMenuVisible = this.router.url !== '/login'; // Oculta o menu na página de login
		});
	}	

	onMenuToggle(isCollapsed: boolean) {
		this.IsMenuCollapsed = isCollapsed;
	}
}

