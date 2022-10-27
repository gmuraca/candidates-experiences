using RedArbor.RedArborTest.Domain.Entities;
using RedArbor.RedArborTest.Domain.Repositories;

namespace RedArborDb.Repositories
{
    public class CandidatesRepository : ICandidatesRepository
    {
        private RedArborDbContext context;

        public CandidatesRepository(RedArborDbContext context)
        {
            this.context = context;
        }

        public async Task CreateCandidate(Candidates candidate)
        {
            try
            {
                this.context.Candidates.Add(candidate);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCandidate(Candidates candidate)
        {
            try
            {
                this.context.Candidates.Remove(candidate);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Candidates> GetCandidateById(int value)
        {
            try
            {
                return await this.context.Candidates.FindAsync(value);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IList<Candidates>> GetCandidates()
        {
            try
            {
                return this.context.Candidates.OrderBy(x => x.Name.Value).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCandidate(Candidates candidate)
        {
            try
            {
                this.context.Candidates.Update(candidate);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
