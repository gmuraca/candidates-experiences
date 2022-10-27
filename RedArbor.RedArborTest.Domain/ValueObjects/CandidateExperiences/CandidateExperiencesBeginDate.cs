namespace RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences
{
    public record CandidateExperiencesBeginDate
    {
        public DateTime Value { get; set; }
        internal CandidateExperiencesBeginDate(DateTime value)
        {
            Value = value;
        }
        public static CandidateExperiencesBeginDate Create(DateTime value)
        {
            Validate(value);
            return new CandidateExperiencesBeginDate(value);
        }
        private static void Validate(DateTime value)
        {
            if (value >= DateTime.Today)
                throw new ArgumentNullException(nameof(value));
        }
    }
}
