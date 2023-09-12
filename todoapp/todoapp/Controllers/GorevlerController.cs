using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using todoapp.Models;
using PagedList;

namespace todoapp.Controllers
{
    public class GorevlerController : Controller
    {
        todoEntities1 model = new todoEntities1();
        // GET: Gorevler
        public ActionResult Index(int page=1,int pageSize=5)
        {
            var gorevler = model.GOREV.ToList().ToPagedList(page, pageSize);

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

        [HttpPost]
        public int gorevTamamla(int id)
        {
            GOREV gorev = model.GOREV.FirstOrDefault(x => x.gorevId == id);
            try
            {
                if (gorev != null)
                {
                    if (gorev.tamamlananId == null)
                    {
                        TAMAMLANAN tamamlanan = new TAMAMLANAN();
                        model.TAMAMLANAN.Add(tamamlanan);

                        gorev.tamamlananId = tamamlanan.tamamlananId;
                    }
                    else
                    {
                        TAMAMLANAN tamamlanan = model.TAMAMLANAN.FirstOrDefault(x => x.tamamlananId == gorev.tamamlananId);
                        model.TAMAMLANAN.Remove(tamamlanan);
                    }
                }

                model.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpPost]
        public int gorevYildizla(int id)
        {
            GOREV gorev = model.GOREV.FirstOrDefault(x => x.gorevId == id);
            try
            {
                if (gorev != null)
                {
                    if (gorev.onemliId == null)
                    {
                        ONEMLI onemli = new ONEMLI();
                        model.ONEMLI.Add(onemli);

                        gorev.onemliId = onemli.onemliId;
                    }
                    else
                    {
                        ONEMLI onemli = model.ONEMLI.FirstOrDefault(x => x.onemliId == gorev.onemliId);
                        model.ONEMLI.Remove(onemli);
                    }
                }
                model.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        [HttpPost]
        public int gorevSil(int id)
        {
            GOREV gorev = model.GOREV.FirstOrDefault(x => x.gorevId == id);
            try
            {
                model.GOREV.Remove(gorev);
                model.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }



        /*
        [HttpPost]
        public int gorevSil(int id)
        {
            GOREV gorev = model.GOREV.FirstOrDefault(x => x.gorevId == id);
            try
            {
                model.GOREV.Remove(gorev);
                model.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        */

        /*
        [HttpGet]
        public ActionResult gorevTamamla(int id)
        {
            GOREV gorev = model.GOREV.FirstOrDefault(x => x.gorevId == id);

            if (gorev != null)
            {
                return View(gorev);
            }

            return null;
        }

        [HttpPost]
        public ActionResult gorevTamamla(GOREV g)
        {
            GOREV gorev = model.GOREV.FirstOrDefault(x => x.gorevId == g.gorevId);

            if (gorev != null)
            {
                if (gorev.tamamlananId == null)
                {
                    TAMAMLANAN tamamlanan = new TAMAMLANAN();
                    model.TAMAMLANAN.Add(tamamlanan);

                    gorev.tamamlananId = tamamlanan.tamamlananId;
                }
                else
                {
                    TAMAMLANAN tamamlanan = model.TAMAMLANAN.FirstOrDefault(x => x.tamamlananId == gorev.tamamlananId);
                    model.TAMAMLANAN.Remove(tamamlanan);
                }
            }

            model.SaveChanges();

            return RedirectToAction("Index");
        }
        */

        /*
        [HttpGet]
        public ActionResult gorevYildizla(int id)
        {
            GOREV gorev = model.GOREV.FirstOrDefault(x => x.gorevId == id);

            if (gorev != null)
            {
                return View(gorev);
            }

            return null;
        }

        [HttpPost]
        public ActionResult gorevYildizla(GOREV g)
        {
            GOREV gorev = model.GOREV.FirstOrDefault(x => x.gorevId == g.gorevId);

            if (gorev != null)
            {
                if (gorev.onemliId == null)
                {
                    ONEMLI onemli = new ONEMLI();
                    model.ONEMLI.Add(onemli);

                    gorev.onemliId = onemli.onemliId;
                }
                else
                {
                    ONEMLI onemli = model.ONEMLI.FirstOrDefault(x => x.onemliId == gorev.onemliId);
                    model.ONEMLI.Remove(onemli);
                }
            }

            model.SaveChanges();

            return RedirectToAction("Index");
        }
        */



        /*
        [HttpGet]
        public ActionResult gorevSil(int id)
        {
            GOREV gorev = model.GOREV.FirstOrDefault(x => x.gorevId == id);

            if (gorev != null)
            {
                return View(gorev);
            }

            return null;
        }

        [HttpPost]
        public ActionResult gorevSil(GOREV g)
        {
            GOREV gorev = model.GOREV.FirstOrDefault(x => x.gorevId == g.gorevId);
            ONEMLI onemli = model.ONEMLI.FirstOrDefault(x => x.onemliId == g.onemliId);
            PLANLANAN planlanan = model.PLANLANAN.FirstOrDefault(x => x.planlananId == g.planlananId);
            TAMAMLANAN tamamlanan = model.TAMAMLANAN.FirstOrDefault(x => x.tamamlananId == g.tamamlananId);

            if(onemli != null)
            {
                model.ONEMLI.Remove(onemli);
            }

            if(planlanan != null)
            {
                model.PLANLANAN.Remove(planlanan);
            }

            if(tamamlanan != null)
            {
                model.TAMAMLANAN.Remove(tamamlanan);
            }

            if (gorev != null)
            {
                model.GOREV.Remove(gorev);
            }

            model.SaveChanges();

            return RedirectToAction("Index");
        }
       */

    }
}