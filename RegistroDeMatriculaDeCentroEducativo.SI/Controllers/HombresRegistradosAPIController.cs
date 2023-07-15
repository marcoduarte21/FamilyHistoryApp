using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RegistroDeMatriculaDeCentroEducativo.SI.Controllers
{
    public class HombresRegistradosAPIController : Controller
    {
        // GET: HombresRegistradosAPIController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HombresRegistradosAPIController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HombresRegistradosAPIController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HombresRegistradosAPIController/Create
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

        // GET: HombresRegistradosAPIController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HombresRegistradosAPIController/Edit/5
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

        // GET: HombresRegistradosAPIController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HombresRegistradosAPIController/Delete/5
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
