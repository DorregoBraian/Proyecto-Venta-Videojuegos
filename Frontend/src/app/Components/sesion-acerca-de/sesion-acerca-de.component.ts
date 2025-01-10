import { Component, OnInit } from '@angular/core';
import { ServisPublicidadApiService } from '../../Servicios/ServicePublicidadApi/servis-publicidad-api.service';
import { AsideComponent } from '../../Component-Principales/aside/aside.component';

@Component({
  selector: 'app-sesion-acerca-de',
  standalone: true,
  imports: [AsideComponent],
  templateUrl: './sesion-acerca-de.component.html',
  styleUrl: './sesion-acerca-de.component.css'
})
export class SesionAcercaDeComponent implements OnInit {
  leftAsideList: any[] = [];
  rightAsideList: any[] = [];

  constructor(private publicidadService: ServisPublicidadApiService) {}
  
  ngOnInit(): void {
    this.loadPublicidad(10); // Carga 10 ofertas
  }

  loadPublicidad(count: number): void {
    this.publicidadService.getPublicidad(count).subscribe({
      next: (deals) => {
        this.leftAsideList = deals.slice(0, 5); // Primeras 4 ofertas
        this.rightAsideList = deals.slice(5);   // Siguientes 4 ofertas
      },
      error: (err) => {
        console.error('Error al cargar las ofertas:', err);
      }
    });
  }
}
