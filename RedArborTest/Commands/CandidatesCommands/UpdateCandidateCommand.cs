namespace RedArborTest.Commands.CandidatesCommands
{
    public record UpdateCandidateCommand(int IdCandidate, string Name, string Surname, DateTime Birthdate, string Email);
}
