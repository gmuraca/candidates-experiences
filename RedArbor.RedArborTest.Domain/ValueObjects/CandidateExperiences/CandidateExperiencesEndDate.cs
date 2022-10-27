namespace RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences
{
    public record CandidateExperiencesEndDate
    {
        public DateTime? Value { get; set; }
        internal CandidateExperiencesEndDate(DateTime? value)
        {
            Value = value;
        }
        public static CandidateExperiencesEndDate Create(DateTime? value)
            => new CandidateExperiencesEndDate(value);
    }
}
