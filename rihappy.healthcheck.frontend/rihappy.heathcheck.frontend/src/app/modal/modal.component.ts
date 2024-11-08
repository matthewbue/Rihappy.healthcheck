import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
	selector: 'app-modal',
	templateUrl: './modal.component.html',
	standalone: true,
	styleUrls: ['./modal.component.css'],
	imports: [CommonModule]
})
export class ModalComponent {
	@Input() components: any[] = [];
	@Input() groupName: string = '';
	@Output() close = new EventEmitter<void>();

	closeModal() {
		this.close.emit();
	}
}
