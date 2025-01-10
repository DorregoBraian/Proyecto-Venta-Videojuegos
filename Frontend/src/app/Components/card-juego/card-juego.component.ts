import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-card-juego',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './card-juego.component.html',
  styleUrl: './card-juego.component.css'
})
export class CardJuegoComponent {
  
  @Input() id!: number;
  @Input() nombre!: string;
  @Input() portada!: string;
  @Input() precio!: number;

  constructor(private router: Router) {}

  verDetalles(): void {
    this.router.navigate(['sesion-detalle', this.id]);
  }

}
