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
  private apiUrlAccount = 'https://api-superapp.gruporihappy.com.br/api/Checkout/hc';
  constructor(private http: HttpClient) { }

  getHealthStatus(): Observable<HealthStatusResponse> {
    return this.http.get<HealthStatusResponse>(this.apiUrl);
  }
  getHealthSuperAppAccount(): Observable<HealthStatusResponse> {
    return this.http.get<any>(this.apiUrlAccount).pipe(
      map((data) => {
        const components = Object.keys(data.entries).map((key) => ({
          name: key,
          description: data.entries[key].description || '',
          status: data.entries[key].status,
        }));

        return {
          categoryName: data.status === 'Healthy' ? 'Sistema em funcionamento' : 'Problemas no sistema',
          status: data.status,
          components: [
            {
              groupName: 'Status Geral',
              components,
            },
          ],
        };
      })
    );
  }
}

