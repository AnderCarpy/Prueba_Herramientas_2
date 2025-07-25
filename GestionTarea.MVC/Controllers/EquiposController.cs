using GestionTarea.ApiConsumer;
using GestionTareas.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionTarea.MVC.Controllers
{
    public class EquiposController : Controller
    {
        // GET: EquiposController
        public ActionResult Index()
        {
            var equipos = CRUD<Equipo>.GetAll();
            return View(equipos);
        }

        // GET: EquiposController/Details/5
        public ActionResult Details(int id)
        {
            var equipo = CRUD<Equipo>.GetById(id);
            return View(equipo);
        }
        
        // GET: EquiposController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EquiposController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Equipo collection)
        {
            try
            {
                CRUD<Equipo>.Create(collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EquiposController/Edit/5
        public ActionResult Edit(int id)
        {
           var equipo = CRUD<Equipo>.GetById(id);
            return View(equipo);
        }

        // POST: EquiposController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Equipo collection)
        {
            try
            {
                CRUD<Equipo>.Update(id, collection);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EquiposController/Delete/5
        public ActionResult Delete(int id)
        {
           var equipo = CRUD<Equipo>.GetById(id);
            return View(equipo);
        }

        // POST: EquiposController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                CRUD<Equipo>.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
