using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SS1314CarWashing.Controllers
{
    public class SaleReportController : Controller
    {
        // GET: SaleReportController
        public ActionResult Index()
        {
            return View();
        }

        // GET: SaleReportController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SaleReportController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SaleReportController/Create
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

        // GET: SaleReportController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SaleReportController/Edit/5
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

        // GET: SaleReportController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SaleReportController/Delete/5
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
