﻿using Microsoft.AspNetCore.Mvc;
using System.Data;
using ThemisWorkshop.Models;

namespace ThemisWorkshop.Controllers
{
    public class TareaController : Controller
    {
        private readonly ThemisworkshopContext _context;

        public static ThemisworkshopContext _temp;

       public static Usuario? usuario;

        public TareaController(ThemisworkshopContext context)
        {
            _context = context;
            _temp = _context;
        }

        [HttpGet]
        [Route("/Tarea/ListarTareas/{pag}")]
        public ActionResult ListarTareas(int pag) 
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            if (pag <= 0) 
            { 
                pag = 1;
            }
            List<Tarea> tareas = LoadTareas(pag);
            return View("ListarTareas",tareas);
        }

        [HttpGet]
        public ActionResult AgregarTarea()
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            TareaViewModel model = new TareaViewModel(null,usuario,false);
            return View("AgregarTarea",model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTarea() 
        {
            int idUsuario = int.Parse(Request.Form["idUsuario"].ToString());
            int idExpediente = int.Parse(Request.Form["idExpediente"].ToString());
            string asunto = Request.Form["asunto"].ToString();
            string descripcion = Request.Form["descripcion"].ToString();
            DateTime fecha = DateTime.Parse(Request.Form["fecha"].ToString());
            DateTime fechaUtc = DateTime.SpecifyKind(fecha, DateTimeKind.Utc);
            fechaUtc = fechaUtc.AddDays(1);
            TimeOnly hora = TimeOnly.Parse(Request.Form["hora"].ToString());

            var Tarea = new Tarea(idUsuario,idExpediente,asunto,descripcion,fechaUtc,hora);
            _context.Tarea.Add(Tarea);
            _context.SaveChanges();
            return RedirectToAction("AgregarTarea");
        }

        [HttpGet]
        [Route("/Tarea/ModificarTarea/{id}")]
        public ActionResult ModificarTarea(int id) 
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            Tarea? tarea = _context.Tarea.Find(id);
            if (tarea != null)
            {
                TareaViewModel model = new TareaViewModel(tarea,usuario,true);
                return View("AgregarTarea",model);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModTarea() 
        {
            int id = int.Parse(Request.Form["idTarea"].ToString());
            Tarea? tarea = _context.Tarea.Find(id);
            if (tarea != null)
            {
                int idExpediente = int.Parse(Request.Form["idExpediente"].ToString());
                string asunto = Request.Form["asunto"].ToString();
                string descripcion = Request.Form["descripcion"].ToString();
                DateTime fecha = DateTime.Parse(Request.Form["fecha"].ToString());
                DateTime fechaUtc = DateTime.SpecifyKind(fecha, DateTimeKind.Utc);
                fechaUtc = fechaUtc.AddDays(1);
                TimeOnly hora = TimeOnly.Parse(Request.Form["hora"].ToString());

                tarea.IdExpediente = idExpediente;
                tarea.Asunto = asunto;
                tarea.Fecha = fechaUtc;
                tarea.Descripcion = descripcion;
                tarea.Hora = hora;
                _context.Update(tarea);
                _context.SaveChanges();
                return Redirect("/Tarea/ListarTareas/1");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        [Route("/Tarea/EliminarTarea/{id}")]
        public ActionResult EliminarTarea(int id)
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            Tarea? tarea = _context.Tarea.Find(id);
            if (tarea != null)
            {
                _context.Tarea.Remove(tarea);
                _context.SaveChanges();
                return Redirect("/Tarea/ListarTareas/1");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        [Route("/Tarea/RealizarTarea/{id}")]
        public ActionResult RealizarTarea(int id) 
        {
            usuario = _context.Usuario.Where(e => e.UserName == HttpContext.Session.GetString("usuario")).FirstOrDefault();
            if (usuario == null)
            {
                return Redirect("/Sesion/IniciarSesion");
            }
            Tarea? tarea = _context.Tarea.Find(id);
            if (tarea != null)
            {
                if (tarea.Realizado == false)
                {
                    tarea.Realizado = true;
                }
                else { 
                    tarea.Realizado = false;
                }
                tarea.Fecha = DateTime.SpecifyKind(tarea.Fecha,DateTimeKind.Utc);
                tarea.Fecha = tarea.Fecha.AddDays(1);
                _context.Tarea.Update(tarea);
                _context.SaveChanges();
                return Redirect("/Tarea/ListarTareas/1");
            }
            else 
            { 
                return RedirectToAction("Error"); 
            }
        }

        [HttpGet]
        public ActionResult Error() 
        { 
            return View("Error");
        }

        private int CantidadTareas()
        {
            return _context.Tarea.Count(e => e.IdUsuario == usuario.IdUsuario);
        }

        private List<Tarea> LoadTareas(int numPag)
        {
            int indIni = (numPag - 1) * 10;
            int max = 10;
            int cantidad = CantidadTareas();

            if (indIni >= cantidad)
            {
                return new List<Tarea>();
            }

            if (indIni + max > cantidad)
            {
                max = cantidad - indIni;
            }

            List<Tarea> tareas = _context.Tarea
                .Where(e => e.IdUsuario == usuario.IdUsuario)
                .OrderBy(e => e.IdTarea)
                .Skip(indIni)
                .Take(max)
                .ToList();
            return tareas;

        }

        public static int ObtenerPaginasFronted(int cantidadPorpagina)
        {
            return (int)Math.Ceiling((double)_temp.Tarea.Count(e => e.IdUsuario == usuario.IdUsuario) / cantidadPorpagina);
        }
    }
}
