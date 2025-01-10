import { CommonModule, CurrencyPipe } from '@angular/common';
import { Component, Input, OnInit } from '@angular/core';
import { ServisFavoritosService } from '../../Servicios/ServiceFavoritos/servis-favoritos.service';
import { ServisCarritoService } from '../../Servicios/ServiceCarrito/servis-carrito.service';
import { MatDialog } from '@angular/material/dialog';
import { AlertPersonalizadosComponent } from '../../Alert/alert-personalizados/alert-personalizados.component';

@Component({
  selector: 'app-card-favoritos',
  standalone: true,
  imports: [CurrencyPipe, CommonModule],
  templateUrl: './card-favoritos.component.html',
  styleUrl: './card-favoritos.component.css',
})
export class CardFavoritosComponent implements OnInit {
  @Input() portada: string = ''; // URL de la imagen
  @Input() titulo: string = ''; // Título del juego
  @Input() precio: number = 0; // Precio unitario del juego
  @Input() id!: number; // ID del juego

  liked: boolean = false;
  disliked: boolean = false;

  constructor(
    private favoritosService: ServisFavoritosService,
    private carritoService: ServisCarritoService,
    private dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.carritoService.obtenerCarrito().subscribe((carrito) => {
      console.log('Carrito actualizado', carrito);
    });
  }

  like() {
    this.liked = true;
    this.disliked = false;
  }

  dislike() {
    this.disliked = true;
    this.liked = false; 
  }

  reset() {
    this.liked = false;
    this.disliked = false; 
  }

  agregarAlCarrito() {
    this.carritoService.agregarJuegoAlCarrito(this.id, 1);
    this.alertPersonalizado(
      'Carrito de Compras',
      `El Juego se agregado a tu carrito con la cantidad de 1 unidades.`,
      '/Alert/Al Carrito.gif'
    );
  }

  // Método para actualizar el localStorage
  eliminarProducto(id: number): void {
    this.favoritosService.eliminarFavoritos(id);
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
