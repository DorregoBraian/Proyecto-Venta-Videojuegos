import {Component } from '@angular/core';
import { CardCarritoComponent } from '../card-carrito/card-carrito.component';
import { CommonModule } from '@angular/common';
import { ServiceJuegosService } from '../../Servicios/ServiceJuegos/ServiceJuegos.service';
import { IJuego } from '../../Modelos/Modelo-Juego';
import { FormsModule } from '@angular/forms';
import { forkJoin, map } from 'rxjs';
import { ServisCarritoService } from '../../Servicios/ServiceCarrito/servis-carrito.service';

@Component({
  selector: 'app-sesion-carrito',
  standalone: true,
  imports: [CardCarritoComponent, CommonModule, FormsModule],
  templateUrl: './sesion-carrito.component.html',
  styleUrl: './sesion-carrito.component.css',
})
export class SesionCarritoComponent {
  carrito: { juego: IJuego; cantidad: number }[] = []; // Lista de juegos con sus cantidades
  totalGeneral: number = 0; // Total general del carrito
  selectedCurrency: string = 'USD'; // Valor predeterminado
  currencyList = [
    { code: 'USD', symbol: '$' }, // Dólar estadounidense
    { code: 'EUR', symbol: '€' }, // Euro
    { code: 'GBP', symbol: '£' }, // Libra esterlina
    { code: 'JPY', symbol: '¥' }, // Yen japonés
    { code: 'CAD', symbol: 'CA$' }, // Dólar canadiense
    { code: 'AUD', symbol: 'A$' }, // Dólar australiano
    { code: 'CNY', symbol: '¥' }, // Yuan chino
    { code: 'BRL', symbol: 'R$' }, // Real brasileño
    { code: 'ARS', symbol: '$' }, // Peso argentino
    { code: 'INR', symbol: '₹' } // Rupia india
  ];

  constructor(
    private juegosService: ServiceJuegosService,
    private carritoService: ServisCarritoService) {}

  ngOnInit(): void {
    // Obtener el carrito inicial desde sessionStorage
    this.carritoService.obtenerCarrito().subscribe((carritoData) => {
      console.log('Carrito:', carritoData);
      this.cargarCarrito(carritoData);
    });
  }

//Carga los datos del carrito desde localStorage
  cargarCarrito(carritoData: { id: number; cantidad: number }[]): void {
    if (!carritoData || carritoData.length === 0) {
      this.carrito = []; // Si no hay datos, inicializamos el carrito como vacío
      this.calcularTotalGeneral(); // Actualizamos el total general a 0
      return;
    }
    const requests = carritoData.map((item) =>
      this.juegosService.getJuegoPorId(item.id).pipe(
        // Agregamos la cantidad al resultado del observable
        map((juego) => ({ juego, cantidad: item.cantidad }))
      )
    );

    // Ejecutamos todas las solicitudes en paralelo y esperamos a que terminen
    forkJoin(requests).subscribe((resultados) => {
      this.carrito = resultados;
      this.calcularTotalGeneral();
    });
  }

  // Método para actualizar el total de un producto en específico
  actualizarTotal(precioTotal: number, id: number): void {
    const producto = this.carrito.find((item) => item.juego.id === id);
    if (producto) {
      producto.cantidad = precioTotal / producto.juego.precio;
    }

    // Recalculamos el total general
    this.calcularTotalGeneral();
  }

  // Método para calcular el total general del carrito
  calcularTotalGeneral(): void {
    this.totalGeneral = this.carrito.reduce(
      (total, item) => total + item.juego.precio * item.cantidad,
      0
    );
  }

  trackById(index: number, item: { juego: IJuego; cantidad: number }): number {
    return item.juego.id;
  }
}
