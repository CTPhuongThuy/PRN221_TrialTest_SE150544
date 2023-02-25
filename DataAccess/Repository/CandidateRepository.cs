using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        public void AddCandidate(CandidateProfile candidate)
        => CandidateDAO.Instance.AddNew(candidate);

        public void DeleteCandidate(string id)
        => CandidateDAO.Instance.DeleteCandidate(id);

        public CandidateProfile GetCandidateById(string id)
        => CandidateDAO.Instance.GetCandidateById(id);

        public IEnumerable<CandidateProfile> GetCandidates()
        => CandidateDAO.Instance.GetCandidates();

        public IQueryable<CandidateProfile> GetCandidatesAsQueryable()
        => CandidateDAO.Instance.GetCandidatesAsQueryable();
        public IQueryable<CandidateProfile> SearchCandidatesAsQueryable(string name, DateTime? time)
        => CandidateDAO.Instance.SearchCandidatesAsQueryable(name,  time);

        public void UpdateCandidate(CandidateProfile candidate)
        => CandidateDAO.Instance.UpdateCandidate(candidate);
    }
}
