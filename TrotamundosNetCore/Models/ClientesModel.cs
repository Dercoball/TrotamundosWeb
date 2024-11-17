using TrotamundosNetCore.Clases.Util;
using TrotamundosNetCore.Clases;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace TrotamundosNetCore.Models
{
	public class ClientesModel
	{
		private readonly IConfiguration _configuration;
		private readonly string connectionString;

		public ClientesModel(IConfiguration configuration)
		{
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

			string ip = _configuration["Conexiones:Monitoreo:trotamundos:Host"];
			string usuario = _configuration["Conexiones:Monitoreo:trotamundos:User"];
			string clave = _configuration["Conexiones:Monitoreo:trotamundos:Pass"];
			string proceso = _configuration["Conexiones:Monitoreo:trotamundos:Titulo"];

			connectionString = $"Server={ip};User Id={Encrypt.Desencriptar(usuario)};Password={Encrypt.Desencriptar(clave)};Trusted_Connection=False;Application Name={proceso}";
		}

		// Método para ejecutar una consulta SQL
		public List<T> EjecutarConsultaSql<T>(string query) where T : class, new()
		{
			if (string.IsNullOrEmpty(query)) throw new ArgumentException("El query no puede estar vacío.");

			ConnectSQL dbSql = new ConnectSQL();
			return dbSql.ejecutaQuerySql<T>(connectionString, query);
		}

		// Método para ejecutar un stored procedure con salida tipada
		public List<T> EjecutarStoredProcedure<T>(string storedProcedure, Dictionary<string, object> parametros = null) where T : class, new()
		{
			if (string.IsNullOrEmpty(storedProcedure)) throw new ArgumentException("El nombre del stored procedure no puede estar vacío.");

			ConnectSQL dbSql = new ConnectSQL();
			return dbSql.ejecutaStoredProcedure<T>(connectionString, storedProcedure, parametros);
		}

		// Método para obtener una lista de clientes
		public List<Clientes> ObtenerClientes()
		{
			string query = "EXEC [trotamundosdb].dbo.ObtenerClientesTotales";
			return EjecutarConsultaSql<Clientes>(query);
		}

		// Método para eliminar un vehículo por ID
		public bool EliminarVehiculo(int idVehiculo)
		{
			if (idVehiculo <= 0) throw new ArgumentException("El ID del vehículo debe ser un número positivo.");

			string query = $"DELETE FROM Trotamundos.dbo.Vehiculos WHERE ID = {idVehiculo}";

			try
			{
				EjecutarConsultaSql<Clientes>(query);
				return true;
			}
			catch (Exception ex)
			{
				// Manejo de errores según sea necesario
				Console.WriteLine($"Error al eliminar vehículo: {ex.Message}");
				return false;
			}
		}

		// Método para obtener la fecha de la base de datos
		public DateTime ObtenerFechaBD()
		{
			ConnectSQL dbSql = new ConnectSQL();
			return dbSql.ObtenerFecha(connectionString);
		}
	}
}
