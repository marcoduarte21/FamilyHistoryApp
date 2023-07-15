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
        public async Task< ActionResult> Edit(Model.Estudiante estudiante)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string json = JsonConvert.SerializeObject(estudiante);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                await httpClient.PostAsync("https://api-matricula-estudiantes.azurewebsites.net/api/EstudianteAPI/EditStudent", byteContent);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> HistorialFamiliar(string cedula)
        {

            List<Model.Estudiante> PadresDelEstudiante;
            PadresDelEstudiante = await GetPadres();
            ViewBag.ListaDePadres = PadresDelEstudiante;

            List<Model.Estudiante> AbuelosDelEstudiante;
            AbuelosDelEstudiante = await GetAbuelos();
            ViewBag.ListaDeLosAbuelos = AbuelosDelEstudiante;

            List<Model.Estudiante> HermanosDelEstudiante;
            HermanosDelEstudiante = await GetHermanos();
            ViewBag.ListaDeHermanos = HermanosDelEstudiante;

            List<Model.Estudiante> TiosDelEstudiante;
            TiosDelEstudiante = await GetTios();
            ViewBag.ListaDeTios = TiosDelEstudiante;

            List<Model.Estudiante> PrimosDelEstudiante;
            PrimosDelEstudiante = await GetPrimos();
            ViewBag.ListaDePrimos = PrimosDelEstudiante;

            List<Model.Estudiante> HijosDelEstudiante;
            HijosDelEstudiante = await GetHijos();
            ViewBag.ListaDeHijos = HijosDelEstudiante;

            return View();

        }

        public static async Task<List<Model.Estudiante>> GetPadres()
        {

            List<Model.Estudiante> ListaDeEstudiantes;

            var httpClient = new HttpClient();
            var respuesta = await httpClient.GetAsync("https://api-matricula-estudiantes.azurewebsites.net/api/HistorialFamiliarAPI/GetPadres");
            string apiRespuesta = await respuesta.Content.ReadAsStringAsync();
            ListaDeEstudiantes = JsonConvert.DeserializeObject<List<Model.Estudiante>>(apiRespuesta);

            return ListaDeEstudiantes.ToList();
        }

        public async Task<List<Model.Estudiante>> GetHijos()
        {

            List<Model.Estudiante> ListaDeEstudiantes;

            var httpClient = new HttpClient();
            var respuesta = await httpClient.GetAsync("https://api-matricula-estudiantes.azurewebsites.net/api/HistorialFamiliarAPI/GetHijos");
            string apiRespuesta = await respuesta.Content.ReadAsStringAsync();
            ListaDeEstudiantes = JsonConvert.DeserializeObject<List<Model.Estudiante>>(apiRespuesta);

            return ListaDeEstudiantes.ToList();
        }


        public async Task<List<Model.Estudiante>> GetAbuelos()
        {

            List<Model.Estudiante> ListaDeEstudiantes;

            var httpClient = new HttpClient();
            var respuesta = await httpClient.GetAsync("https://api-matricula-estudiantes.azurewebsites.net/api/HistorialFamiliarAPI/GetAbuelos");
            string apiRespuesta = await respuesta.Content.ReadAsStringAsync();
            ListaDeEstudiantes = JsonConvert.DeserializeObject<List<Model.Estudiante>>(apiRespuesta);

            return ListaDeEstudiantes.ToList();
        }

        public async Task<List<Model.Estudiante>> GetHermanos()
        {

            List<Model.Estudiante> ListaDeEstudiantes;

            var httpClient = new HttpClient();
            var respuesta = await httpClient.GetAsync("https://api-matricula-estudiantes.azurewebsites.net/api/HistorialFamiliarAPI/GetHermanos");
            string apiRespuesta = await respuesta.Content.ReadAsStringAsync();
            ListaDeEstudiantes = JsonConvert.DeserializeObject<List<Model.Estudiante>>(apiRespuesta);

            return ListaDeEstudiantes.ToList();
        }

        public async Task<List<Model.Estudiante>> GetTios()
        {

            List<Model.Estudiante> ListaDeEstudiantes;

            var httpClient = new HttpClient();
            var respuesta = await httpClient.GetAsync("https://api-matricula-estudiantes.azurewebsites.net/api/HistorialFamiliarAPI/GetTios");
            string apiRespuesta = await respuesta.Content.ReadAsStringAsync();
            ListaDeEstudiantes = JsonConvert.DeserializeObject<List<Model.Estudiante>>(apiRespuesta);

            return ListaDeEstudiantes.ToList();
        }

        public async Task<List<Model.Estudiante>> GetPrimos()
        {

            List<Model.Estudiante> ListaDeEstudiantes;

            var httpClient = new HttpClient();
            var respuesta = await httpClient.GetAsync("https://api-matricula-estudiantes.azurewebsites.net/api/HistorialFamiliarAPI/GetPrimos");
            string apiRespuesta = await respuesta.Content.ReadAsStringAsync();
            ListaDeEstudiantes = JsonConvert.DeserializeObject<List<Model.Estudiante>>(apiRespuesta);

            return ListaDeEstudiantes.ToList();
        }

    }


}
