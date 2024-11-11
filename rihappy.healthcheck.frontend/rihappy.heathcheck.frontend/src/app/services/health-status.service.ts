import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';

export interface Component {
	name: string;
	description: string;
	status: string;
}

export interface Group {
	groupName: string;
	components: Component[];
}

export interface HealthStatusResponse {
	categoryName: string;
	components: Group[];
	status: string;
}

@Injectable({
	providedIn: 'root',
})
export class HealthStatusService {
	private apiUrl = 'https://localhost:7233/api/Health/status';
	private apiUrlAccount = 'https://localhost:7233/api/HealthSuperApp/superApp';

	constructor(private http: HttpClient) { }

	getHealthStatus(): Observable<HealthStatusResponse> {
		return this.http.get<HealthStatusResponse>(this.apiUrl);
	}

	getHealthSuperAppAccount(): Observable<HealthStatusResponse[]> {
		return this.http.get<any>(this.apiUrlAccount).pipe(
			map((data: any[]) => {
				return data.map((category: any) => {
					const components = Object.keys(category.entries).map((key) => ({
						name: key,
						description: category.entries[key].description || 'Descrição não disponível',
						status: category.entries[key].status === 'Healthy' ? 'Operational' : 'Degraded'
					}));
					console.log(category)
					return {
						categoryName: category.groupName,
						status: category.status,
						components: [
							{
								groupName: category.groupName,
								components,
							} as Group,
						],
					} as HealthStatusResponse;
				});
			})
		);
	}
}
