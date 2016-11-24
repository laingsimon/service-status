namespace ServiceStatus
{
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;

    internal class SchemaVersionRepository
    {
        private readonly string connectionString;

        public SchemaVersionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public SchemaVersion GetVersion(string databaseName)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                return connection.Query<SchemaVersion>(
                    $"select MajorSchemaVersion as Major, MinorSchemaVersion as Minor from [{databaseName}].[Meta].[Version]").SingleOrDefault();
            }
        }
    }
}
