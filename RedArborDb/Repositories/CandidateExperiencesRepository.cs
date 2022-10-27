using Microsoft.EntityFrameworkCore;
using RedArbor.RedArborTest.Domain.Entities;
using RedArbor.RedArborTest.Domain.Repositories;
using RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences;

namespace RedArborDb.Repositories
{
    public class CandidateExperiencesRepository : ICandidateExperiencesRepository
    {
        private RedArborDbContext context;

        public CandidateExperiencesRepository(RedArborDbContext context)
        {
            this.context = context;
        }

        public async Task CreateCandidateExperiences(CandidateExperiences candidateExperience)
        {
            try
            {
                this.context.CandidateExperiences.Add(candidateExperience);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCandidateExperience(CandidateExperiences candidateExperience)
        {
            try
            {
                this.context.CandidateExperiences.Remove(candidateExperience);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CandidateExperiences> GetCandidateExperienceById(CandidateExperiencesIdCandidate value)
        {
            try
            {
                return this.context.CandidateExperiences.Include(x => x.Candidate).FirstOrDefault(x => x.IdCandidateExperience.Equals((int)value));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<CandidateExperiences>> GetExperiences()
        {
            try
            {
                return this.context.CandidateExperiences.Include(x => x.Candidate).OrderBy(x => x.BeginDate.Value).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCandidateExperience(CandidateExperiences candidateExperience)
        {
            try
            {
                this.context.CandidateExperiences.Update(candidateExperience);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
