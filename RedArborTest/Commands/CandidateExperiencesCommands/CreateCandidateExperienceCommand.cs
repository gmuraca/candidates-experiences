namespace RedArborTest.Commands.CandidateExperiencesCommands
{
    public record CreateCandidateExperienceCommand(int IdCandidate, string Company, string Job, string Description, double Salary, DateTime BeginDate, DateTime? EndDate);
}
