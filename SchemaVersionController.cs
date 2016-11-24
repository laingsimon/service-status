namespace ServiceStatus
{
    using System.Web.Http;
    using Properties;

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

            return repository.GetVersion(id);
        }
    }
}
