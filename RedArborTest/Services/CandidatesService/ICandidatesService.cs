using RedArbor.RedArborTest.Domain.Entities;
using RedArborTest.Commands.CandidatesCommands;

namespace RedArborTest.Services.CandidatesService
{
    public interface ICandidatesService
    {
        Task<IList<Candidates>> GetCandidates();
        
        Task<Candidates> GetCandidate(int id);

        Task Delete(int id);

        Task Create(CreateCandidateCommand model);

        Task Update(UpdateCandidateCommand model);
    }
}
