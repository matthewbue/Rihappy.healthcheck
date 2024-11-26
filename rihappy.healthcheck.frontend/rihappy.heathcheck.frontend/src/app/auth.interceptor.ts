import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse, HttpRequest, HttpHandlerFn } from '@angular/common/http';
import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn) => {
  const router = inject(Router);
  const token = localStorage.getItem('token');

  // Clona a requisição e adiciona o token, se existir
  const clonedRequest = token
    ? req.clone({ headers: req.headers.set('Authorization', `Bearer ${token}`) })
    : req;

  return next(clonedRequest).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        // Remove token inválido e redireciona para o login
        localStorage.removeItem('token');
        localStorage.removeItem('isLoggedIn');
        router.navigate(['/login']);
      }
      return throwError(() => error); // Repassa o erro para outros interceptores ou chamadas
    })
  );
};
