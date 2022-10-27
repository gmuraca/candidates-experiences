using RedArbor.RedArborTest.Domain.Repositories;
using RedArbor.RedArborTest.Domain.Entities;
using RedArbor.RedArborTest.Domain.ValueObjects.Candidates;

namespace RedArborTest.Queries.CandidatesQueries
{
    public class CandidatesQueries : ICandidatesQueries
    {
        private readonly ICandidatesRepository repo;

        public CandidatesQueries(ICandidatesRepository repo)
        {
            this.repo = repo;
        }

        public async Task<IList<Candidates>> GetCandidates()
        {
            return await this.repo.GetCandidates();
        }

        public async Task<Candidates> GetCandidatesById(int id)
        {
            return await this.repo.GetCandidateById(CandidateId.Create(id));
        }
    }
}
