import { CommonModule } from '@angular/common';
import { Component, Output, EventEmitter } from '@angular/core';
import { StatusComponent } from "../panel-status/panel-status.component";
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-menu',
  standalone: true,
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css'],
  imports: [CommonModule, StatusComponent, RouterLink]
})
export class MenuComponent {
  isCollapsed = false;

  @Output() toggleCollapse = new EventEmitter<boolean>();

  toggleMenu() {
    this.isCollapsed = !this.isCollapsed;
    this.toggleCollapse.emit(this.isCollapsed); // Emitir o estado de colapso
  }
}


