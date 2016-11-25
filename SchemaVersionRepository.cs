namespace ServiceStatus
{
    using System.Data.SqlClient;
    using System.Linq;
    using Dapper;
    using System;
    using System.Text.RegularExpressions;

    internal class SchemaVersionRepository
    {
        private const string DatabaseValidationMatch = @"^[a-z0-9]+$";

        private readonly string connectionString;

        public SchemaVersionRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public static bool IsValidDatabaseName(string databaseName)
        {
            return Regex.IsMatch(databaseName, DatabaseValidationMatch, RegexOptions.IgnoreCase);
        }

        public SchemaVersion GetVersion(string databaseName)
        {
            if (!IsValidDatabaseName(databaseName))
                throw new InvalidOperationException("Potential SQL injection; database name contains invalid characters");

            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();

                return connection.Query<SchemaVersion>(
                    $@"
                    use [{databaseName}]

                    select   MajorSchemaVersion as Major,
                             MinorSchemaVersion as Minor
                    from [Meta].[Version]").SingleOrDefault();
            }
        }
    }
}
