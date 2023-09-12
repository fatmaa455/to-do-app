using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using todoapp.Models;
using PagedList;

namespace todoapp.Controllers
{
    public class OnemliController : Controller
    {
        todoEntities1 model = new todoEntities1();
        // GET: Onemli
        public ActionResult Index(int page=1,int pageSize=4)
        {

            var gorev = model.GOREV.ToList().ToPagedList(page, pageSize);

            for(var i =0; i< gorev.Count; i++)
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
            List<GOREV> gorev = model.GOREV.ToList();
            ViewBag.gorev = gorev;
            */
            

            /*
            var gorevler = model.GOREV.ToList().ToPagedList(page,pageSize);

            return View(gorevler);
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
            ONEMLI onemli = new ONEMLI();
            model.ONEMLI.Add(onemli);

            gorev.onemliId = onemli.onemliId;

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