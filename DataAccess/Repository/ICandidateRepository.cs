using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface ICandidateRepository
    {
        IEnumerable<CandidateProfile> GetCandidates();
        IQueryable<CandidateProfile> GetCandidatesAsQueryable();
        IQueryable<CandidateProfile> SearchCandidatesAsQueryable(string name, DateTime? time);
        CandidateProfile GetCandidateById(string id);
        void AddCandidate(CandidateProfile candidate);  
        void UpdateCandidate(CandidateProfile candidate);
        void DeleteCandidate(string id);
    }
}
