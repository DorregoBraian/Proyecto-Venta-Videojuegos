import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-sesion-registrar',
  standalone: true,
  imports: [CommonModule,FormsModule,RouterLink],
  templateUrl: './sesion-registrar.component.html',
  styleUrl: './sesion-registrar.component.css',
})
export class SesionRegistrarComponent {
  nombre: string = '';
  email: string = '';
  password: string = '';
  passwordError: string | null = null;

  onRegister() {
    if (this.validatePassword(this.password)) {
      console.log('Usuario registrado:', {
        nombre: this.nombre,
        email: this.email,
        password: this.password,
      });
    }
  }

  private validatePassword(password: string): boolean {
    const regex =
      /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$/;
    if (!regex.test(password)) {
      this.passwordError =
        'La contraseña debe tener al menos 8 caracteres, incluyendo una mayúscula, una minúscula, un número y un carácter especial.';
      return false;
    }
    this.passwordError = null;
    return true;
  }
}
