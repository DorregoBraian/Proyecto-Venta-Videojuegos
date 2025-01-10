import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ServisLoginService {
  private apiUsuario = 'https://localhost:7134/api/Usuario'; // URL para obtener usuario
  private isLoggedInSubject = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) {}
  
  // Método para obtener los datos del usuario
  getDataUsuario(id: number): Observable<any> {
    return this.http.get(`${this.apiUsuario}/${id}`);
  }

  // Método para Iniciar Sesion
  login(email: string, password: string): Observable<{ message: string; userId: number }> {
    const payload = { email, password };
    return this.http.post<{ message: string; userId: number }>(`${this.apiUsuario}/login`, payload);
  }
  
  // Método para reuperar Contraseña
  recuperarContrasena(email: string, nuevaContrasena: string): Observable<any> {
    const payload = { email, nuevaContrasena };
    return this.http.post(`${this.apiUsuario}/recuperar-contrasena`, payload);
  }

  enviarEmail(destinatario: string, asunto: string, mensaje: string) {
    const payload = { destinatario, asunto, mensaje };
    return this.http.post(`${this.apiUsuario}/enviar-correo`, payload);
  }

  // Método único para actualizar y obtener el estado
  statusLogin(status?: boolean): boolean {
    if (status !== undefined) {
      this.isLoggedInSubject.next(status); // Actualiza el estado
    }
    return this.isLoggedInSubject.getValue(); // Retorna el estado actual
  }

  // Método adicional (opcional) para observar cambios en el estado
  checkStatusLogged() {
    return this.isLoggedInSubject.asObservable();
  }
}
