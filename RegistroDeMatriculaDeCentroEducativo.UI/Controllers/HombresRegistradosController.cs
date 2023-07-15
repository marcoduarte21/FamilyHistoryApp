using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RegistroDeMatriculaDeCentroEducativo.UI.Controllers
{
    public class HombresRegistradosController : Controller
    {

        private DA.DBContexto Connection;
        BL.GestorDeLaMatricula GestorDeLaMatricula;

        public HombresRegistradosController(DA.DBContexto connection)
        {
            Connection = connection;
            GestorDeLaMatricula = new BL.GestorDeLaMatricula(Connection);
        }
        // GET: HombresRegistradosController
        public ActionResult Index()
        {
            List<Model.Estudiante> Lista;
            Lista = GestorDeLaMatricula.ListeLosHombresRegistrados();
            return View(Lista);
        }

        // GET: HombresRegistradosController/Details/5
        public ActionResult Details(string cedula)
        {
            Model.Estudiante estudiante;
            estudiante = GestorDeLaMatricula.RetorneElEstudiantePorIdentificacion(cedula);
            ViewBag.edad = GestorDeLaMatricula.RetorneLaEdad(estudiante);
            return View(estudiante);
        }
        
    }
}
