using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace RegistroDeMatriculaDeCentroEducativo.SI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        DA.DBContexto DBContexto;
        StudentService StudentService;
        public StudentController(DA.DBContexto connection)
        {
            DBContexto = connection;
            StudentService = new StudentService(connection);
        }

        [HttpGet("GetEstudiantes")]
        public List<Estudiante> GetEstudiantes()
        {
            return GestorDeLaMatricula.ListeLosEstudiantes();
        }

        [HttpGet("GetEstudiante")]
        public IActionResult GetEstudiante(int id)
        {
            try
            {
                Estudiante estudiante = GestorDeLaMatricula.RetorneElEstudiantePorId(id);
                if (estudiante != null)
                {
                    return Ok(estudiante);
                }
                else return BadRequest("Student Not Found :(.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetStudentByCedula")]
        public IActionResult GetEstudianteByCedula(string cedula)
        {
            Estudiante estudiante = new Estudiante();
            estudiante = GestorDeLaMatricula.RetorneElEstudiantePorIdentificacion(cedula);

            if (estudiante != null)
            {
                return Ok(estudiante);
            }
            else
            {
                return NotFound("Student not found :(.");
            }

        }

        [HttpPost("CreateStudent")]
        public IActionResult CreateStudent([FromBody] EstudianteDTO estudiante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Estudiante existingStudent = GestorDeLaMatricula.RetorneElEstudiantePorIdentificacion(estudiante.Cedula);

                    if (existingStudent != null)
                    {

                        throw new CustomException("El estudiante ya existe." , 400);
                    }
                    else

                    GestorDeLaMatricula.Registre(estudiante);
                    Estudiante newStudent = GestorDeLaMatricula.RetorneElEstudiantePorIdentificacion(estudiante.Cedula);
                    return Ok(newStudent);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.ErrorCode, ex.Message);
            }
        }

        [HttpPut("EditStudent")]
        public IActionResult EditStudent([FromBody] EstudianteDTO estudiante)
        {

            if (ModelState.IsValid)
            {
                GestorDeLaMatricula.Edite(estudiante);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

    }


}
