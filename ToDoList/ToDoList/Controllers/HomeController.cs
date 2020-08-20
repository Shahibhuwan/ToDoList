using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _content;

        public HomeController()
        {
             _content = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _content.Dispose();
        }

        public ActionResult Index()
        {
           var information = _content.Informations.ToList();
            return View(information);
        }

        
     
        public ActionResult New()
        {
            return View();
        }


       
        public ActionResult Edit(int id)
        {
            var info = _content.Informations.SingleOrDefault(c => c.Id == id);

           
            return View("New",info);
        }


        
        public ActionResult Delete(int id)
        {
            var infoInDb = _content.Informations.SingleOrDefault(c => c.Id == id);
            _content.Informations.Remove(infoInDb);
            _content.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Create(Information information)
        {
            if(!ModelState.IsValid)
            {
                var info = new Information
                {
                    Id = information.Id,
                    Note = information.Note,
                    Subject = information.Note,
                    Date = information.Date
                };
                return View();

            }

            if (information.Id == 0)
            {
                _content.Informations.Add(information);
            }

            else
            {
                var infoInDb = _content.Informations.SingleOrDefault(c => c.Id == information.Id);
                infoInDb.Note = information.Note;
                infoInDb.Subject = infoInDb.Subject;
                infoInDb.Date = information.Date;

            }
            _content.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
            


    }

}