using RedArbor.RedArborTest.Domain.Entities;
using System.Collections.Generic;

namespace RedArborTest.Queries.CandidateExperiencesQueries
{
    public interface ICandidateExperiencesQueries
    {
        Task<CandidateExperiences> GetCandidateExperiencesById(int id);
        Task<IList<CandidateExperiences>> GetCandidateExperiences();
    }
}
