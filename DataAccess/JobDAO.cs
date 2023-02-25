using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class JobDAO
    {
        private static JobDAO instance = null;
        private static readonly object instanceLock = new object();
        private JobDAO() { }
        public static JobDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new JobDAO();
                    }
                    return instance;
                }
            }

        }
        public IEnumerable<JobPosting> GetJobs()
        {
            var jobs = new List<JobPosting>();
            try
            {
                using var context = new CandidateManagementContext();
                jobs = context.JobPostings.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return jobs;
        }
    }
}
