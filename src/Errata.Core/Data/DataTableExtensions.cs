using System;
using System.Collections.Generic;
using System.Data;

namespace Errata.Data
{
    public static class DataTableExtensions
    {
        public static bool IsSameAs(this DataTable dataTable, DataTable otherDataTable)
        {
            if (dataTable.Rows.Count != otherDataTable.Rows.Count
                || dataTable.Columns.Count != otherDataTable.Columns.Count)
                return false;

            for (int iRow = 0; iRow < dataTable.Rows.Count; iRow++)
            {
                var row = dataTable.Rows[iRow];
                var otherRow = otherDataTable.Rows[iRow];
                for (var iCol = 0; iCol < dataTable.Columns.Count; iCol++)
                {
                    if (!ValuesMatch(dataTable.Columns[iCol], row[iCol], otherRow[iCol]))
                        return false;
                }
            }
            return true;
        }

        private static bool ValuesMatch(DataColumn column, object value1, object value2)
        {
            if (column.DataType == typeof(double))
            {
                return Math.Abs((double)value1 - (double)value2) < .00000001;
            }

            if (column.DataType == typeof(Int32))
            {
                return (int)value1 == (int)value2;
            }
            return false;
        }

        public static HashSet<T> UniqueValueHash<T>(this DataTable dataTable, string keyColumnName)
        {
            HashSet<T> values = new HashSet<T>();
            foreach (DataRow row in dataTable.Rows)
                values.Add(row.Field<T>(keyColumnName));

            return values;
        }

        public static DataTable RowsOfValue<T>(this DataTable dataTable, string keyColumnName, IEnumerable<T> values)
        {
            var hashedValues = new HashSet<T>(values);
            var filtered = dataTable.Clone();
            foreach (DataRow  row in dataTable.Rows)
            {
                if (hashedValues.Contains(row.Field<T>(keyColumnName)))
                {
                    var clonedRow = filtered.NewRow();
                    clonedRow.ItemArray = (object[]) row.ItemArray.Clone() ;
                    filtered.Rows.Add(clonedRow);
                }
            }
            return filtered;
        }
    }
}
