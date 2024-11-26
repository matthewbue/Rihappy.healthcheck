import { Component } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [RouterModule, FormsModule, CommonModule,],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  loginData = { username: '', password: '' };
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit(): void {
    this.authService.login(this.loginData.username, this.loginData.password).subscribe({
      next: (response: any) => {
        if (response.success && response.token) {
          localStorage.setItem('token', response.token);
          localStorage.setItem('isLoggedIn', 'true');
          this.router.navigate(['/status']);
        } else {
          this.errorMessage = response.message || 'Erro no login';
        }
      },
      error: (err) => {
        this.errorMessage = 'Falha ao se conectar ao servidor';
        console.error(err);
      },
    });
  }
}

