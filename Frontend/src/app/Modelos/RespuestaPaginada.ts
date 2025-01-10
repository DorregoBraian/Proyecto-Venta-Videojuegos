import { IJuego } from './Modelo-Juego';

export interface RespuestaPaginada {
  juegos: IJuego[];
  nextCursor: number;
  totalJuegos: number;
}
