using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class JobRepository : IJobRepository
    {
        public IEnumerable<JobPosting> GetJob()
        => JobDAO.Instance.GetJobs();
    }
}
