import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServisPublicidadApiService {

  private apiUrl = 'https://www.cheapshark.com/api/1.0/deals?storeID=1&upperPrice=20';
  
  constructor(private http: HttpClient) {}

  getPublicidad(count: number): Observable<any[]> {
    return new Observable(observer => {
      this.http.get<any[]>(this.apiUrl).subscribe({
        next: (deals) => {
          const selectedDeals = deals.slice(0, count); // Selecciona la cantidad solicitada
          observer.next(selectedDeals); // Envía las ofertas seleccionadas al suscriptor
          observer.complete();
        },
        error: (err) => {
          observer.error(err); // Envía el error al suscriptor
        }
      });
    });
  }
}
