export interface PeticionPaciente
{
    idPaciente?: number;
    tipoDocumento: string;
    numeroDocumento: string;
    nombrePaciente: string;
    fechaNacimiento: string;
    correoElectronico: string;
    genero: string;
    direccion: string;
    numeroTelefono: string;
    activo: boolean;
}
