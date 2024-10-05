import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);

  // Verifica si el token existe en el localStorage
  const token = localStorage.getItem('token');

  if (!token) {
    // Si no hay token, redirigir a la página de login
    router.navigate(['/login']);
    return false; // Cancela la navegación
  }

  return true; // Permite la navegación si hay token
};
