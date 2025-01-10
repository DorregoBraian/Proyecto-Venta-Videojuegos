import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { ServisCarritoService } from '../../Servicios/ServiceCarrito/servis-carrito.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-card-carrito',
  standalone: true,
  imports: [CurrencyPipe],
  templateUrl: './card-carrito.component.html',
  styleUrl: './card-carrito.component.css',
})
export class CardCarritoComponent {
  @Input() portada: string = ''; // URL de la imagen
  @Input() titulo: string = ''; // Título del juego
  @Input() precio: number = 0; // Precio unitario del juego
  @Input() cantidad: number = 1; // Cantidad seleccionada
  @Input() id!: number; // ID del juego

  @Output() totalActualizado = new EventEmitter<number>(); // Creamos un Output para emitir el total al padre

  constructor(private carritoService: ServisCarritoService, private router: Router) {}

  // Método para decrementar la cantidad
  decrementarCantidad(): void {
    if (this.cantidad > 1) {
      this.cantidad--;
      this.emitirTotal();
      this.carritoService.actualizarCantidad(this.id, this.cantidad);
    }
  }

  // Método para incrementar la cantidad
  incrementarCantidad(): void {
    this.cantidad++;
    this.emitirTotal();
    this.carritoService.actualizarCantidad(this.id, this.cantidad);
  }

  // Método que emite el total al componente padre
  emitirTotal() {
    const total = this.precio * this.cantidad;
    this.totalActualizado.emit(total);
  }

  // Método para actualizar el localStorage
  eliminarProducto(id: number): void {
    this.carritoService.eliminarJuego(id);
  }
  
  navegarAlDetalle(id: number): void {
    // Navegar a la ruta con el ID del juego
    this.router.navigate(['/sesion-detalle', id]);
  }
}
