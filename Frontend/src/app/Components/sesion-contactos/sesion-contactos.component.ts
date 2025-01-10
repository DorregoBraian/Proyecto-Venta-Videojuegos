import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ServisLoginService } from '../../Servicios/ServiceLogin/servis-login.service';
import { MatDialog } from '@angular/material/dialog';
import { AlertPersonalizadosComponent } from '../../Alert/alert-personalizados/alert-personalizados.component';

@Component({
  selector: 'app-sesion-contactos',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './sesion-contactos.component.html',
  styleUrl: './sesion-contactos.component.css',
})
export class SesionContactosComponent {
  nombre: string = '';
  correo: string = '';
  asunto: string = '';
  telefono: string = '';
  mensaje: string = '';

  constructor(
    private emailService: ServisLoginService,
    private dialog: MatDialog
  ) {}

  sendEmail() {
    this.emailService
      .enviarEmail(this.correo, this.asunto, this.mensaje)
      .subscribe({
        next: (response) => {
          console.log(response);
          this.alertPersonalizado(
            'Correo Enviado',
            'El correo se envio al destinatario',
            '/Alert/Ok.gif'
          );
        },
        error: (error) => {
          this.alertPersonalizado(
            'Error',
            'El correo no se pudo enviar correctamente',
            '/Alert/CorreoNoEnviado.gif'
          );
        },
      });
  }

  cancelar() {
    window.location.href = '/';
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
}
