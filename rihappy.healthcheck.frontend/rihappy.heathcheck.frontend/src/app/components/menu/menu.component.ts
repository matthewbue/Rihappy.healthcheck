import { CommonModule } from '@angular/common';
import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-menu',
  standalone: true,
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css'],
  imports: [CommonModule]
})
export class MenuComponent {
  isCollapsed = false;
  activeTab: string = 'status'; // Define a aba ativa inicial

  setActiveTab(tab: string) {
    console.log(tab)
    this.activeTab = tab;
  }
  @Output() toggleCollapse = new EventEmitter<boolean>();

  toggleMenu() {
    this.isCollapsed = !this.isCollapsed;
    this.toggleCollapse.emit(this.isCollapsed);
  }

  selectedPlatform = 'google-cloud';
  transitioning = false;

  setActivePlatform(platform: string) {
    if (this.selectedPlatform !== platform) {
      this.transitioning = true;
      setTimeout(() => {
        this.selectedPlatform = platform;
        this.transitioning = false;
      }, 300); // Define o tempo da transição para 300ms
    }
  }
}
