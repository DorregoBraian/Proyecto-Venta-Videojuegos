import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { IJuego } from '../../Modelos/Modelo-Juego';
import { IGenero } from '../../Modelos/Modelo-Genero';
import { IIdioma } from '../../Modelos/Modelo-Idioma';
import { IPlataforma } from '../../Modelos/Modelo-Plataforma';
import { IClasificacion } from '../../Modelos/Modelo-Clasificacion';
import { RespuestaPaginada } from '../../Modelos/RespuestaPaginada';
import { RespuestaFiltroPaginada } from '../../Modelos/RespuestaFiltroPaginado';

@Injectable({
  providedIn: 'root',
})
export class ServiceJuegosService {
  private apiUrl = 'https://localhost:7134/api/Juegos'; // URL base de tu API REST
  private apiGenerosUrl = 'https://localhost:7134/api/Genero'; // URL para obtener géneros
  private apiIdiomasUrl = 'https://localhost:7134/api/Idioma'; // URL para obtener idiomas
  private apiPlataformasUrl = 'https://localhost:7134/api/Plataforma'; // URL para obtener plataforma
  private apiClasificacionUrl = 'https://localhost:7134/api/Clasificacion'; // URL para obtener clasificacion

  constructor(private http: HttpClient) {}

  // Obtener todos los juegos
  getJuegos(): Observable<IJuego[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // Método de Paginación en el servicio
  getJuegosPaginados(
    cursor: number | null,
    limit: number
  ): Observable<RespuestaPaginada> {
    return this.http.get<RespuestaPaginada>(
      `${this.apiUrl}/paginacion?cursor=${cursor}&limit=${limit}`
    );
  }

  // Obtener géneros
  getGeneros(): Observable<IGenero[]> {
    return this.http.get<IGenero[]>(this.apiGenerosUrl);
  }

  // Obtener idiomas
  getIdiomas(): Observable<IIdioma[]> {
    return this.http.get<IIdioma[]>(this.apiIdiomasUrl);
  }

  // Obtener todas las Plataformas
  getPlataformas(): Observable<IPlataforma[]> {
    return this.http.get<IPlataforma[]>(this.apiPlataformasUrl);
  }

  // Obtener todas las Clasificacion
  getClasificacion(): Observable<IClasificacion[]> {
    return this.http.get<IClasificacion[]>(this.apiClasificacionUrl);
  }

  // Obtener un juego por ID
  getJuegoPorId(id: number): Observable<IJuego> {
    return this.http.get<IJuego>(`${this.apiUrl}/${id}`);
  }

  // Obtener un juego por Nombre
  getJuegoPorNombre(nombre: string): Observable<IJuego[]> {
    return this.http.get<IJuego[]>(`${this.apiUrl}/nombre/${nombre}`);
  }

  // Método para filtrar juegos con paginación
  filtrarJuegos(
    idioma?: string,
    plataforma?: string,
    clasificacion?: string,
    genero?: string,
    page: number = 1,
    pageSize?: number
  ): Observable<RespuestaFiltroPaginada> {
    const params: any = {
      idioma,
      plataforma,
      genero,
      clasificacion,
      page,
      pageSize,
    };

    return this.http.get<RespuestaFiltroPaginada>(`${this.apiUrl}/filtrar`, { params });
  }
}
