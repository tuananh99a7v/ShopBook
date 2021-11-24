using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace UniLibrary.Helper
{
    public static class DataTableHelper
    {
        public static DataTable ListToDataTable<T>(IEnumerable<T> data) where T : class
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }
            foreach (T item in data)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static IEnumerable<T> DataTableToList<T>(DataTable table) where T : class, new()
        {
            List<T> list = new List<T>();
            foreach (var row in table.AsEnumerable())
            {
                T obj = new T();
                foreach (var prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                        propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType), null);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                list.Add(obj);
            }
            return list;
        }
    }
}