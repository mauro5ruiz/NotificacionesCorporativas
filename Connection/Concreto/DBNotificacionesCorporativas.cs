using Connection.Extracto;
using Microsoft.Data.SqlClient;
using RP601.Business;
using System.Data;

namespace Connection.Concreto
{
    public class DBNotificacionesCorporativas : INotificacionesCorporativas
    {
        public List<Notificacion> GetNotificacionesCorporativas()
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
            return notificacionesBd;
        }

        public void NuevaNotificacionCorporativa(string asunto, int idNotificacion)
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
