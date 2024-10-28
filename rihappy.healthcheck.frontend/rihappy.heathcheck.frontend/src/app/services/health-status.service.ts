import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// Interface para tipar a resposta da API
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
}

@Injectable({
  providedIn: 'root',
  
})
export class HealthStatusService {

  private apiUrl = 'https://localhost:7233/api/Health/status';

  constructor(private http: HttpClient) { }

  getHealthStatus(): Observable<HealthStatusResponse> {
    return this.http.get<HealthStatusResponse>(this.apiUrl);
  }
}
