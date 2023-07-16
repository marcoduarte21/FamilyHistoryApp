using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

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
        public async Task<ActionResult> Index()
        {
            List<Model.Estudiante> ListaDeEstudiantes;

            var httpClient = new HttpClient();
            var respuesta = await httpClient.GetAsync("https://api-matricula-estudiantes.azurewebsites.net/api/EstudianteAPI/GetEstudiantes");
            string apiRespuesta = await respuesta.Content.ReadAsStringAsync();
            ListaDeEstudiantes = JsonConvert.DeserializeObject<List<Model.Estudiante>>(apiRespuesta);

            return View(ListaDeEstudiantes);
        }

        // GET: EstudianteController/Details/5
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

        // GET: EstudianteController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: EstudianteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Create(Model.Estudiante estudiante)
        {
            try
            {
                var httpClient = new HttpClient();

                string json = JsonConvert.SerializeObject(estudiante);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                await httpClient.PostAsync("https://api-matricula-estudiantes.azurewebsites.net/api/EstudianteAPI/CreateStudent", byteContent);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: EstudianteController/Edit/5
        public async Task< ActionResult> Edit(int id)
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

            return View(estudiante);
        }

        // POST: EstudianteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< ActionResult> Edit(Model.EstudianteParaIE estudiante)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string json = JsonConvert.SerializeObject(estudiante);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await httpClient.PutAsync("https://api-matricula-estudiantes.azurewebsites.net/api/EstudianteAPI/EditStudent", byteContent);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult HistorialFamiliar(string cedula)
        {

            Model.Estudiante estudiante = GestorDeLaMatricula.RetorneElEstudiantePorIdentificacion(cedula);
            string nombre = estudiante.Nombre;

            ViewBag.NombreDelEstudiante = nombre;

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

        

    }


}
