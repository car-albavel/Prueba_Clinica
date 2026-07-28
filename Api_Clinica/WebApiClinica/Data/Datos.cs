using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApiClinica.Data
{
    public class Datos
    {
        private readonly string _CadenaSQL = "";
        public static ConexionBase ClsSQL = new ConexionBase();

        public Datos(IConfiguration configuration)
        {
            _CadenaSQL = configuration.GetConnectionString("StrClinica");
        }

        public DataSet TraerPacientes()
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataSet ds = new DataSet();

            sqlCmd.CommandText = "sp_GetAllPacientes";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            ds = ClsSQL.CreateDataSetBench(sqlCmd, _CadenaSQL);
            return ds;
        }

        public DataSet TraerPacientePorID(int pacienteID)
        {
            SqlCommand sqlCmd = new SqlCommand();
            DataSet ds = new DataSet();

            sqlCmd.CommandText = "sp_GetPacienteByID";
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.AddWithValue("@PacienteID", pacienteID);
            ds = ClsSQL.CreateDataSetBench(sqlCmd, _CadenaSQL);
            return ds;
        }

        public void InsertarPaciente(Paciente paciente)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "sp_InsertPaciente";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@TipoDocumento", paciente.TipoDocumento);
            sqlCmd.Parameters.AddWithValue("@NumeroDocumento", paciente.NumeroDocumento);
            sqlCmd.Parameters.AddWithValue("@Nombre", paciente.NombrePaciente);
            sqlCmd.Parameters.AddWithValue("@FechaNacimiento", paciente.FechaNacimiento);
            sqlCmd.Parameters.AddWithValue("@CorreoElectronico", paciente.CorreoElectronico);
            sqlCmd.Parameters.AddWithValue("@Genero", paciente.Genero);
            sqlCmd.Parameters.AddWithValue("@Direccion", paciente.Direccion);
            sqlCmd.Parameters.AddWithValue("@NumeroTelefono", paciente.NumeroTelefono);
            sqlCmd.Parameters.AddWithValue("@Activo", paciente.Activo);

            ClsSQL.ExecuteNonQuery(sqlCmd, _CadenaSQL);
        }

        public void ActualizarPaciente(Paciente paciente)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "sp_UpdatePaciente";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@PacienteID", paciente.IdPaciente);
            sqlCmd.Parameters.AddWithValue("@TipoDocumento", paciente.TipoDocumento);
            sqlCmd.Parameters.AddWithValue("@NumeroDocumento", paciente.NumeroDocumento);
            sqlCmd.Parameters.AddWithValue("@Nombre", paciente.NombrePaciente);
            sqlCmd.Parameters.AddWithValue("@FechaNacimiento", paciente.FechaNacimiento);
            sqlCmd.Parameters.AddWithValue("@CorreoElectronico", paciente.CorreoElectronico);
            sqlCmd.Parameters.AddWithValue("@Genero", paciente.Genero);
            sqlCmd.Parameters.AddWithValue("@Direccion", paciente.Direccion);
            sqlCmd.Parameters.AddWithValue("@NumeroTelefono", paciente.NumeroTelefono);
            sqlCmd.Parameters.AddWithValue("@Activo", paciente.Activo);

            ClsSQL.ExecuteNonQuery(sqlCmd, _CadenaSQL);
        }

        public void EliminarPaciente(int pacienteID)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandText = "sp_DeletePaciente";
            sqlCmd.CommandType = CommandType.StoredProcedure;

            sqlCmd.Parameters.AddWithValue("@PacienteID", pacienteID);

            ClsSQL.ExecuteNonQuery(sqlCmd, _CadenaSQL);
        }




    }
}
