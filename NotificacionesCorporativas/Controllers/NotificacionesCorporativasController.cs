using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NotificacionesCorporativas.Models;
using RP601.Business;
using System.Data;
using System.Drawing;

namespace NotificacionesCorporativas.Controllers
{
    public class NotificacionesCorporativasController : Controller
    {
        
        public IActionResult Index()
        {
            List<Notificacion> notificacionesBd = new List<Notificacion>();

            String dataSource = ConfigurationMgr.GetValue(XmlServerConfiguration.XML_KEY.DataSourceSQL);
            string cadenaConexion = "Data Source=" + dataSource + ";Initial Catalog=controlLenox2;Integrated Security=True; Encrypt=False";
            SqlConnection conecttion = new SqlConnection(cadenaConexion);
            conecttion.Open();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                // Declaramos una consulta
                string sqlSelect = "select id, notificacion from notificaciones where id_empresa = -1";

                // Abrimos la conexión y generamos un SqlCommand con la conexión y la consulta
                // Instanciamos un SqlDataAdapter a partir del SqlCommand
                conexion.Open();
                SqlCommand commandSelect = new SqlCommand(sqlSelect, conexion);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(commandSelect);

                SqlDataReader dr = commandSelect.ExecuteReader();
                DataTable schemaTable = dr.GetSchemaTable();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Notificacion noti = new Notificacion();
                        noti.Id = (int)dr["id"];
                        noti.Asunto = dr["notificacion"].ToString();
                        notificacionesBd.Add(noti);
                    }
                }
                conexion.Close();
            }            
            NotificacionesCorporativasViewModel notificaciones = new NotificacionesCorporativasViewModel()
            {
                Notificaciones = notificacionesBd,
            };
            return View(notificaciones);
        }
        
        public PartialViewResult CargarNotificacionesCorporativas()
        {
            List<Notificacion> notificacionesBd = NotificacionMgr.GetNotificacionesPorEmpresa(-1);

            NotificacionesCorporativasViewModel notificaciones = new NotificacionesCorporativasViewModel()
            {
                Notificaciones = notificacionesBd,
            };
            return PartialView(notificaciones);
        }

        public IActionResult NuevaNotificacionCorporativa(string asunto, int idNotificacion)
        {
            String dataSource = ConfigurationMgr.GetValue(XmlServerConfiguration.XML_KEY.DataSourceSQL);
            string cadenaConexion = "Data Source=" + dataSource + ";Initial Catalog=controlLenox2;Integrated Security=True; Encrypt=False";
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string query = "";
                conexion.Open();
                // el valor codigo se envía desde Js, donde 1 hace refeencia cuando se agrega una nueva notificación
                // y 2 es cuando se hace un update de la notificación.
                if (idNotificacion == 0)
                    query = $"insert into notificaciones (notificacion, leida, id_cliente, id_empresa, fecha_alta) values ('{asunto}', 0,0, -1, GETDATE())";
                else
                    query = $"update notificaciones set notificacion = '{asunto}' where  id = {idNotificacion}";

                SqlCommand commandInsert = new SqlCommand(query, conexion);
                commandInsert.ExecuteNonQuery();
                conexion.Close();
            }
            return RedirectToAction("index");
        }
        public void EliminarNotificacionCorporativa(int idNotCorp)
        {
            String dataSource = ConfigurationMgr.GetValue(XmlServerConfiguration.XML_KEY.DataSourceSQL);
            string cadenaConexion = "Data Source=" + dataSource + ";Initial Catalog=controlLenox2;Integrated Security=True; Encrypt=False";
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                conexion.Open();
                string delete = $"delete notificaciones where id = {idNotCorp}";
                SqlCommand commandDelete = new SqlCommand(delete, conexion);
                commandDelete.ExecuteNonQuery();
                conexion.Close();
            }
        }
    }
}
