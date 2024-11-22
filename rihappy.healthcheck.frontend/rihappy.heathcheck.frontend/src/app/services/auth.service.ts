import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
	providedIn: 'root',
  })
  export class AuthService {
	private apiUrl = 'https://localhost:7233/api/auth/login';
  
	constructor(private http: HttpClient) {}
  
	login(username: string, password: string): Observable<any> {
	  return this.http.post(`${this.apiUrl}`, { username, password });
	}
  
	getProtectedData(): Observable<any> {
	  const token = localStorage.getItem('token');		
	  const headers = new HttpHeaders({
		Authorization: `Bearer ${token}`,
	  });
	  return this.http.get(`${this.apiUrl}/healthcheck/status`, { headers });
	}
  }
