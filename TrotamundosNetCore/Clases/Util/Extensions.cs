using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TrotamundosNetCore.Clases.Util
{
    public static class Extensions
    {

        #region BD
        /// <summary>
        /// Convierte json a DataTable
        /// </summary>
        /// <typeparam name="T">Clase para datatable</typeparam>
        /// <param name="json">Cadena que contiene el json a convertir</param>
        /// <returns>Datatable con los datos del json con las propiedades de la clase <typeparamref name="T"/></returns>
        public static DataTable JsonToDataTable<T>(this string json) where T : class, new()
        {
            return ListToDataTable(JsonConvert.DeserializeObject<List<T>>(json));
        }

        /// <summary>
        /// Convierte Lista en DataTable
        /// </summary>
        /// <typeparam name="T">Clase para datatable</typeparam>
        /// <param name="obj">Lista de objetos a colocar a la datatable</param>
        /// <returns>Datatable con los datos de los objetos <typeparamref name="T"/> de la lista</returns>
        public static DataTable ToDataTable<T>(this List<T> obj) where T : class, new()
        {
            return ListToDataTable(obj);
        }

        /// <summary>
        /// Convierte datatable en lista
        /// </summary>
        /// <typeparam name="T">Clase para objetos de la lista</typeparam>
        /// <param name="dataTable">Datatable a convertir</param>
        /// <returns>Lista con elementos de la clase <typeparamref name="T"/></returns>
        public static List<T> ToList<T>(this DataTable dataTable) where T : class, new()
        {
            List<T> lista = DataTableToList<T>(dataTable);
            if (lista == null)
                return new List<T>();
            return lista;
        }

        #endregion

        #region Metodos privados

        private static DataTable ListToDataTable<T>(IList<T> data = null!)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        private static List<T> DataTableToList<T>(DataTable table) where T : class, new()
        {
            if (table != null && table.Rows.Count > 0)
            {
                List<T> data = new List<T>();
                foreach (DataRow row in table.Rows)
                {
                    T item = GetItem<T>(row);
                    data.Add(item);
                }

                return data;
            }
            else
            {
                return new List<T>();
            }
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if ((pro.Name.ToUpper() == column.ColumnName.ToUpper()) || (pro.Name.ToUpper() == (column.ColumnName.ToUpper() + "1")))
                    {
                        if (dr[column.ColumnName] != DBNull.Value)
                        {
                            pro.SetValue(obj, dr[column.ColumnName]);
                        }
                        break;
                    }
                }
            }
            return obj;
        }

        #endregion
    }
}
