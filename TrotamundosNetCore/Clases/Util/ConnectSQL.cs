using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrotamundosNetCore.Clases.Util
{
    public class ConnectSQL
    {

        private SqlConnection connectionSql;
        private SqlCommand stmtSql;

        private bool getConnectionSql(string url)
        {
            bool lb_Respuesta = true;
            try
            {
                if (connectionSql == null)
                {
                    connectionSql = new SqlConnection(url);
                    connectionSql.Open();
                }
                if (connectionSql.State == ConnectionState.Closed)
                {
                    connectionSql = new SqlConnection(url);
                    connectionSql.Open();
                }
            }
            catch (Exception e)
            {
                lb_Respuesta = false;
                throw;
            }
            return lb_Respuesta;
        }

        public void CerrarConexion()
        {
            try
            {
                if (connectionSql != null && connectionSql.State == ConnectionState.Open)
                {
                    connectionSql.Close();
                    connectionSql = null;
                }
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Ejecuta query SQL, regresa lista con columnas separadas con el caracter '|'
        /// </summary>
        /// <param name="stringConnectionSql">Cadena de conexion</param>
        /// <param name="query">Query a ejecutar</param>
        /// <returns>Lista con los resultados de la query con columnas separadas con el caracter '|'</returns>
        public List<string> ejecutaQuerySql(string stringConnectionSql, string query)
        {
            List<string> datos = new List<string>();

            using (SqlConnection connectionSql = new SqlConnection(stringConnectionSql))
            {
                try
                {
                    connectionSql.Open();
                    using (SqlCommand stmtSql = new SqlCommand(query, connectionSql))
                    {
                        using (SqlDataReader resultSql = stmtSql.ExecuteReader())
                        {
                            if (resultSql.HasRows)
                            {
                                while (resultSql.Read())
                                {
                                    int num = resultSql.FieldCount;
                                    string resultQuery = string.Empty;
                                    for (int i = 0; i < num; i++)
                                        resultQuery += resultSql.GetValue(i).ToString().Replace("|", "") + "|";
                                    datos.Add(resultQuery.Substring(0, resultQuery.Length - 1));
                                }
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    if (connectionSql.State != ConnectionState.Open)
                        connectionSql.Close();
                    throw;
                }
            }

            return datos;
        }

        /// <summary>
        /// Ejecuta query de SQL, regresa los objetos en una lista
        /// </summary>
        /// <typeparam name="T">Clase con el nombre de las columnas como propiedades, debe tener contructor vacio</typeparam>
        /// <param name="stringConnectionSql">Cadena de conexion</param>
        /// <param name="query">Query a ejecutar</param>
        /// <returns>Lista con los resultados de la query en objetos de tipo <typeparamref name="T"/></returns>
        public List<T> ejecutaQuerySql<T>(string stringConnectionSql, string query) where T : class, new()
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connectionSql = new SqlConnection(stringConnectionSql))
            {
                try
                {
                    connectionSql.Open();
                    using (SqlCommand stmtSql = new SqlCommand(query, connectionSql))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(stmtSql))
                        {
                            adapter.Fill(dataTable);
                        }
                    }

                }
                catch (Exception e)
                {
                    if (connectionSql.State != ConnectionState.Open)
                        connectionSql.Close();
                    throw;
                }
            }

            return dataTable.ToList<T>();
        }

        /// <summary>
        /// Ejecuta stored procedure de SQL, regresa lista con columnas separadas con el caracter '|'
        /// </summary>
        /// <param name="stringConnectionSql">Cadena de conexion</param>
        /// <param name="sp">Stored Procedure a ejecutar (no poner exec ni parametros)</param>
        /// <param name="parametros"><see cref="Dictionary{TKey, TValue}"/> con el nombre de la variable SQL como llave y el objeto (cadena, numero, fecha o DataTable) que contiene como valor</param>
        /// <returns>Lista con los resultados del stored procedure con columnas separadas con el caracter '|'</returns>
        public List<string> ejecutaStoredProcedure(string stringConnectionSql, string sp, Dictionary<string, object> parametros = null)
        {
            List<string> datos = new List<string>();
            using (SqlConnection connectionSql = new SqlConnection(stringConnectionSql))
            {
                try
                {
                    connectionSql.Open();
                    stmtSql = new SqlCommand(sp, connectionSql);
                    stmtSql.CommandType = CommandType.StoredProcedure;
                    stmtSql.CommandTimeout = 0;
                    if (parametros != null)
                    {
                        foreach (string key in parametros.Keys)
                        {
                            SqlParameter param = stmtSql.Parameters.AddWithValue(key, parametros[key] ?? DBNull.Value);
                            if (parametros[key] is DataTable)
                            {
                                param.SqlDbType = SqlDbType.Structured;
                            }
                        }
                    }

                    using (SqlDataReader resultSql = stmtSql.ExecuteReader())
                    {
                        if (resultSql.HasRows)
                        {
                            while (resultSql.Read())
                            {
                                int num = resultSql.FieldCount;
                                string resultQuery = string.Empty;
                                for (int i = 0; i < num; i++)
                                    resultQuery += resultSql.GetValue(i).ToString().Replace("|", "") + "|";
                                datos.Add(resultQuery.Substring(0, resultQuery.Length - 1));
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    if (connectionSql.State != ConnectionState.Open)
                        connectionSql.Close();
                    throw;
                }
            }
            return datos;
        }


        /// <summary>
        /// Ejecuta stored procedure de SQL, regresa los objetos en una lista
        /// </summary>
        /// <typeparam name="T">Clase con el nombre de las columnas como propiedades, debe tener contructor vacio</typeparam>
        /// <param name="stringConnectionSql">Cadena de conexion</param>
        /// <param name="sp">Stored Procedure a ejecutar (no poner exec ni parametros)</param>
        /// <param name="parametros"><see cref="Dictionary{TKey, TValue}"/> con el nombre de la variable SQL como llave y el objeto (cadena, numero, fecha o DataTable) que contiene como valor</param>
        /// <returns>Lista con los resultados del stored procedure en objetos de tipo <typeparamref name="T"/></returns>
        public List<T> ejecutaStoredProcedure<T>(string stringConnectionSql, string sp, Dictionary<string, object> parametros = null) where T : class, new()
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection connectionSql = new SqlConnection(stringConnectionSql))
            {
                try
                {
                    connectionSql.Open();
                    stmtSql = new SqlCommand(sp, connectionSql);
                    stmtSql.CommandType = CommandType.StoredProcedure;
                    stmtSql.CommandTimeout = 0;
                    if (parametros != null)
                    {
                        foreach (string key in parametros.Keys)
                        {
                            SqlParameter param = stmtSql.Parameters.AddWithValue(key, parametros[key] ?? DBNull.Value);
                            if (parametros[key] is DataTable)
                            {
                                param.SqlDbType = SqlDbType.Structured;
                            }
                        }
                    }

                    using (SqlDataAdapter adapter = new SqlDataAdapter(stmtSql))
                    {
                        adapter.Fill(dataTable);
                    }

                }
                catch (Exception e)
                {
                    if (connectionSql.State != ConnectionState.Open)
                        connectionSql.Close();
                    throw;
                }
            }
            return dataTable.ToList<T>();
        }

        public string ejecutaSelectValidacion(string stringConnectionSql, string query)
        {
            string res = null;

            using (SqlConnection connectionSql = new SqlConnection(stringConnectionSql))
            {
                try
                {
                    connectionSql.Open();
                    using (SqlCommand stmtSql = new SqlCommand(query, connectionSql))
                    {
                        using (SqlDataReader resultSql = stmtSql.ExecuteReader())
                        {
                            if (resultSql.HasRows)
                            {
                                while (resultSql.Read())
                                {
                                    int num = resultSql.FieldCount;
                                    for (int i = 0; i < num; i++)
                                        res += resultSql.GetValue(i).ToString().Replace("|", "") + "|";
                                    res = res.Substring(0, res.Length - 1);
                                    break;
                                }
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    if (connectionSql.State != ConnectionState.Open)
                        connectionSql.Close();
                    throw;
                }
            }

            return res;
        }

        public DateTime ObtenerFecha(string stringConnectionSql)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection connectionSql = new SqlConnection(stringConnectionSql))
            {
                try
                {
                    connectionSql.Open();
                    using (SqlCommand stmtSql = new SqlCommand("select getdate() Fecha", connectionSql))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(stmtSql))
                        {
                            adapter.Fill(dataTable);
                        }
                    }

                }
                catch (Exception e)
                {
                    if (connectionSql.State != ConnectionState.Open)
                        connectionSql.Close();
                    throw e;
                }
            }

            if (dataTable.Rows.Count > 0)
            {
                return (DateTime)dataTable.Rows[0][0];
            }
            else
            {
                return DateTime.Now;
            }
        }


    }
}
