import { Token } from '@angular/compiler';
import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const AuthGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = localStorage.getItem('token');
  const isLoggedIn = token !==null;

  if (!isLoggedIn) {
    router.navigate(['/login']);
    return false;
  }
  return true;
};
