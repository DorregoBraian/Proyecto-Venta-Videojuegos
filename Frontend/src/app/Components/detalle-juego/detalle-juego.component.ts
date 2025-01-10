import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { IJuego } from '../../Modelos/Modelo-Juego';
import { ServiceJuegosService } from '../../Servicios/ServiceJuegos/ServiceJuegos.service';
import { CommonModule } from '@angular/common';
import { UrlPipePipe } from '../../Pipe/url-pipe.pipe';
import { ServisCarritoService } from '../../Servicios/ServiceCarrito/servis-carrito.service';
import { ServisFavoritosService } from '../../Servicios/ServiceFavoritos/servis-favoritos.service';
import { MatDialog } from '@angular/material/dialog';
import { AlertPersonalizadosComponent } from '../../Alert/alert-personalizados/alert-personalizados.component';

@Component({
  selector: 'app-detalle-juego',
  standalone: true,
  imports: [CommonModule, RouterLink, UrlPipePipe],
  templateUrl: './detalle-juego.component.html',
  styleUrl: './detalle-juego.component.css',
})
export class DetalleJuegoComponent implements OnInit {
  juego: IJuego | null = null;
  nombreJuego?: string;
  cantidad: number = 0;
  id!: number;
  portada: string = '';
  listCarrusel: string[] = []; // Elementos visibles del carrusel (1 video + 3 imágenes)
  listaCompleta: string[] = []; /* Todos los elementos (videos y imágenes)*/
  indiceInicio: number = 0; // Índice de inicio de la ventana visible
  esFavorito: boolean = false; // Estado inicial
  carritoLleno: boolean = false; // Estado inicial del carrito vacío

  constructor(
    private route: ActivatedRoute,
    private dialog: MatDialog,
    private carritoService: ServisCarritoService,
    private juegosService: ServiceJuegosService,
    private favoritoService: ServisFavoritosService
  ) {}

  ngOnInit(): void {
    this.id = this.route.parent!.snapshot.params['id'];
    this.cargarJuego(this.id);
  }

  esImagen(url: string): boolean {
    return url.endsWith('.png') || url.endsWith('.jpg') || url.endsWith('.gif');
  }

  cargarJuego(id: number): void {
    this.juegosService.getJuegoPorId(id).subscribe({
      next: (juego) => {
        this.juego = juego;
        this.nombreJuego= juego.titulo;
        console.log(this.juego);
        this.portada = juego.portada;
        this.listaCompleta = [
          ...(juego.videos || []), // Agrega los videos si existen
          ...(juego.imagenes || []), // Agrega las imágenes si existen
        ];
        // Muestra los primeros 4 elementos (1 video + 3 imágenes)
        this.actualizarVentana();
      },
      error: (error) => {
        console.error('Error al cargar el juego:', error);
        alert('No se pudo cargar la información del juego.');
      },
    });
  }
  // Actualiza la ventana visible del carrusel
  actualizarVentana(): void {
    const ventana = 4; // Tamaño de la ventana
    this.listCarrusel = this.listaCompleta.slice(
      this.indiceInicio,
      this.indiceInicio + ventana
    );

    // Si el índice supera el tamaño total, ajusta la ventana circularmente
    if (this.listCarrusel.length < ventana) {
      this.listCarrusel = [
        ...this.listCarrusel,
        ...this.listaCompleta.slice(0, ventana - this.listCarrusel.length),
      ];
    }
  }

  siguiente(): void {
    if (this.listaCompleta.length > 0) {
      // Retrocede el índice de inicio circularmente
      this.indiceInicio =
        (this.indiceInicio - 1 + this.listaCompleta.length) %
        this.listaCompleta.length;

      // Mueve la última posición al inicio de la ventana visible
      const nuevoElemento =
        this.listaCompleta[
          (this.indiceInicio + this.listCarrusel.length - 1) %
            this.listaCompleta.length
        ];
      this.listCarrusel.pop(); // Elimina el último elemento
      this.listCarrusel.unshift(nuevoElemento); // Agrega el nuevo elemento al principio
    }
  }

  anterior(): void {
    if (this.listaCompleta.length > 0) {
      // Avanza el índice de inicio circularmente
      this.indiceInicio = (this.indiceInicio + 1) % this.listaCompleta.length;

      // Mueve el primer elemento al final de la ventana visible
      const nuevoElemento =
        this.listaCompleta[
          (this.indiceInicio + this.listCarrusel.length - 1) %
            this.listaCompleta.length
        ];
      this.listCarrusel.shift(); // Elimina el primer elemento
      this.listCarrusel.push(nuevoElemento); // Agrega el nuevo elemento al final
    }
  }

  actualizarPortada(nuevaPortada: string): void {
    if (this.esImagen(nuevaPortada)) {
      this.portada = nuevaPortada;
      console.log('es uan img ' + nuevaPortada);
    } else {
      this.portada = nuevaPortada;
      console.log('es uan video ' + nuevaPortada);
    }
  }

  cambiarCantidad(valor: number): void {
    this.cantidad = Math.max(this.cantidad + valor, 0); // No permitir valores negativos
  }

  agregarAlCarrito(): void {
    // Validar que la cantidad seleccionada sea mayor a cero
    if (this.cantidad <= 0) {
      this.alertPersonalizado(
        'Cantidad Inválida',
        `La cantidad debe ser mayor que cero.`,
        '/Alert/Cero NO.gif'
      );
      return;
    }
    // Agrego el Juego al carrito
    this.carritoService.agregarJuegoAlCarrito(this.id, this.cantidad);
    this.alertPersonalizado(
      'Carrito de Compras',
      `El Juego ${this.nombreJuego} se agregado a tu carrito con la cantidad de ${this.cantidad} unidades.`,
      '/Alert/Al Carrito.gif'
    );
    // Alterna entre carrito lleno y vacío
    this.carritoLleno = !this.carritoLleno;
  }

  agregarAFavoritos(): void {
    if (this.esFavorito) {
      // Si ya es favorito, lo elimina
      this.favoritoService.eliminarFavoritos(this.id);
      this.alertPersonalizado(
        'Tus Favoritos',
        `El juego ${this.nombreJuego} fue eliminado de tus favoritos.`,
        '/Alert/No Favorito.gif'
      );
    } else {
      // Si no es favorito, lo agrega
      this.favoritoService.agregarJuegoAFavoritos(this.id);
      this.alertPersonalizado(
        'Tus Favoritos',
        `El juego ${this.nombreJuego} fue agregado a tus favoritos.`,
        '/Alert/Mi Favorito.jpg'
      );
    }

    // Alternar el estado de favorito
    this.esFavorito = !this.esFavorito;
  }

  separarComa(input: string[] | undefined): string {
    if (!input || input.length === 0) {
      return ''; // Retorna un string vacío si el array es undefined o está vacío
    }
    return input.join(', '); // Une los elementos con una coma y un espacio
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
