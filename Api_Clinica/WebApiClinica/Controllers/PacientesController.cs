using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApiClinica.Data;

namespace WebApiClinica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacientesController : ControllerBase
    {
        private readonly Datos _datos;

        public PacientesController(IConfiguration configuration)
        {
            _datos = new Datos(configuration);
        }

        // GET: api/pacientes
        [HttpGet]
        public IActionResult GetAllPacientes()
        {
            try
            {
                DataSet ds = _datos.TraerPacientes();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    var pacientes = new List<Paciente>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        pacientes.Add(new Paciente
                        {
                            IdPaciente = Convert.ToInt32(row["PacienteID"]),
                            TipoDocumento = row["TipoDocumento"].ToString() ?? string.Empty,
                            NumeroDocumento = row["NumeroDocumento"].ToString() ?? string.Empty,
                            NombrePaciente = row["Nombre"].ToString() ?? string.Empty,
                            FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                            CorreoElectronico = row["CorreoElectronico"].ToString() ?? string.Empty,
                            Genero = row["Genero"].ToString() ?? string.Empty,
                            Direccion = row["Direccion"].ToString() ?? string.Empty,
                            NumeroTelefono = row["NumeroTelefono"].ToString() ?? string.Empty,
                            Activo = Convert.ToBoolean(row["Activo"])
                        });
                    }

                    return Ok(pacientes);
                }

                return Ok(new List<Paciente>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener los pacientes", error = ex.Message });
            }
        }

        // GET: api/pacientes/{id}
        [HttpGet("{id}")]
        public IActionResult GetPacienteById(int id)
        {
            try
            {
                DataSet ds = _datos.TraerPacientePorID(id);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    var paciente = new Paciente
                    {
                        IdPaciente = Convert.ToInt32(row["PacienteID"]),
                        TipoDocumento = row["TipoDocumento"].ToString() ?? string.Empty,
                        NumeroDocumento = row["NumeroDocumento"].ToString() ?? string.Empty,
                        NombrePaciente = row["Nombre"].ToString() ?? string.Empty,
                        FechaNacimiento = Convert.ToDateTime(row["FechaNacimiento"]),
                        CorreoElectronico = row["CorreoElectronico"].ToString() ?? string.Empty,
                        Genero = row["Genero"].ToString() ?? string.Empty,
                        Direccion = row["Direccion"].ToString() ?? string.Empty,
                        NumeroTelefono = row["NumeroTelefono"].ToString() ?? string.Empty,
                        Activo = Convert.ToBoolean(row["Activo"])
                    };

                    return Ok(paciente);
                }

                return NotFound(new { mensaje = $"Paciente con ID {id} no encontrado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener el paciente", error = ex.Message });
            }
        }

        // POST: api/pacientes
        [HttpPost]
        public IActionResult CreatePaciente([FromBody] Paciente paciente)
        {
            try
            {
                if (paciente == null)
                {
                    return BadRequest(new { mensaje = "Los datos del paciente son requeridos" });
                }

                if (string.IsNullOrWhiteSpace(paciente.NumeroDocumento) || 
                    string.IsNullOrWhiteSpace(paciente.NombrePaciente))
                {
                    return BadRequest(new { mensaje = "El número de documento y el nombre son requeridos" });
                }

                _datos.InsertarPaciente(paciente);

                return CreatedAtAction(
                    nameof(GetPacienteById), 
                    new { id = paciente.IdPaciente }, 
                    new { mensaje = "Paciente creado exitosamente", paciente }
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al crear el paciente", error = ex.Message });
            }
        }

        // PUT: api/pacientes/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePaciente(int id, [FromBody] Paciente paciente)
        {
            try
            {
                if (paciente == null)
                {
                    return BadRequest(new { mensaje = "Los datos del paciente son requeridos" });
                }

                if (id != paciente.IdPaciente)
                {
                    return BadRequest(new { mensaje = "El ID del paciente no coincide" });
                }

                if (string.IsNullOrWhiteSpace(paciente.NumeroDocumento) || 
                    string.IsNullOrWhiteSpace(paciente.NombrePaciente))
                {
                    return BadRequest(new { mensaje = "El número de documento y el nombre son requeridos" });
                }

                _datos.ActualizarPaciente(paciente);

                return Ok(new { mensaje = "Paciente actualizado exitosamente", paciente });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar el paciente", error = ex.Message });
            }
        }

        // DELETE: api/pacientes/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePaciente(int id)
        {
            try
            {
                _datos.EliminarPaciente(id);

                return Ok(new { mensaje = $"Paciente con ID {id} eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al eliminar el paciente", error = ex.Message });
            }
        }
    }
}
