import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router, RouterLink } from '@angular/router';
import { AlertPersonalizadosComponent } from '../../Alert/alert-personalizados/alert-personalizados.component';
import { Observable } from 'rxjs';
import { ServisLoginService } from '../../Servicios/ServiceLogin/servis-login.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, CommonModule, FormsModule],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean = false;
  menuOpen = false;
  nombreUsuario: string = '';

  constructor(
    private dialog: MatDialog, 
    private router: Router,
    private loginService: ServisLoginService
  ) {}

  ngOnInit(): void {
    this.checkLoginState();
    this.datosUsuario();
  }

  
  toggleMenu() {
    this.menuOpen = !this.menuOpen;
  }

  closeMenu() {
    this.menuOpen = false;
  }

  // Método para verificar si la sesión está activa
  checkLoginState(): void {
    // Revisamos ambos tipos de almacenamiento
    const sessionLogin = sessionStorage.getItem('isLoggedIn') === 'true';
    const persistentLogin = localStorage.getItem('isLoggedIn') === 'true';
    
    // Si hay sesión activa en cualquiera, establecemos isLoggedIn en true
    this.isLoggedIn = sessionLogin || persistentLogin;
    console.log('Estado inicial de sesión (isLoggedIn):', this.isLoggedIn);
  }

  //Maneja el evento de cierre de sesión.
  logout(): void {
    localStorage.removeItem('isLoggedIn'); // Elimina el estado de sesión
    localStorage.removeItem('mantenerSesion'); // Elimina la preferencia de "Recordarme"
    sessionStorage.removeItem('isLoggedIn');
    this.isLoggedIn = false; // Actualiza el estado local
    this.alertPersonalizado2(
      'Adios y Gracias por la visita',
      'Grasias por visitar mi pagina, espero verte pronto.',
      '/Alert/Adios.gif'
    ).subscribe(() => {
      this.router.navigate(['/home']).then(() => {
        window.location.reload(); // Forzar recarga de la página
      });
    });
  }

  datosUsuario(): void {
    const idString = localStorage.getItem('userId');
    const id = idString ? parseInt(idString, 10) : 0;

    this.loginService.getDataUsuario(id).subscribe((data) => {
      this.nombreUsuario = data.nombre;
      console.log('Datos de usuario:', data);
      console.log('Nombre de usuario:', this.nombreUsuario);
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

}
