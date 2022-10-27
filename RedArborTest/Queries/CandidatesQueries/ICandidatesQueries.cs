using RedArbor.RedArborTest.Domain.Entities;

namespace RedArborTest.Queries.CandidatesQueries
{
    public interface ICandidatesQueries
    {
        Task<Candidates> GetCandidatesById(int id);
        Task<IList<Candidates>> GetCandidates();
    }
}
