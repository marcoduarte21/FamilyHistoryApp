using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RegistroDeMatriculaDeCentroEducativo.SI.Controllers
{
    public class EstudianteAPIController : Controller
    {
        // GET: EstudianteAPIController
        public ActionResult Index()
        {
            return View();
        }

        // GET: EstudianteAPIController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstudianteAPIController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstudianteAPIController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstudianteAPIController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstudianteAPIController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstudianteAPIController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EstudianteAPIController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
