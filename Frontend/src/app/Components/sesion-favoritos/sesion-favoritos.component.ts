import { Component, OnInit } from '@angular/core';
import { IJuego } from '../../Modelos/Modelo-Juego';
import { ServiceJuegosService } from '../../Servicios/ServiceJuegos/ServiceJuegos.service';
import { ServisFavoritosService } from '../../Servicios/ServiceFavoritos/servis-favoritos.service';
import { CardFavoritosComponent } from '../card-favoritos/card-favoritos.component';
import {
  CdkDragDrop,
  DragDropModule,
  moveItemInArray,
} from '@angular/cdk/drag-drop';

@Component({
  selector: 'app-sesion-favoritos',
  standalone: true,
  imports: [CardFavoritosComponent, DragDropModule],
  templateUrl: './sesion-favoritos.component.html',
  styleUrl: './sesion-favoritos.component.css',
})
export class SesionFavoritosComponent implements OnInit {
  listFavoritos: IJuego[] = []; // Lista de juegos favoritos

  constructor(
    private juegosService: ServiceJuegosService,
    private favoritosService: ServisFavoritosService
  ) {}

  ngOnInit(): void {
    // Suscribirse a los cambios en los favoritos
    this.favoritosService.obtenerFavoritos().subscribe((favoritosData) => {
      this.cargarFavoritos(favoritosData);
      console.log('la lista es ' + this.listFavoritos);
    });
  }

  cargarFavoritos(favoritosData: { id: number }[]): void {
    if (!favoritosData || favoritosData.length === 0) {
      this.listFavoritos = []; // Si no hay favoritos, lista vacía
      return;
    }

    // Convertir IDs a objetos IJuego
    const ids = favoritosData.map((f) => f.id);
    this.listFavoritos = this.listFavoritos.filter((juego) =>
      ids.includes(juego.id)
    );

    // Obtener detalles de nuevos juegos que no estén cargados
    favoritosData.forEach((juegoFavorito) => {
      if (!this.listFavoritos.find((j) => j.id === juegoFavorito.id)) {
        this.juegosService
          .getJuegoPorId(juegoFavorito.id)
          .subscribe((juego) => {
            this.listFavoritos.push(juego);
          });
      }
    });
  }

  // Método para manejar el drag & drop
  moverJuego(event: CdkDragDrop<IJuego[]>): void {
    moveItemInArray(
      this.listFavoritos,
      event.previousIndex,
      event.currentIndex
    );
  }

  // Método para optimizar la renderización en el *ngFor
  trackById(index: number, item: IJuego): number {
    return item.id;
  }
}
