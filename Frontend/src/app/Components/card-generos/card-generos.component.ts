import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-card-generos',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './card-generos.component.html',
  styleUrl: './card-generos.component.css'
})
export class CardGenerosComponent {

  @Input() nombre: string = '';
  @Input() imagen: string = '';
  @Input() color: string = '';

}
