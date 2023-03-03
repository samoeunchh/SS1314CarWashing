using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SS1314CarWashing.Models;

namespace SS1314CarWashing.Controllers
{
    public class SaleReportController : Controller
    {
        private readonly AppDbContext _context;

        public SaleReportController(AppDbContext context)
        {
            _context = context;
        }
        // GET: SaleReportController
        public ActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> GetData(string fromDate,string toDate)
        {
            if (string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
            {
                return null;
            }
            else
            {
                var fDate = DateTime.Parse(fromDate + " 00:00:00");
                var tDate = DateTime.Parse(toDate + " 23:59:59");
                var sale = await (from s in _context.Sale
                                  join c in _context.Customer
                                  on s.CustomerId equals c.CustomerId
                                  where s.IssueDate >= fDate && s.IssueDate <= tDate
                                  orderby s.IssueDate descending
                                  select new SaleDTO
                                  {
                                      SaleId = s.SaleId,
                                      CustomerName = c.CustomerName,
                                      Total = s.Total,
                                      GrandTotal = s.GrandTotal,
                                      Discount = s.Discount,
                                      IssueDate = s.IssueDate,
                                      InvoiceNumber = s.InvoiceNumber,
                                  }).ToListAsync();
                return Json(sale);
            }
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
