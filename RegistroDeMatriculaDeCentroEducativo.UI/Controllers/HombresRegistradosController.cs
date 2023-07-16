using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;

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
        public async Task<IActionResult> Index()
        {

            var httpClient = new HttpClient();
            List<Model.Estudiante> lista;


            var respuesta = await httpClient.GetAsync("https://api-matricula-estudiantes.azurewebsites.net/api/HombresRegistradosAPI/GetDetallesHombresRegistrados");
            string apiRespuesta = await respuesta.Content.ReadAsStringAsync();
            lista = JsonConvert.DeserializeObject<List<Model.Estudiante>>(apiRespuesta);

            return View(lista);
        }

        // GET: HombresRegistradosController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Model.Estudiante estudiante;

            var httpClient = new HttpClient();

            var query = new Dictionary<string, string>()
            {

                ["id"] = id.ToString(),
            };

            var uri = QueryHelpers.AddQueryString("https://api-matricula-estudiantes.azurewebsites.net/api/EstudianteAPI/GetEstudiante", query);

            var response = await httpClient.GetAsync(uri);
            string apiResponse = await response.Content.ReadAsStringAsync();
            estudiante = JsonConvert.DeserializeObject<Model.Estudiante>(apiResponse);


            ViewBag.edad = GestorDeLaMatricula.RetorneLaEdad(estudiante);
            return View(estudiante);
        }

    }
}
