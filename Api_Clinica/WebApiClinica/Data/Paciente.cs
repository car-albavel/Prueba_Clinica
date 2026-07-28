namespace WebApiClinica.Data
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty;
        public string NombrePaciente { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string CorreoElectronico { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string NumeroTelefono { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
