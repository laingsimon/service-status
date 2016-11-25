namespace ServiceStatus
{
    using System;
    using System.Web.Http;
    using Properties;
    using System.Diagnostics;

    public class SchemaVersionController : ApiController
    {
        private readonly SchemaVersionRepository repository;

        public SchemaVersionController()
        {
            this.repository = new SchemaVersionRepository(Settings.Default.ConnectionString);
        }

        [HttpGet]
        public SchemaVersion Index(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            if (!SchemaVersionRepository.IsValidDatabaseName(id))
                return null;

            try
            {
                return repository.GetVersion(id);
            }
            catch (Exception exc)
            {
                ThrowIfLocalRequest(exc);
                return null;
            }
        }

        [Conditional("DEBUG")]
        private void ThrowIfLocalRequest(Exception exc)
        {
            if (Request.RequestUri.IsLoopback)
                return;

            throw exc;
        }
    }
}
