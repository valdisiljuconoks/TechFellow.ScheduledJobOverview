using System.Collections.Generic;
using System.Web.Http;

namespace TechFellow.ScheduledJobOverview.Controllers
{
    [Authorize]
    public class JobInfoController : ApiController
    {
        public IEnumerable<Models.JobDescriptionViewModel> GetList()
        {
            var repository = new JobRepository();
            return repository.GetList();
        }
    }
}
