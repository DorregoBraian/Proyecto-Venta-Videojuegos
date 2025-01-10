import { Routes } from '@angular/router';
import { SesionHomeComponent } from './Components/sesion-home/sesion-home.component';
import { SesionAcercaDeComponent } from './Components/sesion-acerca-de/sesion-acerca-de.component';
import { SesionListJuegosComponent } from './Components/sesion-list-juegos/sesion-list-juegos.component';
import { SesionContactosComponent } from './Components/sesion-contactos/sesion-contactos.component';
import { DetalleJuegoComponent } from './Components/detalle-juego/detalle-juego.component';
import { SesionLoginComponent } from './Components/sesion-login/sesion-login.component';
import { SesionRegistrarComponent } from './Components/sesion-registrar/sesion-registrar.component';
import { SesionRecuperarPasswordComponent } from './Components/sesion-recuperar-password/sesion-recuperar-password.component';
import { SesionCarritoComponent } from './Components/sesion-carrito/sesion-carrito.component';
import { SesionDetalleJuegoComponent } from './Components/sesion-detalle-juego/sesion-detalle-juego.component';
import { SesionFavoritosComponent } from './Components/sesion-favoritos/sesion-favoritos.component';

export const routes: Routes = [
  { path: 'home', component: SesionHomeComponent },
  { path: 'carrito', component: SesionCarritoComponent },
  { path: 'favoritos', component: SesionFavoritosComponent },
  { path: 'acerca-de', component: SesionAcercaDeComponent },
  { path: 'list-juegos', component: SesionListJuegosComponent },
  { path: 'contactos', component: SesionContactosComponent },
  { path: 'login', component: SesionLoginComponent },
  { path: 'sing-up', component: SesionRegistrarComponent },
  { path: 'sing-up/login', component: SesionLoginComponent },
  { path: 'login/sing-up', component: SesionRegistrarComponent },
  { path: 'login/reset-password', component: SesionRecuperarPasswordComponent },
  {path: 'sesion-detalle/:id',component: SesionDetalleJuegoComponent,
    children: [
      { path: 'detalle', component: DetalleJuegoComponent }, // Subruta para "detalle"
      { path: 'carrito', component: SesionCarritoComponent }, // Subruta para "carrito"
      { path: 'favoritos', component: SesionFavoritosComponent }, // Subruta para "favoritos"
      { path: '', redirectTo: 'detalle', pathMatch: 'full' }, // Redirigir a "detalle" por defecto
    ],
  },
  { path: '**', redirectTo: 'home', pathMatch: 'full' },
];
