using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using todoapp.Models;
using PagedList;

namespace todoapp.Controllers
{
    public class PlanlananController : Controller
    {
        todoEntities1 model = new todoEntities1();
        // GET: Planlanan
        public ActionResult Index(int page = 1, int pageSize = 5)
        {

            var gorev = model.GOREV.ToList().ToPagedList(page, pageSize);

            for (var i = 0; i < gorev.Count; i++)
            {

                if (gorev[i].gorevTarihi != null)
                {
                    gorev[i].gorevTarihiString = gorev[i].gorevTarihi.Value.ToString("dd-MM-yyyy");
                }
                else
                {
                    gorev[i].gorevTarihiString = "";
                }

            }

            return View(gorev);

            /*
            List<PLANLANAN> planlanan = model.PLANLANAN.ToList();

            List<GOREV> gorev = model.GOREV.ToList();
            ViewBag.gorev = gorev;

            return View(planlanan);
            */
        }

        [HttpGet]
        public ActionResult gorevEkle()
        {
            GOREV gorev = new GOREV();

            return View(gorev);
        }

        [HttpPost]
        public ActionResult gorevEkle(GOREV gorev)
        {
            if (gorev.gorevTarihi == null)
            {
                gorev.gorevTarihi = DateTime.Now;
            }

            model.GOREV.Add(gorev);

            PLANLANAN planlanan = new PLANLANAN();
            model.PLANLANAN.Add(planlanan);
            gorev.planlananId = planlanan.planlananId;

            model.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}