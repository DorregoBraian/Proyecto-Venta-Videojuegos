import { IJuego } from "./Modelo-Juego";

export interface RespuestaFiltroPaginada {
  juegos: IJuego[];
  totalJuegos: number;
}
