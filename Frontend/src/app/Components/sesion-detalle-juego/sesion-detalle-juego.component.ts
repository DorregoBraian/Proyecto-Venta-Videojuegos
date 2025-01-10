import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-sesion-detalle-juego',
  standalone: true,
  imports: [RouterLink, RouterOutlet],
  templateUrl: './sesion-detalle-juego.component.html',
  styleUrl: './sesion-detalle-juego.component.css'
})
export class SesionDetalleJuegoComponent implements OnInit {
  id!: number;

  constructor(private route: ActivatedRoute) {}

  ngOnInit(): void {
    // Accede al id de la URL
    this.id = this.route.snapshot.params['id'];
  }

}
