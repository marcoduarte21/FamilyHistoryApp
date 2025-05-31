using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistroDeMatriculaDeCentroEducativo.BL.interfaces;
using RegistroDeMatriculaDeCentroEducativo.Model.Entities;

namespace RegistroDeMatriculaDeCentroEducativo.SI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MujeresRegistradasAPIController : ControllerBase
    {
        DA.DBContexto DBContexto;
        GestorDeLaMatricula GestorDeLaMatricula;
        public MujeresRegistradasAPIController(DA.DBContexto connection)
        {
            DBContexto = connection;
            GestorDeLaMatricula = new BL.GestorDeLaMatricula(connection);
        }


        [HttpGet("GetDetallesMujeresRegistradas")]
        // GET: VentasAPI
        public List<Estudiante> GetDetallesMujeresRegistradas()
        {
            return GestorDeLaMatricula.ListeLasMujeresRegistradas();
        }

    }

}
