export interface IJuego {
  id: number;
  titulo: string;
  precio: number;
  portada: string;
  descripcion: string;
  desarrollador: string;
  editor: string;
  clasificacion: string;
  fechaDeLanzamiento: Date;
  generos: string[];
  plataformas: string[];
  imagenes: string[];
  videos: string[];
  idiomas: string[];
}
