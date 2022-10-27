namespace RedArbor.RedArborTest.Domain.ValueObjects.Candidates
{
    public record CandidateModifyDate
    {
        public DateTime? Value { get; set; }
        internal CandidateModifyDate(DateTime? value)
        {
            Value = value;
        }
        public static CandidateModifyDate Create(DateTime value)
            => new CandidateModifyDate(value);
    }
}
