using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistroDeMatriculaDeCentroEducativo.Model;

namespace RegistroDeMatriculaDeCentroEducativo.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialFamiliarAPIController : ControllerBase
    {
        DA.DBContexto DBContexto;
        BL.GestorDeLaMatricula GestorDeLaMatricula;
        public HistorialFamiliarAPIController(DA.DBContexto connection)
        {
            DBContexto = connection;
            GestorDeLaMatricula = new BL.GestorDeLaMatricula(connection);
        }

        [HttpGet("GetPadres")]
        public IActionResult GetPadres(string cedula)
        {
            try
            {
                Estudiante estudiante = GestorDeLaMatricula.RetorneElEstudiantePorIdentificacion(cedula);
                if (estudiante == null)
                {
                    return BadRequest("Student with cedula not found.");
                }
                else
                {
                    return Ok(GestorDeLaMatricula.ListeLosPadres(cedula));
                }
            }
            catch (Exception ex)
            {
                    return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetHijos")]
        public List<Model.Estudiante> GetHijos(string cedula)
        {
            return GestorDeLaMatricula.ListeLosHijos(cedula);
        }

        [HttpGet("GetHermanos")]
        public List<Model.Estudiante> GetHermanos(string cedula)
        {
            return GestorDeLaMatricula.ListeLosHermanos(cedula);
        }

        [HttpGet("GetAbuelos")]
        public List<Model.Estudiante> GetAbuelos(string cedula)
        {
            return GestorDeLaMatricula.ListeLosAbuelos(cedula);
        }

        [HttpGet("GetTios")]
        public List<Model.Estudiante> GetTios(string cedula)
        {
            return GestorDeLaMatricula.ListeLosTios(cedula);
        }

        [HttpGet("GetPrimos")]
        public List<Model.Estudiante> GetPrimos(string cedula)
        {
            return GestorDeLaMatricula.ListeLosPrimos(cedula);
        }

    }
}
