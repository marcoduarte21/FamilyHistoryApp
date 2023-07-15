using Microsoft.AspNetCore.Mvc;

namespace RegistroDeMatriculaDeCentroEducativo.UI.Controllers
{
    public class EstudianteController : Controller
    {

        private DA.DBContexto Connection;
        BL.GestorDeLaMatricula GestorDeLaMatricula;

        public EstudianteController(DA.DBContexto connection)
        {
            Connection = connection;
            GestorDeLaMatricula = new BL.GestorDeLaMatricula(Connection);
        }
        // GET: EstudianteController
        public ActionResult Index()
        {
            
            List<Model.Estudiante> ListaDeEstudiantes;
            ListaDeEstudiantes = GestorDeLaMatricula.ListeLosEstudiantes();
            return View(ListaDeEstudiantes);
        }

        // GET: EstudianteController/Details/5
        public ActionResult Details(int id)
        {
            Model.Estudiante estudiante;
            estudiante = GestorDeLaMatricula.RetorneElEstudiantePorId(id);
            ViewBag.edad = GestorDeLaMatricula.RetorneLaEdad(estudiante);
            return View(estudiante);
        }

        // GET: EstudianteController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: EstudianteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Model.Estudiante estudiante)
        {
            try
            {
                GestorDeLaMatricula.Registre(estudiante);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: EstudianteController/Edit/5
        public ActionResult Edit(int id)
        {
            Model.Estudiante estudiante;
                estudiante = GestorDeLaMatricula.RetorneElEstudiantePorId(id);
            return View(estudiante);
        }

        // POST: EstudianteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Model.Estudiante estudiante)
        {
            try
            {
                GestorDeLaMatricula.Edite(estudiante.Id, estudiante.Cedula, estudiante.Nombre, estudiante.PrimerApellido, estudiante.SegundoApellido, estudiante.Sexo, (DateTime)estudiante.FechaDeNacimiento,
                estudiante.CedulaMadre, estudiante.CedulaPadre);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult HistorialFamiliar(string cedula)
        {

            List<Model.Estudiante> PadresDelEstudiante;
            PadresDelEstudiante = GestorDeLaMatricula.ListeLosPadres(cedula);
            ViewBag.ListaDePadres = PadresDelEstudiante;

            List<Model.Estudiante> AbuelosDelEstudiante;
            AbuelosDelEstudiante = GestorDeLaMatricula.ListeLosAbuelos(cedula);
            ViewBag.ListaDeLosAbuelos = AbuelosDelEstudiante;

            List<Model.Estudiante> HermanosDelEstudiante;
            HermanosDelEstudiante = GestorDeLaMatricula.ListeLosHermanos(cedula);
            ViewBag.ListaDeHermanos = HermanosDelEstudiante;

            List<Model.Estudiante> TiosDelEstudiante;
            TiosDelEstudiante = GestorDeLaMatricula.ListeLosTios(cedula);
            ViewBag.ListaDeTios = TiosDelEstudiante;

            List<Model.Estudiante> PrimosDelEstudiante;
            PrimosDelEstudiante = GestorDeLaMatricula.ListeLosPrimos(cedula);
            ViewBag.ListaDePrimos = PrimosDelEstudiante;

            List<Model.Estudiante> HijosDelEstudiante;
            HijosDelEstudiante = GestorDeLaMatricula.ListeLosHijos(cedula);
            ViewBag.ListaDeHijos = HijosDelEstudiante;

            return View();

        }

        public ActionResult CreateStudent()
        {

            return View();
        }

        
        

    }


}
