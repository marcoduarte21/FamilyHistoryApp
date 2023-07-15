using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RegistroDeMatriculaDeCentroEducativo.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteAPIController : ControllerBase
    {
        DA.DBContexto DBContexto;
        BL.GestorDeLaMatricula GestorDeLaMatricula;
        public EstudianteAPIController(DA.DBContexto connection)
        {
            DBContexto = connection;
            GestorDeLaMatricula = new BL.GestorDeLaMatricula(connection);
        }

        [HttpGet("GetEstudiantes")]
        public List<Model.Estudiante> GetEstudiantes()
        {
            return GestorDeLaMatricula.ListeLosEstudiantes();
        }

        [HttpGet("GetEstudiante")]
        public Model.Estudiante GetEstudiante(int id)
        {
            return GestorDeLaMatricula.RetorneElEstudiantePorId(id);
        }

        [HttpPost("CreateStudent")]
        public IActionResult CreateStudent([FromBody] Model.EstudianteParaIE estudiante)
        {
            GestorDeLaMatricula.Registre(estudiante);
            return Ok(estudiante);
        }

        [HttpPut("EditStudent")]
        public IActionResult EditStudent([FromBody] Model.EstudianteParaIE estudiante)
        {

            if (ModelState.IsValid)
            {
                GestorDeLaMatricula.Edite(estudiante.Id, estudiante.Cedula, estudiante.Nombre, estudiante.PrimerApellido, estudiante.SegundoApellido, estudiante.Sexo, (DateTime)estudiante.FechaDeNacimiento,
                estudiante.CedulaMadre, estudiante.CedulaPadre);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }


    }


}
