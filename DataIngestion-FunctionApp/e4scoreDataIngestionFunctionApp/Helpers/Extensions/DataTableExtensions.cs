using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Helpers.Extensions
{
    public static class DataTableExtensions
    {
        public static List<T> ConvertToList<T>(this DataTable dataTable)
        {
            List<T> resultList = new List<T>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                T listItem = GetListItem<T>(dataRow);
                resultList.Add(listItem);
            }
            return resultList;
        }
        private static T GetListItem<T>(DataRow dataRow)
        {
            Type tempType = typeof(T);
            PropertyInfo[] propertyInfos = tempType.GetProperties();
            T resultObject = Activator.CreateInstance<T>();

            foreach (DataColumn column in dataRow.Table.Columns)
            {
                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    try
                    {

                        if (propertyInfo.Name == column.ColumnName)
                        {
                            var value = (dataRow[propertyInfo.Name] == DBNull.Value) ? null : dataRow[propertyInfo.Name];
                            propertyInfo.SetValue(resultObject, value);
                        }
                        else
                            continue;
                    }
                    catch
                    {
                        propertyInfo.SetValue(resultObject, GetDefaultValue(propertyInfo.PropertyType), null);
                    }
                }
            }
            return resultObject;
        }
        private static object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);

            return null;
        }
        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }

}
