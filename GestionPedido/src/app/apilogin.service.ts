import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiloginService {
  private apiUrl = 'http://localhost:5032/api/Login';

  constructor(private http: HttpClient) {}

  // Método para iniciar sesión
  login(credentials: { usuarioName: string; password: string }): Observable<any> {
    return this.http.post<any>(this.apiUrl, credentials);
  }
}
