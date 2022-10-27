using RedArbor.RedArborTest.Domain.Entities;
using RedArbor.RedArborTest.Domain.Repositories;
using RedArbor.RedArborTest.Domain.ValueObjects.Candidates;
using RedArborTest.Commands.CandidatesCommands;
using RedArborTest.Queries.CandidatesQueries;

namespace RedArborTest.Services.CandidatesService
{
    public class CandidatesService : ICandidatesService
    {
        private readonly ICandidatesRepository repo;
        private readonly ICandidatesQueries candidateQueries;
        public CandidatesService(ICandidatesRepository repo, ICandidatesQueries candidateQueries)
        {
            this.repo = repo;
            this.candidateQueries = candidateQueries;
        }

        public async Task<Candidates> GetCandidate(int id)
        {
            return await this.candidateQueries.GetCandidatesById(id);
        }

        public async Task<IList<Candidates>> GetCandidates()
        {
            return await this.candidateQueries.GetCandidates();
        }

        public async Task Create(Candidates candidate)
        {
            await this.repo.CreateCandidate(candidate);
        }

        public async Task Delete(int id)
        {
            var candidate = await this.candidateQueries.GetCandidatesById(id);
            await repo.DeleteCandidate(candidate);
        }

        public async Task Update(Candidates candidate)
        {
            candidate.SetModifyDate(CandidateModifyDate.Create(DateTime.Now));
            await repo.UpdateCandidate(candidate);
        }

        public async Task Create(CreateCandidateCommand model)
        {
            var newCandidate = new Candidates();
            newCandidate.SetName(CandidateName.Create(model.Name));
            newCandidate.SetSurname(CandidateSurname.Create(model.Surname));
            newCandidate.SetEmail(CandidateEmail.Create(model.Email));
            newCandidate.SetBirthdate(CandidateBirthdate.Create(model.Birthdate));
            newCandidate.SetInsertDate(CandidateInsertDate.Create(DateTime.Now));

            await this.repo.CreateCandidate(newCandidate);
        }

        public async Task Update(UpdateCandidateCommand model)
        {
            var updatedCandidate = await this.repo.GetCandidateById(model.IdCandidate);

            updatedCandidate.SetName(CandidateName.Create(model.Name));
            updatedCandidate.SetSurname(CandidateSurname.Create(model.Surname));
            updatedCandidate.SetEmail(CandidateEmail.Create(model.Email));
            updatedCandidate.SetBirthdate(CandidateBirthdate.Create(model.Birthdate));
            updatedCandidate.SetModifyDate(CandidateModifyDate.Create(DateTime.Now));

            await this.repo.UpdateCandidate(updatedCandidate);
        }
    }
}
