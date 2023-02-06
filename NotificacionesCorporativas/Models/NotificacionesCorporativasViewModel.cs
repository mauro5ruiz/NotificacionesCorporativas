using RP601.Business;
using System.ComponentModel.DataAnnotations;

namespace NotificacionesCorporativas.Models
{
    public class NotificacionesCorporativasViewModel
    {
        [Display(Name = "Id")]
        public int IdNotificacionCorportaiva { get; set; }

        [Required(ErrorMessage = "Debe introducir la información de la notificación")]
        [Display(Name = "Descripcion")]
        public string Notificacion { get; set; }
        public DateTime FechaAlta { get; set; }
        public int Estado { get; set; }
        public int Orden { get; set; }
        public List<Notificacion> Notificaciones { get; set; }
    }
}
