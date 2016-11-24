using System.Web.Http;

namespace ServiceStatus
{
    public class WindowsServiceController : ApiController
    {
        private readonly ServiceRepository _repository;

        public WindowsServiceController()
        {
            _repository = new ServiceRepository();
        }

        [HttpGet]
        public Service Index(string id)
        {
            return _repository.GetStatus(id);
        }
    }
}
