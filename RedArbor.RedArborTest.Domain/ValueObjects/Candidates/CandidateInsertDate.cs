namespace RedArbor.RedArborTest.Domain.ValueObjects.Candidates
{
    public record CandidateInsertDate
    {
        public DateTime Value { get; set; }
        internal CandidateInsertDate(DateTime value)
        {
            Value = value;
        }
        public static CandidateInsertDate Create(DateTime value)
            => new CandidateInsertDate(value);
    }
}
