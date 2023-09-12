using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using todoapp.Models;

namespace todoapp.Controllers
{
    public class YeniListeController : Controller
    {
        todoEntities1 model = new todoEntities1();
        // GET: YeniListe
        public ActionResult Index()
        {
            List<LISTE> liste = model.LISTE.ToList();

            return View(liste);
        }

        [HttpGet]
        public ActionResult listeEkle()
        {
            LISTE liste = new LISTE();

            return View(liste);
        }

        [HttpPost]
        public ActionResult listeEkle(LISTE liste)
        {
            model.LISTE.Add(liste);
            model.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult listeDetay(int id)
        {
            LISTE liste = model.LISTE.FirstOrDefault(x => x.listeId == id);

            List<GOREV> gorev = model.GOREV.ToList();
            ViewBag.gorev = gorev;

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

            return View(liste);
        }
        
        [HttpGet]
        public ActionResult gorevEkle(int id)
        {
            GOREV gorev = new GOREV();
            gorev.listeId = id;
            model.SaveChanges();
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

        [HttpPost]
        public int gorevSil(int id)
        {
            LISTE liste = model.LISTE.FirstOrDefault(x => x.listeId == id);
            try
            {
                model.LISTE.Remove(liste);
                model.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}