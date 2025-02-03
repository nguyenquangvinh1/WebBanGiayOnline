using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebBanGiay.Areas.Admin.Controllers.SanPham
{
    public class ChatLieuController : Controller
    {
        // GET: ChatLieuController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ChatLieuController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ChatLieuController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChatLieuController/Create
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

        // GET: ChatLieuController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ChatLieuController/Edit/5
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

        // GET: ChatLieuController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ChatLieuController/Delete/5
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
