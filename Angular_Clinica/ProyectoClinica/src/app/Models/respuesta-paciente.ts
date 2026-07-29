export interface RespuestaPaciente 
{
    id: number;
    tipoDocumento: string;
    numeroDocumento: string;
    nombrePaciente: string;
    fechaNacimiento: Date;
    correoElectronico: string;
    genero: string;
    direccion: string;
    numeroTelefono: string;
    activo: boolean;

}
