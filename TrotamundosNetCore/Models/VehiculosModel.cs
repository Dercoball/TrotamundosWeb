using TrotamundosNetCore.Clases.Util;
using TrotamundosNetCore.Clases;

namespace TrotamundosNetCore.Models
{
    public class VehiculosModel
    {
        internal string connectionString { get; private set; }
        private readonly IConfiguration _configuration;


        public VehiculosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public VehiculosModel() : base()
        {
            string ip = _configuration["Host"];
            string usuario = _configuration["User"];
            string clave = _configuration["Pass"];
            string proceso = _configuration["Titulo"];
            connectionString = $"Server={ip};user id={Encrypt.Desencriptar(usuario)};password={Encrypt.Desencriptar(clave)};Trusted_Connection=False;Application Name={proceso}";
        }


        public List<T> ejecutaQuerySql<T>(string query) where T : class, new()
        {
            ConnectSQL dbSql = new ConnectSQL();
            return dbSql.ejecutaQuerySql<T>(connectionString, query);
        }

        /// <summary>
        /// Ejecuta consulta SQL a base de datos, regresa filas en <see cref="List{T}"/>
        /// </summary>
        /// <typeparam name="T">Clase con propiedades que concuerdan con las columnas de la consulta</typeparam>
        /// <param name="query">Consulta SQL a ejecutar</param>
        /// <returns><see cref="List{T}"/> con los registros de la consulta como objetos de tipo <see cref="T"/></returns>
        public List<T> EjecutaQuery<T>(string query) where T : class, new()
        {
            ConnectSQL dbSql = new ConnectSQL();
            return dbSql.ejecutaQuerySql<T>(connectionString, query);
        }

        /// <summary>
        /// Ejecuta stored procedure en base de datos, regresa filas en <see cref="List{T}"/>
        /// </summary>
        /// <typeparam name="T">Clase con propiedades que concuerdan con las columnas de la salida del stored procedure</typeparam>
        /// <param name="sp">Stored Procedure a ejecutar en la base de datos</param>
        /// <param name="parametros">Parametros de stored procedure, los identificadores deben coincidir con los definidos en el stored procedure. No requieren tener el prefijo @</param>
        /// <returns><see cref="List{T}"/> con los registros de salida como objetos de tipo <see cref="T"/></returns>
        public List<T> EjecutaStoredProcedure<T>(string sp, Dictionary<string, object> parametros = null) where T : class, new()
        {
            ConnectSQL dbSql = new ConnectSQL();
            return dbSql.ejecutaStoredProcedure<T>(connectionString, sp, parametros);
        }

        /// <summary>
        /// Ejecuta consulta SQL a base de datos, regresa filas en lista con las columnas separadas por el caracter '|'
        /// </summary>
        /// <param name="query">Consulta SQL a ejecutar</param>
        /// <returns><see cref="List{string}"/> con los registros de la consulta</returns>
        public List<string> EjecutaQuery(string query)
        {
            ConnectSQL dbSql = new ConnectSQL();
            return dbSql.ejecutaQuerySql(connectionString, query);
        }

        /// <summary>
        /// Ejecuta stored procedure en base de datos, regresa filas en lista con las columnas separadas por el caracter '|'
        /// </summary>
        /// <param name="sp">Stored Procedure a ejecutar en la base de datos</param>
        /// <param name="parametros">Parametros de stored procedure, los identificadores deben coincidir con los definidos en el stored procedure. No requieren tener el prefijo @</param>
        /// <returns><see cref="List{string}"/> con los registros de salida</returns>
        public List<string> EjecutaStoredProcedure(string sp, Dictionary<string, object> parametros = null)
        {
            ConnectSQL dbSql = new ConnectSQL();
            return dbSql.ejecutaStoredProcedure(connectionString, sp, parametros);
        }

        /// <summary>
        /// Obtener la fecha en la base de datos
        /// </summary>
        /// <returns><see cref="DateTime"/> con el valor de la fecha y hora que regresa la funcion SQL GETDATE()</returns>
        public DateTime ObtenerFechaBD()
        {
            ConnectSQL dbSql = new ConnectSQL();
            return dbSql.ObtenerFecha(connectionString);
        }

        /// <summary>
        /// Adjunta un archivo para el bot de envio de alertas para que envie un archivo
        /// </summary>
        /// <param name="archivo">Archivo a enviar</param>
        public void EnviaArchivoPorTelegram(ArchivoTelegram archivo)
        {
            EjecutaStoredProcedure("DBMensajes.dbo.EnviaArchivoPorTelegram", new Dictionary<string, object>()
            {
                {"nombre", archivo.nombre},
                {"mensaje", archivo.mensaje },
                {"sistema", archivo.sistema },
                {"subsistema", archivo.subsistema },
                {"data", archivo.data }
            });
        }


        public List<Vehiculos> ObtenerVehiculos()
        {
            string query = $"exec [trotamundosdb].[dbo].ObtenerVehiculos";

            return ejecutaQuerySql<Vehiculos>(query);
        }

        public List<Vehiculos> ModificarVehículo(int idVehiculo)
        {
            string query = $"delete from  Trotamundos.dbo.Vehiculos where ID = {idVehiculo}";

            return ejecutaQuerySql<Vehiculos>(query);
        }
    }
}
