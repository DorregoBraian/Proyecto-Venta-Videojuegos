import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ServisLoginService } from '../../Servicios/ServiceLogin/servis-login.service';
import { MatDialog } from '@angular/material/dialog';
import { AlertPersonalizadosComponent } from '../../Alert/alert-personalizados/alert-personalizados.component';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-sesion-login',
  standalone: true,
  imports: [RouterLink, FormsModule, CommonModule],
  templateUrl: './sesion-login.component.html',
  styleUrl: './sesion-login.component.css',
})
export class SesionLoginComponent implements OnInit {
  email: string = '';
  password: string = '';
  mostrarContrasena: boolean = false;
  mensajeRecuperacion: string = '';
  mantenerSesion: boolean = false; // Estado del checkbox "Recordarme"

  constructor(
    private router: Router,
    private dialog: MatDialog,
    private loginService: ServisLoginService,
  ) {}

  ngOnInit(): void {
    
  }

  login(): void {
    console.log('Datos de inicio de sesión:', this.email, this.password);

    if (!this.email || !this.password) {
      this.alertPersonalizado(
        'Campos incompletos',
        'Por favor, completa todos los campos.',
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
    
    this.loginService.login(this.email, this.password).subscribe({
      next: (response) => {
        console.log('Respuesta del servidor:', response);
        // Guardar el userId en localStorage
        localStorage.setItem('userId', response.userId.toString());
        console.log('Inicio de sesión exitoso, UserID:', response.userId);

        if (this.mantenerSesion) {
          // Guardar sesión persistente
          localStorage.setItem('isLoggedIn', 'true');
          localStorage.setItem('mantenerSesion',JSON.stringify(this.mantenerSesion));
          console.log('Estado de Mantener Sesion Abierta:',this.mantenerSesion);
        }else {
          // Guardar sesión temporal
          sessionStorage.setItem('isLoggedIn', 'true');
          localStorage.setItem('mantenerSesion',JSON.stringify(this.mantenerSesion));
          console.log('Estado de Mantener Sesion Abierta:',this.mantenerSesion);
        }
        this.alertPersonalizado2(
          'Inicio exitoso',
          response.message,
          '/Alert/Ok.gif'
        ).subscribe(() => {
          this.router.navigate(['/home']).then(() => {
            window.location.reload(); // Forzar recarga de la página
          });
        });
      },
      error: (err) => {
        console.error('Error al iniciar sesión:', err);
        this.alertPersonalizado(
          'Error al iniciar sesión',
          'El Email o la Contraseña ingresada son Incorrectos.',
          '/Alert/AH AH AH.gif'
        );
      },
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

  alertPersonalizado2(title: string,message: string,imageUrl: string): Observable<void> {
    const dialogRef = this.dialog.open(AlertPersonalizadosComponent, {
      data: { title, message, imageUrl },
      width: '400px',
      panelClass: 'custom-dialog-container',
    });
    return dialogRef.afterClosed(); 
  }

  onCheckboxChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.mantenerSesion = input.checked; // Actualiza el valor manualmente
    console.log('Checkbox cambiado:', input.checked);
  }

  isValidEmail(email: string): boolean {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
  }

  toggleMostrarContrasena(): void {
    this.mostrarContrasena = !this.mostrarContrasena;
  }
}
