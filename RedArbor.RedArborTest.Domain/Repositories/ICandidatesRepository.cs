using RedArbor.RedArborTest.Domain.Entities;
using RedArbor.RedArborTest.Domain.ValueObjects.Candidates;

namespace RedArbor.RedArborTest.Domain.Repositories
{
    public interface ICandidatesRepository
    {
        public Task<IList<Candidates>> GetCandidates();

        public Task CreateCandidate(Candidates candidate);

        public Task<Candidates> GetCandidateById(int value);

        public Task UpdateCandidate(Candidates candidate);

        public Task DeleteCandidate(Candidates candidate);
    }
}
