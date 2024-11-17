using TrotamundosNetCore.Clases.Util;
using TrotamundosNetCore.Clases;
using System.Data.SqlClient;
using System.Data;

namespace TrotamundosNetCore.Models
{
	public class Model
	{
		internal string ConnectionString { get; private set; }

		public Model(string host, string user, string pass, string proceso)
		{
			// Desencriptación de usuario y contraseña
	

			// Creación de la cadena de conexión utilizando parámetros desencriptados
			ConnectionString = $"Server={host};User ID={user};Password={pass};Trusted_Connection=False;Application Name={proceso};";
		}

		public List<T> EjecutaQuerySql<T>(string query) where T : class, new()
		{
			ConnectSQL dbSql = new ConnectSQL();
			return dbSql.ejecutaQuerySql<T>(ConnectionString, query);
		}

		public List<T> EjecutaStoredProcedure<T>(string sp, Dictionary<string, object> parametros = null) where T : class, new()
		{
			ConnectSQL dbSql = new ConnectSQL();
			return dbSql.ejecutaStoredProcedure<T>(ConnectionString, sp, parametros);
		}

		public List<string> EjecutaQuery(string query)
		{
			ConnectSQL dbSql = new ConnectSQL();
			return dbSql.ejecutaQuerySql(ConnectionString, query);
		}

		public List<string> EjecutaStoredProcedure(string sp, Dictionary<string, object> parametros = null)
		{
			ConnectSQL dbSql = new ConnectSQL();
			return dbSql.ejecutaStoredProcedure(ConnectionString, sp, parametros);
		}

		public DateTime ObtenerFechaBD()
		{
			ConnectSQL dbSql = new ConnectSQL();
			return dbSql.ObtenerFecha(ConnectionString);
		}

		public void EnviaArchivoPorTelegram(ArchivoTelegram archivo)
		{
			EjecutaStoredProcedure("DBMensajes.dbo.EnviaArchivoPorTelegram", new Dictionary<string, object>
			{
				{"nombre", archivo.nombre},
				{"mensaje", archivo.mensaje },
				{"sistema", archivo.sistema },
				{"subsistema", archivo.subsistema },
				{"data", archivo.data }
			});
		}

		public List<Clientes> ObtenerClientes()
		{
			string query = "EXEC [trotamundosdb].dbo.ObtenerClientesTotales";
			return EjecutaQuerySql<Clientes>(query);
		}

        public List<Vehiculos> ObtenerVehiculos()
        {
            string query = $"exec [trotamundosdb].[dbo].ObtenerVehiculos";

            return EjecutaQuerySql<Vehiculos>(query);
        }

 
            // Consulta con parámetro
         public List<Vehiculos> ObtenerVehiculo(int IdVehiculo)
         {
             
             string query = $"exec [trotamundosdb].[dbo].[ObtenerVehiculoWeb] @IdVehiculo = {IdVehiculo}";

             
             return EjecutaQuerySql<Vehiculos>(query);
         }

        



        public List<Clientes> ModificarVehículo(int idVehiculo)
		{
			string query = $"DELETE FROM Trotamundos.dbo.Vehiculos WHERE ID = {idVehiculo}";
			return EjecutaQuerySql<Clientes>(query);
		}
	}
}
