namespace RedArborTest.Commands.CandidatesCommands
{
    public record CreateCandidateCommand(string Name, string Surname, DateTime Birthdate, string Email);
}
