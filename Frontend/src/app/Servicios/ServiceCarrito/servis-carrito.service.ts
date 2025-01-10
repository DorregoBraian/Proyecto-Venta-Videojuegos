import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ServisCarritoService {
  private carritoSubject = new BehaviorSubject<{ id: number; cantidad: number }[]>([]);

  constructor() {
    this.cargarCarritoDesdeLocalStorage();
  }

  // Carga el carrito asociado al userId actual desde el localStorage
  private cargarCarritoDesdeLocalStorage(): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      const carrito = JSON.parse(localStorage.getItem(`carrito_${userId}`) || '[]');
      this.carritoSubject.next(carrito);
    } else {
      // Si no hay un usuario logueado, se asegura que la lista esté vacía
      this.carritoSubject.next([]);
    }
  }

  // Permite observar cambios en el carrito
  obtenerCarrito(): Observable<{ id: number; cantidad: number }[]> {
    return this.carritoSubject.asObservable();
  }

  // Agregar un juego al carrito
  agregarJuegoAlCarrito(id: number, cantidad: number): void {
    const carrito = this.carritoSubject.getValue();
    const juegoExistente = carrito.find((item) => item.id == id);

    if (juegoExistente) {
      juegoExistente.cantidad += cantidad;
    } else {
      carrito.push({ id, cantidad });
    }

    this.actualizarCarrito(carrito);
  }

  // Actualizar el carrito completo en el localStorage
  actualizarCarrito(carrito: { id: number; cantidad: number }[]): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      const carritoNormalizado = carrito.map((item) => ({
        id: +item.id,
        cantidad: item.cantidad,
      }));
      localStorage.setItem(`carrito_${userId}`, JSON.stringify(carritoNormalizado));
      this.carritoSubject.next(carritoNormalizado);
    } else {
      this.carritoSubject.next([]);
    }
  }

  // Actualizar solo la cantidad de un juego
  actualizarCantidad(id: number, nuevaCantidad: number): void {
    const carrito = this.carritoSubject.getValue();
    const index = carrito.findIndex((item) => item.id === id);

    if (index > -1) {
      carrito[index].cantidad = nuevaCantidad;
      this.actualizarCarrito(carrito);
    }
  }

  // Eliminar un juego del carrito
  eliminarJuego(id: number): void {
    const carrito = this.carritoSubject.getValue();
    const carritoActualizado = carrito.filter((item) => item.id !== id);
    this.actualizarCarrito(carritoActualizado);
  }
}
