using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using todoapp.Models;
using PagedList;

namespace todoapp.Controllers
{
    public class GunumController : Controller
    {
        todoEntities1 model = new todoEntities1();
        // GET: Gunum
        public ActionResult Index(int page = 1, int pageSize = 3)
        {
            var gorevler = model.GOREV.ToList();
            //.ToPagedList(page, pageSize);

            for (var i = 0; i < gorevler.Count; i++)
            {

                if (gorevler[i].gorevTarihi != null)
                {
                    gorevler[i].gorevTarihiString = gorevler[i].gorevTarihi.Value.ToString("dd-MM-yyyy");
                }
                else
                {
                    gorevler[i].gorevTarihiString = "";
                }

            }

            return View(gorevler);
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

            model.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}