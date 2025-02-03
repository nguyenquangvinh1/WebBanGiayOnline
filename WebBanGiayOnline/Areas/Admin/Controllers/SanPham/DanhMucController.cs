using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{
    public class DanhMucController : Controller
    {
        // GET: DanhMucController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DanhMucController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DanhMucController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DanhMucController/Create
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

        // GET: DanhMucController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DanhMucController/Edit/5
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

        // GET: DanhMucController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DanhMucController/Delete/5
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
