namespace RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences
{
    public record CandidateExperiencesInsertDate
    {
        public DateTime Value { get; set; }
        internal CandidateExperiencesInsertDate(DateTime value)
        {
            Value = value;
        }
        public static CandidateExperiencesInsertDate Create(DateTime value)
            => new CandidateExperiencesInsertDate(value);
    }
}
