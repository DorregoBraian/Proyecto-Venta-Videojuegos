import { Component, OnInit } from '@angular/core';
import { CardJuegoComponent } from '../card-juego/card-juego.component';
import { CommonModule } from '@angular/common';
import { IJuego } from '../../Modelos/Modelo-Juego';
import { ServiceJuegosService } from '../../Servicios/ServiceJuegos/ServiceJuegos.service';
import { IPlataforma } from '../../Modelos/Modelo-Plataforma';
import { IGenero } from '../../Modelos/Modelo-Genero';
import { IIdioma } from '../../Modelos/Modelo-Idioma';
import { IClasificacion } from '../../Modelos/Modelo-Clasificacion';
import { FormsModule } from '@angular/forms';
import { RespuestaPaginada } from '../../Modelos/RespuestaPaginada';

@Component({
  selector: 'app-sesion-list-juegos',
  standalone: true,
  imports: [CardJuegoComponent, CommonModule, FormsModule],
  templateUrl: './sesion-list-juegos.component.html',
  styleUrl: './sesion-list-juegos.component.css',
})
export class SesionListJuegosComponent implements OnInit {
  generos: { nombre: string; imagen: string }[] = [];
  selectedGenero = { nombre: '', imagen: '/Genero/Shooter.png' };
  dropdownGeneroOpen = false;

  idiomas: { nombre: string; imagen: string }[] = [];
  selectedIdioma = { nombre: '', imagen: '/Idioma/Español.png' };
  dropdownIdiomaOpen = false;

  clasificaciones: { nombre: string; imagen: string }[] = [];
  selectedClasificacion = { nombre: '', imagen: '/Pegi/pegi.png' };
  dropdownClasificacionOpen = false;

  plataformas: { nombre: string; imagen: string }[] = [];
  selectedPlataforma = { nombre: '', imagen: '/Plataforma/PC.png' };
  dropdownPlataformaOpen = false;

  juegosPaginados: IJuego[] = []; // Juegos de la página actual
  numerosDePagina: number[] = []; // Lista de numero segun la Paginacion
  paginaActual: number = 1; // Página inicial
  juegosPorPagina: number = 20; // 4 columnas x 5 filas
  totalPaginas: number = 0;
  modoBusqueda: boolean = false;
  nombreBusqueda: string = '';

  constructor(private servicioJuegos: ServiceJuegosService) {}

  ngOnInit(): void {
    this.obtenerJuegos();
    this.obtenerGeneros();
    this.obtenerPlataformas();
    this.obtenerIdiomas();
    this.obtenerClasificaciones();
  }

  obtenerJuegos(): void {
    // Obtenemos los juegos paginados desde el servicio
    this.servicioJuegos
      .getJuegosPaginados(this.paginaActual, this.juegosPorPagina)
      .subscribe({
        next: (data: RespuestaPaginada) => {
          console.log('Respuesta del servicio:', data);          
          this.juegosPaginados = data.juegos; // Los juegos obtenidos
          this.totalPaginas = Math.ceil(data.totalJuegos / this.juegosPorPagina); // Calculamos el total de páginas
          this.generarNumerosDePagina(); // Generamos los números de página
        },
        error: (err: any) => {
          console.error('Error al obtener los juegos:', err);
        },
      });
  }

  // Genera los números de página
  generarNumerosDePagina(): void {
    this.numerosDePagina = Array.from(
      { length: this.totalPaginas },
      (_, i) => i + 1
    );
  }

  // Cambia la página actual y actualiza los juegos visibles
  irAPagina(numero: number): void {
    this.paginaActual = numero;
    if (this.filtrosSeleccionados()) {
      this.filtrarJuegos(); // Cargar los juegos filtrados para la página anterior
    } else {
      this.obtenerJuegos(); // Cargar los juegos sin filtros para la página anterior
    }
  }

  // Pasa a la página anterior
  paginaAnterior(): void {
    console.log(this.paginaActual)
    if (this.paginaActual > 1) {
      this.paginaActual--;
      if (this.filtrosSeleccionados()) {
        this.filtrarJuegos(); // Cargar los juegos filtrados para la página anterior
      } else {
        this.obtenerJuegos(); // Cargar los juegos sin filtros para la página anterior
      }
    }
  }

  // Pasa a la página siguiente
  paginaSiguiente(): void {
    console.log(this.paginaActual)
    if (this.paginaActual < this.totalPaginas) {
      this.paginaActual++;
      if (this.filtrosSeleccionados()) {
        this.filtrarJuegos(); // Cargar los juegos filtrados para la página anterior
      } else {
        this.obtenerJuegos(); // Cargar los juegos sin filtros para la página anterior
      }
    }
  }

  // Busca un juego por nombre usando el servicio
  buscarJuegosPorNombre(): void {
    const nombreInput = this.normalizeTexto(this.nombreBusqueda.trim());

    if (nombreInput === '') {
      // Si el input está vacío, muestra todos los juegos
      this.limpiarBusqueda();
      return;
    }
    this.servicioJuegos.getJuegoPorNombre(nombreInput).subscribe(
      (resultados) => {
        // Filtramos los resultados que coincidan con el nombre buscado
        const juegosFiltrados = resultados.filter((juego) =>
          this.normalizeTexto(juego.titulo).includes(nombreInput)
        );

        // Actualizamos los datos para la paginación
        this.totalPaginas = Math.ceil(
          juegosFiltrados.length / this.juegosPorPagina
        );
        this.juegosPaginados = juegosFiltrados.slice(0, this.juegosPorPagina); // Mostramos la primera página
        this.generarNumerosDePagina();
        this.modoBusqueda = true; // Activa el modo de búsqueda
        this.paginaActual = 1; // Restablecemos a la primera página
      },
      (error) => {
        console.error('Error al buscar:', error);
      }
    );
  }

  // Limpia la búsqueda y restaura la lista completa paginada
  limpiarBusqueda(): void {
    this.modoBusqueda = false;
    this.nombreBusqueda = '';
    this.paginaActual = 1; // Restablecemos a la primera página
    this.obtenerJuegos(); // Volvemos a cargar los datos desde el backend
  }

  // Normaliza el texto
  normalizeTexto(texto: string): string {
    if (!texto) return '';

    // Eliminar tildes y acentos
    let textoNormalizado = texto
      .normalize('NFD')
      .replace(/[\u0300-\u036f]/g, '');

    // Reemplazar las comillas inclinadas por comillas normales
    textoNormalizado = textoNormalizado
      .replace(/[‘’‚]/g, "'")
      .replace(/[“”«»„]/g, '"');

    // Eliminar otros caracteres especiales comunes (puedes agregar más si es necesario)
    textoNormalizado = textoNormalizado.replace(
      /[^a-zA-Z0-9áéíóúÁÉÍÓÚàèìòùãõâêîôûäëïöü]/g,
      ''
    );

    // Convertir todo a minúsculas
    return textoNormalizado.toLowerCase();
  }

  // Obtener todas las Generos para los Filtros
  obtenerGeneros(): void {
    this.servicioJuegos.getGeneros().subscribe({
      next: (data: IGenero[]) => {
        this.generos = data.map((item) => ({
          nombre: item.nombre,
          imagen: `/Genero/${item.nombre}.png`,
        }));
      },
      error: (err: any) => {
        console.error('Error al obtener los generos:', err);
      },
    });
  }

  // Obtener todas las Idiomas para los Filtros
  obtenerIdiomas(): void {
    this.servicioJuegos.getIdiomas().subscribe({
      next: (data: IIdioma[]) => {
        this.idiomas = data.map((item) => ({
          nombre: item.nombre,
          imagen: `/Idioma/${item.nombre}.png`,
        }));
      },
      error: (err: any) => {
        console.error('Error al obtener los idiomas:', err);
      },
    });
  }

  // Obtener todas las Plataformas para los Filtros
  obtenerPlataformas(): void {
    this.servicioJuegos.getPlataformas().subscribe({
      next: (data: IPlataforma[]) => {
        this.plataformas = data.map((item) => ({
          nombre: item.nombre,
          imagen: `/Plataforma/${item.nombre}.png`,
        }));
      },
      error: (err: any) => {
        console.error('Error al obtener los paltaformas:', err);
      },
    });
  }

  // Obtener todas las Clasificacion para los Filtros
  obtenerClasificaciones(): void {
    this.servicioJuegos.getClasificacion().subscribe({
      next: (data: IClasificacion[]) => {
        this.clasificaciones = data.map((item) => ({
          nombre: item.nombre,
          imagen: `/Pegi/pegi-${item.nombre.split(' ')[1]}.png`,
        }));
      },
      error: (err: any) => {
        console.error('Error al obtener las clasificaciones:', err);
      },
    });
  }

  filtrarJuegos(): void {
    // Llamar al servicio para filtrar los juegos usando los filtros seleccionados
    this.servicioJuegos
      .filtrarJuegos(
        this.selectedIdioma.nombre,
        this.selectedPlataforma.nombre,
        this.selectedClasificacion.nombre,
        this.selectedGenero.nombre,
        this.paginaActual,
        this.juegosPorPagina
      )
      .subscribe(
        (juegosFiltrados) => {
          console.log('Respuesta del servicio:', juegosFiltrados);
          this.juegosPaginados = juegosFiltrados.juegos; // Los juegos obtenidos
          this.totalPaginas = Math.ceil(juegosFiltrados.totalJuegos / this.juegosPorPagina); // Calculamos el total de páginas
          this.generarNumerosDePagina(); // Generamos los números de página
        },
        (error) => {
          console.error('Error al obtener los juegos filtrados', error);
        }
      );
  }

  // Método verificar si los filtros fueron seleccionados
  filtrosSeleccionados(): boolean {
    return (
      this.selectedIdioma.nombre.trim() !== '' ||
      this.selectedPlataforma.nombre.trim() !== '' ||
      this.selectedClasificacion.nombre.trim() !== '' ||
      this.selectedGenero.nombre.trim() !== ''
    );
  }

  // Método para alternar la visibilidad de los dropdowns
  toggleDropdown(
    type: 'clasificacion' | 'plataforma' | 'idioma' | 'genero'
  ): void {
    switch (type) {
      case 'clasificacion':
        this.dropdownClasificacionOpen = !this.dropdownClasificacionOpen;
        break;

      case 'plataforma':
        this.dropdownPlataformaOpen = !this.dropdownPlataformaOpen;
        break;

      case 'idioma':
        this.dropdownIdiomaOpen = !this.dropdownIdiomaOpen;
        break;

      case 'genero':
        this.dropdownGeneroOpen = !this.dropdownGeneroOpen;
        break;

      default:
        console.error('Tipo de dropdown no soportado:', type);
        break;
    }
  }

  // Método para seleccionar una opción en los dropdowns
  selectOption(
    type: 'clasificacion' | 'plataforma' | 'idioma' | 'genero',
    option: any
  ): void {
    switch (type) {
      case 'clasificacion':
        this.selectedClasificacion = option;
        this.dropdownClasificacionOpen = false;
        break;

      case 'plataforma':
        this.selectedPlataforma = option;
        this.dropdownPlataformaOpen = false;
        break;

      case 'idioma':
        this.selectedIdioma = option;
        this.dropdownIdiomaOpen = false;
        break;

      case 'genero':
        this.selectedGenero = option;
        this.dropdownGeneroOpen = false;
        break;

      default:
        console.error('Tipo de selección no soportado:', type);
        break;
    }
    this.filtrarJuegos();
  }
}
