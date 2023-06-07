using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace Lax.Data.Sql.SqlServer {

    public static class SqlServerInsertionHelpers {

        public static async Task BulkInsertRows<TModel>(
            this SqlConnection db,
            IEnumerable<TModel> models,
            string tableName,
            CancellationToken cancellationToken = default) {

            var modelType = typeof(TModel);

            var dt = new DataTable();

            var bulkCopy = new SqlBulkCopy(db) { DestinationTableName = tableName };

            foreach (var property in modelType.GetProperties()) {

                dt.Columns.Add(new DataColumn(property.Name, property.PropertyType));
                bulkCopy.ColumnMappings.Add(property.Name, property.Name);

            }

            foreach (var model in models) {

                var dr = dt.NewRow();

                foreach (var property in modelType.GetProperties()) {

                    dr[property.Name] = property.GetValue(model);

                }

                dt.Rows.Add(dr);

            }

            await bulkCopy.WriteToServerAsync(dt, cancellationToken);

        }

    }

}