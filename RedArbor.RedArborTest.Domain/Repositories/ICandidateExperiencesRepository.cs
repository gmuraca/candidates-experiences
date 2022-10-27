using RedArbor.RedArborTest.Domain.Entities;
using RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences;

namespace RedArbor.RedArborTest.Domain.Repositories
{
    public interface ICandidateExperiencesRepository
    {
        public Task<IList<CandidateExperiences>> GetExperiences();

        public Task CreateCandidateExperiences(CandidateExperiences candidate);

        public Task<CandidateExperiences> GetCandidateExperienceById(CandidateExperiencesIdCandidate value);

        Task UpdateCandidateExperience(CandidateExperiences candidateExperiences);

        Task DeleteCandidateExperience(CandidateExperiences candidateExperiences);
    }
}
