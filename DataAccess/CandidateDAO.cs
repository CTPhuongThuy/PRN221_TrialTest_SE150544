using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    class CandidateDAO
    {
        private static CandidateDAO instance = null;
        private static readonly object instanceLock = new object();
        private CandidateDAO() { }
        public static CandidateDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CandidateDAO();
                    }
                    return instance;
                }
            }

        }

        public IEnumerable<CandidateProfile> GetCandidates()
        {
            var candidates = new List<CandidateProfile>();
            try
            {
                using var context = new CandidateManagementContext();
                candidates = context.CandidateProfiles.Include(c => c.Posting).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return candidates;
        }

        public CandidateProfile GetCandidateById(string id)
        {
            CandidateProfile candidate = null;
            try
            {
                using var context = new CandidateManagementContext();
                candidate = context.CandidateProfiles.Include(c => c.Posting).SingleOrDefault(a => a.CandidateId.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return candidate;
        }
       
        public void AddNew(CandidateProfile candidate)
        {
            try
            {
                CandidateProfile c = GetCandidateById(candidate.CandidateId);
                if (c == null)
                {
                    using var context = new CandidateManagementContext();
                    context.CandidateProfiles.Add(candidate);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This candidate ID is existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateCandidate(CandidateProfile candidate)
        {
            try
            {
                CandidateProfile c = GetCandidateById(candidate.CandidateId);
                if (c != null)
                {
                    using var context = new CandidateManagementContext();
                    context.CandidateProfiles.Update(candidate);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This candidate ID is not existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteCandidate (string id)
        {
            try
            {
                CandidateProfile c = GetCandidateById(id);
                if (c != null)
                {
                    using var context = new CandidateManagementContext();
                    context.CandidateProfiles.Remove(c);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("This candidate ID is not existed!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal IQueryable<CandidateProfile> GetCandidatesAsQueryable()
        {
            IQueryable<CandidateProfile> candidates;
            try
            {
                using var context = new CandidateManagementContext();
                candidates = context.CandidateProfiles
                    .Include(c => c.Posting)
                    .ToList()
                    .AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return candidates;
        }

        internal IQueryable<CandidateProfile> SearchCandidatesAsQueryable(string name, DateTime? date)
        {
            IQueryable<CandidateProfile> candidates;
            try
            {
                using var context = new CandidateManagementContext();
                candidates = context.CandidateProfiles
                    .Where(candidate => candidate.Fullname.ToLower().Contains(name.ToLower()) || date == candidate.Birthday).ToList().AsQueryable();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return candidates;
        }
    }
}
