namespace RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences
{
    public record CandidateExperiencesModifyDate
    {
        public DateTime? Value { get; set; }
        internal CandidateExperiencesModifyDate(DateTime? value)
        {
            Value = value;
        }
        public static CandidateExperiencesModifyDate Create(DateTime? value)
            => new CandidateExperiencesModifyDate(value);
    }
}
