using System.Collections.Generic;
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


        public static Dictionary<TK, TV> Dictionary<TK, TV>(this DbDataReader reader, string keyColumn, string valueColumn)
        {
            var keyPos = reader.GetOrdinal(keyColumn);
            var valuePos = reader.GetOrdinal(valueColumn);
            return reader.Dictionary<TK, TV>(keyPos, valuePos);
        }


        public static Dictionary<TK, TV> Dictionary<TK, TV>(this DbDataReader reader, int keyColumnPos, int valueColumnPos)
        {
            var dict = new Dictionary<TK, TV>();
            while (reader.Read())
                dict[(TK)reader.GetValue(keyColumnPos)] = (TV)reader.GetValue(valueColumnPos);

            return dict;
        }

    }
}
