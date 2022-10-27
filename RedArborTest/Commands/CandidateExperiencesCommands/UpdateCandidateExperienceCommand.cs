using RedArbor.RedArborTest.Domain.Entities;
using System;

namespace RedArborTest.Commands.CandidateExperiencesCommands
{
    public record UpdateCandidateExperienceCommand(int IdCandidateExperience, int IdCandidate, string Company, string Job, string Description, double Salary, DateTime BeginDate, DateTime? EndDate, Candidates Candidate);
}
