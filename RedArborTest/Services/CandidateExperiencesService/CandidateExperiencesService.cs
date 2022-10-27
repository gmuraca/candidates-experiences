using RedArbor.RedArborTest.Domain.Entities;
using RedArbor.RedArborTest.Domain.Repositories;
using RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences;
using RedArborTest.Commands.CandidateExperiencesCommands;
using RedArborTest.Queries.CandidateExperiencesQueries;

namespace RedArborTest.Services.CandidateExperiencesService
{
    public class CandidateExperiencesService : ICandidateExperiencesService
    {
        private readonly ICandidateExperiencesRepository repo;
        private readonly ICandidateExperiencesQueries candidateExperiencesQueries;
        public CandidateExperiencesService(ICandidateExperiencesRepository repo, ICandidateExperiencesQueries candidateExperiencesQueries)
        {
            this.repo = repo;
            this.candidateExperiencesQueries = candidateExperiencesQueries;
        }

        public async Task Create(CreateCandidateExperienceCommand candidateExperiences)
        {
            var newCandidateExperience = new CandidateExperiences();
            newCandidateExperience.SetIdCandidate(candidateExperiences.IdCandidate);
            newCandidateExperience.SetCompany(CandidateExperiencesCompany.Create(candidateExperiences.Company));
            newCandidateExperience.SetJob(CandidateExperiencesJob.Create(candidateExperiences.Job));
            newCandidateExperience.SetDescription(CandidateExperiencesDescription.Create(candidateExperiences.Description));
            newCandidateExperience.SetSalary(CandidateExperiencesSalary.Create(candidateExperiences.Salary));
            newCandidateExperience.SetBeginDate(CandidateExperiencesBeginDate.Create(candidateExperiences.BeginDate));
            newCandidateExperience.SetEndDate(CandidateExperiencesEndDate.Create(candidateExperiences.EndDate));
            newCandidateExperience.SetInsertDate(CandidateExperiencesInsertDate.Create(DateTime.Now));

            await this.repo.CreateCandidateExperiences(newCandidateExperience);
        }

        public async Task Delete(int id)
        {
            var candidateExperience = await this.candidateExperiencesQueries.GetCandidateExperiencesById(id);

            if (candidateExperience == null)
                throw new Exception("Not Found");

            await repo.DeleteCandidateExperience(candidateExperience);
        }

        public async Task<CandidateExperiences> GetExperienceById(int id)
        {
            return await this.candidateExperiencesQueries.GetCandidateExperiencesById(id);
        }

        public async Task<IList<CandidateExperiences>> GetExperiences()
        {
            return await this.candidateExperiencesQueries.GetCandidateExperiences();
        }

        public async Task Update(UpdateCandidateExperienceCommand candidateExperiences)
        {
            var updatedCandidateExperience = await this.candidateExperiencesQueries.GetCandidateExperiencesById(candidateExperiences.IdCandidateExperience);

            updatedCandidateExperience.SetCompany(CandidateExperiencesCompany.Create(candidateExperiences.Company));
            updatedCandidateExperience.SetJob(CandidateExperiencesJob.Create(candidateExperiences.Job));
            updatedCandidateExperience.SetDescription(CandidateExperiencesDescription.Create(candidateExperiences.Description));
            updatedCandidateExperience.SetSalary(CandidateExperiencesSalary.Create(candidateExperiences.Salary));
            updatedCandidateExperience.SetBeginDate(CandidateExperiencesBeginDate.Create(candidateExperiences.BeginDate));
            updatedCandidateExperience.SetEndDate(CandidateExperiencesEndDate.Create(candidateExperiences.EndDate));
            updatedCandidateExperience.SetModifyDate(CandidateExperiencesModifyDate.Create(DateTime.Now));

            await repo.UpdateCandidateExperience(updatedCandidateExperience);
        }
    }
}
