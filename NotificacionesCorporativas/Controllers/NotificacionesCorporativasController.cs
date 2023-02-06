using Connection.Concreto;
using Connection.Extracto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NotificacionesCorporativas.Models;
using RP601.Business;
using RP601.Business.Concreto;
using System.Data;
using System.Drawing;

namespace NotificacionesCorporativas.Controllers
{
    public class NotificacionesCorporativasController : Controller
    {
        private INotificacionesCorporativas repositorioNotificaciones;
        public NotificacionesCorporativasController(INotificacionesCorporativas repoNotificacionesCorporativas)
        {
            repositorioNotificaciones = repoNotificacionesCorporativas;
        }

        public IActionResult Index()
        {
            List<Notificacion> notificacionesBd = repositorioNotificaciones.GetNotificacionesCorporativas();
                
            NotificacionesCorporativasViewModel notificaciones = new NotificacionesCorporativasViewModel()
            {
                Notificaciones = notificacionesBd,
            };
            return View(notificaciones);
        }       

        public IActionResult NuevaNotificacionCorporativa(string asunto, int idNotificacion)
        {
            repositorioNotificaciones.NuevaNotificacionCorporativa(asunto, idNotificacion);

            return RedirectToAction("index");
        }
        public void EliminarNotificacionCorporativa(int idNotCorp)
        {
            repositorioNotificaciones.EliminarNotificacionCorporativa(idNotCorp);
        }
    }
}
