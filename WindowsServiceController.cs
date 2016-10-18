using System;
using System.Web.Http;

namespace ServiceStatus
{
    public class WindowsServiceController : ApiController
    {
        private readonly ServiceRepository _repository;

        public WindowsServiceController()
        {
            Console.WriteLine("Controller created");
            _repository = new ServiceRepository();
        }

        [HttpGet]
        public Service Status(string id)
        {
            Console.WriteLine("Action called: " + id);

            return _repository.GetStatus(id);
        }
    }
}
