using RP601.Business;

namespace Connection.Extracto
{
    public interface INotificacionesCorporativas
    {
        List<Notificacion> GetNotificacionesCorporativas();
        void NuevaNotificacionCorporativa(string asunto, int idNotificacion);
        void EliminarNotificacionCorporativa(int idNotCorp);
    }
}
