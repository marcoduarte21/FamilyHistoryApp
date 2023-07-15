using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RegistroDeMatriculaDeCentroEducativo.SI.Controllers
{
    public class MujeresRegistradasAPIController : Controller
    {
        // GET: MujeresRegistradasAPIController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MujeresRegistradasAPIController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MujeresRegistradasAPIController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MujeresRegistradasAPIController/Create
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

        // GET: MujeresRegistradasAPIController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MujeresRegistradasAPIController/Edit/5
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

        // GET: MujeresRegistradasAPIController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MujeresRegistradasAPIController/Delete/5
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
