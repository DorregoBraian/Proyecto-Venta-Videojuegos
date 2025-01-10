import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ServisLoginService } from '../../Servicios/ServiceLogin/servis-login.service';
import { AlertPersonalizadosComponent } from '../../Alert/alert-personalizados/alert-personalizados.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-sesion-recuperar-password',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sesion-recuperar-password.component.html',
  styleUrl: './sesion-recuperar-password.component.css',
})
export class SesionRecuperarPasswordComponent implements OnInit {
  email: string = '';
  nuevaContrasena: string = '';
  confirmarContrasena: string = '';
  contrasenaNueva: boolean = false;
  confirmarContrasenaNueva: boolean = false;

  constructor(
    private dialog: MatDialog,
    private loginService: ServisLoginService
  ) {}

  ngOnInit(): void {
    
  }

  onSubmit(): void {
    // Validar contraseñas
    if (this.nuevaContrasena !== this.confirmarContrasena) {
      this.alertPersonalizado(
        'Campos no coincidentes',
        'Las contraseñas no coinciden.',
        '/Alert/AH AH AH.gif'
      );
      return;
    }
    if (!this.isValidEmail(this.email)) {
      this.alertPersonalizado(
        'Correo no válido',
        'Por favor, introduce un correo electrónico válido.',
        '/Alert/AH AH AH.gif'
      );
      return;
    }
    
    // Llamar al servicio para restablecer la contraseña
    this.loginService.recuperarContrasena(this.email, this.nuevaContrasena)
      .subscribe({
        next: (response) => {
          console.log('Respuesta del servidor:', response);
          alert(response.message);          // Redirigir al usuario al login o home
          window.location.href = '/login';
        },
        error: (err) => {
          console.error('Error al restablecer la contraseña:', err);
          alert('Hubo un error al restablecer la contraseña. Inténtalo de nuevo.');
        }
      });
  }

  alertPersonalizado(title: string, message: string, imageUrl: string): void {
      this.dialog.open(AlertPersonalizadosComponent, {
        data: {
          title: title,
          message: message,
          imageUrl: imageUrl,
        },
        width: '500px',
        disableClose: true, // Evita cerrar el diálogo haciendo clic fuera
      });
    }
    
  isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }
  
  toggleMostrarNuevaContrasena() {
    this.contrasenaNueva = !this.contrasenaNueva;
  }

  toggleMostrarConfirmarContrasena() {
    this.confirmarContrasenaNueva = !this.confirmarContrasenaNueva;
  }
}
