using RedArbor.RedArborTest.Domain.Entities;
using RedArborTest.Commands.CandidateExperiencesCommands;

namespace RedArborTest.Services.CandidateExperiencesService
{
    public interface ICandidateExperiencesService
    {
        Task<IList<CandidateExperiences>> GetExperiences();

        Task<CandidateExperiences> GetExperienceById(int id);

        Task Create(CreateCandidateExperienceCommand candidateExperience);

        Task Update(UpdateCandidateExperienceCommand candidateExperience);

        Task Delete(int id);
    }
}
