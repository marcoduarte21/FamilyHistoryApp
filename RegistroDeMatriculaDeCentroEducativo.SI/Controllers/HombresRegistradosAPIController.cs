using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistroDeMatriculaDeCentroEducativo.BL.interfaces;
using RegistroDeMatriculaDeCentroEducativo.Model.Entities;

namespace RegistroDeMatriculaDeCentroEducativo.SI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HombresRegistradosAPIController : ControllerBase
    {
        DA.DBContexto DBContexto;
        GestorDeLaMatricula GestorDeLaMatricula;

        public HombresRegistradosAPIController(DA.DBContexto connection)
        {
            DBContexto = connection;
            GestorDeLaMatricula = new BL.GestorDeLaMatricula(connection);
        }


        [HttpGet("GetDetallesHombresRegistrados")]
        // GET: VentasAPI
        public List<Estudiante> GetDetallesHombresRegistrados()
        {
            return GestorDeLaMatricula.ListeLosHombresRegistrados();
        }


    }
}
