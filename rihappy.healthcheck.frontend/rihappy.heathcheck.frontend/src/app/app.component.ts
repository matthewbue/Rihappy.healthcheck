import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { StatusComponent } from "./components/panel-status/panel-status.component";
import { PanelGcpComponent } from './components/panel-gcp/panel-gcp.component';
import { CommonModule } from '@angular/common';
import { MenuComponent } from "./components/menu/menu.component";
import { ModalComponent } from './modal/modal.component';

@Component({
	selector: 'app-root',
	standalone: true,
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.css'],
	imports: [RouterOutlet, StatusComponent, CommonModule, MenuComponent, ModalComponent, PanelGcpComponent] // Removi a duplicação
})
export class AppComponent {
	title = 'rihappy.heathcheck.frontend';
	IsMenuCollapsed = false;

	onMenuToggle(isCollapsed: boolean) {
		this.IsMenuCollapsed = isCollapsed;
	}
}

