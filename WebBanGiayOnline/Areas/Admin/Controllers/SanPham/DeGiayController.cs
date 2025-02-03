using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{
    public class DeGiayController : Controller
    {
        // GET: DeGiayController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DeGiayController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeGiayController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeGiayController/Create
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

        // GET: DeGiayController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeGiayController/Edit/5
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

        // GET: DeGiayController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeGiayController/Delete/5
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
