using RedArbor.RedArborTest.Domain.Entities;
using RedArbor.RedArborTest.Domain.Repositories;
using RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences;

namespace RedArborTest.Queries.CandidateExperiencesQueries
{
    public class CandidateExperiencesQueries : ICandidateExperiencesQueries
    {
        private readonly ICandidateExperiencesRepository repo;
        public CandidateExperiencesQueries(ICandidateExperiencesRepository repo)
        {
            this.repo = repo;
        }
        public async Task<IList<CandidateExperiences>> GetCandidateExperiences()
        {
            return await this.repo.GetExperiences();
        }

        public async Task<CandidateExperiences> GetCandidateExperiencesById(int id)
        {
            return await this.repo.GetCandidateExperienceById(CandidateExperiencesIdCandidate.Create(id));   
        }
    }
}
