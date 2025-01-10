import { Component, OnInit } from '@angular/core';
import { CardGenerosComponent } from '../card-generos/card-generos.component';
import { CommonModule } from '@angular/common';
import { AsideComponent } from '../../Component-Principales/aside/aside.component';
import { ServisPublicidadApiService } from '../../Servicios/ServicePublicidadApi/servis-publicidad-api.service';

@Component({
  selector: 'app-sesion-home',
  standalone: true,
  imports: [CardGenerosComponent, CommonModule, AsideComponent],
  templateUrl: './sesion-home.component.html',
  styleUrl: './sesion-home.component.css'
})
export class SesionHomeComponent implements OnInit {
  leftAsideList: any[] = [];
  rightAsideList: any[] = [];

  currentOffset = 0;
  cardWidth = 295; // Ajusta según el ancho de tu tarjeta más el margen
  
  generos = [
    { nombre: 'Terror', imagen: '/Generos/genero-terror.jpg', color: '#D90404' }, //rojo
    { nombre: 'Aventura', imagen: '/Generos/genero-aventura.webp', color: '#344290' }, // azul
    { nombre: 'Estrategia', imagen: '/Generos/genero-estrategia.webp', color: '#D9763D' }, //naranja
    { nombre: 'Carreras', imagen: '/Generos/genero-carreras.jpg', color: '#8DA633' }, //verde
    { nombre: 'Shooter', imagen: '/Generos/genero-shooters.jpeg', color: '#444DF2' }, // violeta
    { nombre: 'RPG', imagen: '/Generos/genero-RPG.webp', color: '#D9B64E' }, // amarillo
    { nombre: 'Accion', imagen: '/Generos/genero-accion.webp', color: '#8D9FA6' } // gris
  ];

  constructor(private publicidadService: ServisPublicidadApiService) {}

  ngOnInit() {
    this.loadPublicidad(8); // Carga 8 ofertas
    setInterval(() => {
      this.moveCarousel();
    }, 3000); // Intervalo de 3 segundos
  }

  moveCarousel() {
    this.currentOffset -= this.cardWidth;
    if (Math.abs(this.currentOffset) >= this.cardWidth * this.generos.length) {
      // Resetea al inicio para mantener 4 tarjetas visibles siempre
      this.currentOffset = 0;
    }
  }

  loadPublicidad(count: number): void {
    this.publicidadService.getPublicidad(count).subscribe({
      next: (deals) => {
        this.leftAsideList = deals.slice(0, 4); // Primeras 4 ofertas
        this.rightAsideList = deals.slice(4);   // Siguientes 4 ofertas
      },
      error: (err) => {
        console.error('Error al cargar las ofertas:', err);
      }
    });
  }
  
}
