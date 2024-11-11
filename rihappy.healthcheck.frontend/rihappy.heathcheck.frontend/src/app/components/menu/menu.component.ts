import { CommonModule } from '@angular/common';
import { Component, Output, EventEmitter, OnInit } from '@angular/core';

@Component({
	selector: 'app-menu',
	standalone: true,
	templateUrl: './menu.component.html',
	styleUrls: ['./menu.component.css'],
	imports: [CommonModule]
})
export class MenuComponent implements OnInit {
	isCollapsed = false;
	activeTab: string = 'status';

	@Output() toggleCollapse = new EventEmitter<boolean>();

	toggleMenu() {
		this.isCollapsed = !this.isCollapsed;
		this.toggleCollapse.emit(this.isCollapsed);
	}

	selectedPlatform = 'google-cloud';
	transitioning = false;

	ngOnInit() {
		const savedTab = localStorage.getItem('activeTab');
		this.activeTab = savedTab ? savedTab : 'status';
	}

	setActiveTab(tab: string) {
		this.activeTab = tab;
		localStorage.setItem('activeTab', tab);
	}

	setActivePlatform(platform: string) {
		if (this.selectedPlatform !== platform) {
			this.transitioning = true;
			setTimeout(() => {
				this.selectedPlatform = platform;
				this.transitioning = false;
			}, 300);
		}
	}
}
