using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using todoapp.Models;
using PagedList;

namespace todoapp.Controllers
{
    public class TamamlananController : Controller
    {
        todoEntities1 model = new todoEntities1();
        // GET: Tamamlanan
        public ActionResult Index(int page = 1, int pageSize = 4)
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
            var tamamlanan = model.TAMAMLANAN.ToList().ToPagedList(page, pageSize);

            List<GOREV> gorev = model.GOREV.ToList();
            ViewBag.gorev = gorev;

            return View(tamamlanan);
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
            model.GOREV.Add(gorev);

            if (gorev.gorevTarihi != null)
            {
                PLANLANAN planlanan = new PLANLANAN();
                model.PLANLANAN.Add(planlanan);
                gorev.planlananId = planlanan.planlananId;
            }

            TAMAMLANAN tamamlanan = new TAMAMLANAN();
            model.TAMAMLANAN.Add(tamamlanan);
            gorev.tamamlananId = tamamlanan.tamamlananId;

            model.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}