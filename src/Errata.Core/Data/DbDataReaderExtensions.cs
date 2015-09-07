using System.Data;
using System.Data.Common;

namespace Errata.Data
{
    public static class DbDataReaderExtensions
    {
        public static DataTable DataTable(this DbDataReader reader)
        {
            var fieldCount = reader.FieldCount;
            var dt = new DataTable();
            for (int i = 0; i < fieldCount; i++)
            {
                var name = reader.GetName(i);
                var ft = reader.GetFieldType(i);
                dt.Columns.Add(new DataColumn(name, ft));

            }
            while (reader.Read())
            {
                var row = dt.NewRow();
                for (int i = 0; i < fieldCount; i++)
                {
                    row[i] = reader.GetValue(i);
                }

                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
