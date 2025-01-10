import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ServisFavoritosService {
  private favoritosSubject = new BehaviorSubject<{ id: number }[]>([]);

  constructor() {
    this.cargarFavoritosDesdeLocalStorage();
  }

  // Carga la lista de favoritos asociada al userId actual desde el localStorage
  private cargarFavoritosDesdeLocalStorage(): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      const favoritos = JSON.parse(
        localStorage.getItem(`favoritos_${userId}`) || '[]'
      );
      this.favoritosSubject.next(favoritos);
    }else {
      // Si no hay un usuario logueado, se asegura que la lista esté vacía
      this.favoritosSubject.next([]);
    }
  }

  // Permite observar cambios en la lista de favoritos
  obtenerFavoritos(): Observable<{ id: number }[]> {
    return this.favoritosSubject.asObservable();
  }

  // Agregar un juego a favoritos
  agregarJuegoAFavoritos(id: number): void {
    const favoritos = this.favoritosSubject.getValue();

    // Verificar si el juego ya está en favoritos
    const juegoExistente = favoritos.find((item) => item.id === id);

    if (!juegoExistente) {
      // Agregar el juego si no existe
      favoritos.push({ id });
      this.actualizarFavoritos(favoritos);
    }
  }

  // Actualizar la lista de favoritos en el localStorage
  actualizarFavoritos(favoritos: { id: number }[]): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      localStorage.setItem(`favoritos_${userId}`, JSON.stringify(favoritos));
      this.favoritosSubject.next(favoritos);
    } else {
      this.favoritosSubject.next([]);
    }
  }
  
  // Eliminar un juego de favoritos
  eliminarFavoritos(id: number): void {
    const favoritos = this.favoritosSubject.getValue();

    // Filtrar la lista para eliminar el juego con el ID proporcionado
    const favoritosActualizados = favoritos.filter(
      (item) => item.id !== id
    );

    this.actualizarFavoritos(favoritosActualizados);
  }

  // Verificar si un juego ya está en favoritos
  juegoEsFavorito(id: number): boolean {
    const favoritos = this.favoritosSubject.getValue();
    return favoritos.some((item) => item.id === id);
  }

  // Alternar el estado de un juego en favoritos (agregar o eliminar)
  toggleFavorito(id: number): void {
    if (this.juegoEsFavorito(id)) {
      this.eliminarFavoritos(id);
    } else {
      this.agregarJuegoAFavoritos(id);
    }
  }
}