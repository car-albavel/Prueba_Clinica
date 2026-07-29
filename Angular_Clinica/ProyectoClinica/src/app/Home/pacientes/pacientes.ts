import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

import { Services } from '../../Services/services';
import { RespuestaPaciente } from '../../Models/respuesta-paciente';
import { PeticionPaciente } from '../../Models/peticion-paciente';

@Component({
  selector: 'app-pacientes',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './pacientes.html',
  styleUrl: './pacientes.css',
})
export class Pacientes implements OnInit {

  pacientes = signal<RespuestaPaciente[]>([]);
  form: FormGroup;
  editando = signal<boolean>(false);
  idEditando: number | null = null;
  mensaje = signal<string>('');
  error = signal<string>('');
  cargando = signal<boolean>(false);

  constructor(private _service: Services, private _fb: FormBuilder) {
    this.form = this._fb.group({
      tipoDocumento: ['', Validators.required],
      numeroDocumento: ['', Validators.required],
      nombrePaciente: ['', Validators.required],
      fechaNacimiento: ['', Validators.required],
      correoElectronico: ['', [Validators.email]],
      genero: ['', Validators.required],
      direccion: [''],
      numeroTelefono: [''],
      activo: [true]
    });
  }

  ngOnInit(): void {
    this.cargarPacientes();
  }

  cargarPacientes(): void {
    this.cargando.set(true);
    this._service.getPacientes().subscribe({
      next: (data) => {
        this.pacientes.set(data);
        this.cargando.set(false);
      },
      error: (err) => {
        this.error.set('Error al cargar los pacientes');
        this.cargando.set(false);
        console.error(err);
      }
    });
  }

  guardar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.mensaje.set('');
    this.error.set('');
    const paciente: PeticionPaciente = this.form.value;

    if (this.editando() && this.idEditando !== null) {
      paciente.idPaciente = this.idEditando;
      this._service.editPaciente(this.idEditando, paciente).subscribe({
        next: () => {
          this.mensaje.set('Paciente actualizado exitosamente');
          this.cancelar();
          this.cargarPacientes();
        },
        error: (err) => {
          this.error.set(err.error?.mensaje || 'Error al actualizar el paciente');
          console.error(err);
        }
      });
    } else {
      this._service.addPaciente(paciente).subscribe({
        next: () => {
          this.mensaje.set('Paciente creado exitosamente');
          this.cancelar();
          this.cargarPacientes();
        },
        error: (err) => {
          this.error.set(err.error?.mensaje || 'Error al crear el paciente');
          console.error(err);
        }
      });
    }
  }

  editar(paciente: RespuestaPaciente): void {
    this.editando.set(true);
    this.idEditando = paciente.idPaciente;
    this.mensaje.set('');
    this.error.set('');
    this.form.patchValue({
      tipoDocumento: paciente.tipoDocumento?.trim(),
      numeroDocumento: paciente.numeroDocumento,
      nombrePaciente: paciente.nombrePaciente,
      fechaNacimiento: paciente.fechaNacimiento?.substring(0, 10),
      correoElectronico: paciente.correoElectronico,
      genero: paciente.genero?.trim(),
      direccion: paciente.direccion,
      numeroTelefono: paciente.numeroTelefono,
      activo: paciente.activo
    });
  }

  eliminar(id: number): void {
    if (!confirm('¿Está seguro de eliminar este paciente?')) return;

    this._service.deletePaciente(id).subscribe({
      next: () => {
        this.mensaje.set('Paciente eliminado exitosamente');
        this.cargarPacientes();
      },
      error: (err) => {
        this.error.set('Error al eliminar el paciente');
        console.error(err);
      }
    });
  }

  cancelar(): void {
    this.editando.set(false);
    this.idEditando = null;
    this.form.reset({ activo: true });
  }
}
