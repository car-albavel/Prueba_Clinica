import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

import { RespuestaPaciente } from '../Models/respuesta-paciente';
import { PeticionPaciente } from '../Models/peticion-paciente';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json'
  })
};

@Injectable({
  providedIn: 'root'
})
export class Services {

  url: string = 'http://localhost:5146/api/pacientes';

  constructor(private _http: HttpClient) { }

  getPacientes(): Observable<RespuestaPaciente[]> {
    return this._http.get<RespuestaPaciente[]>(this.url);
  }

  getPacientePorId(id: number): Observable<RespuestaPaciente> {
    return this._http.get<RespuestaPaciente>(`${this.url}/${id}`);
  }

  addPaciente(paciente: PeticionPaciente): Observable<any> {
    return this._http.post<any>(this.url, paciente, httpOptions);
  }

  editPaciente(id: number, paciente: PeticionPaciente): Observable<any> {
    return this._http.put<any>(`${this.url}/${id}`, paciente, httpOptions);
  }

  deletePaciente(id: number): Observable<any> {
    return this._http.delete<any>(`${this.url}/${id}`);
  }
}
