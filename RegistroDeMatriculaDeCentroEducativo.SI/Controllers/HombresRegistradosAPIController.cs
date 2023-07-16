using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RegistroDeMatriculaDeCentroEducativo.SI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HombresRegistradosAPIController : ControllerBase
    {
        DA.DBContexto DBContexto;
        BL.GestorDeLaMatricula GestorDeLaMatricula;

        public HombresRegistradosAPIController(DA.DBContexto connection)
        {
            DBContexto = connection;
            GestorDeLaMatricula = new BL.GestorDeLaMatricula(connection);
        }


        [HttpGet("GetDetallesHombresRegistrados")]
        // GET: VentasAPI
        public List<Model.Estudiante> GetDetallesHombresRegistrados()
        {
            return GestorDeLaMatricula.ListeLosHombresRegistrados();
        }


    }
}
