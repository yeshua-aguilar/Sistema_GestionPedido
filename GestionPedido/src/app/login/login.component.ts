import { Component } from '@angular/core';
import { CardModule } from 'primeng/card';
import { FloatLabelModule } from 'primeng/floatlabel';
import { ButtonModule } from 'primeng/button';
import { FormsModule } from '@angular/forms';

import { ApiloginService } from '../apilogin.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CardModule, FloatLabelModule, FormsModule, ButtonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  usuarioName: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private apiService: ApiloginService, private router: Router) {}

  // Método para iniciar sesión
  onLogin() {
    const credentials = { usuarioName: this.usuarioName, password: this.password };

    this.apiService.login(credentials).subscribe({
      next: (response) => {
        console.log('Inicio de sesión exitoso', response);
        localStorage.setItem('token', response.token);
        this.router.navigate(['/home']);
      },
      error: (error) => {
        console.error('Error de inicio de sesión', error);
        this.errorMessage = 'Credenciales incorrectas';
      }
    });
  }

}
