namespace RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences
{
    public record CandidateExperiencesJob
    {
        public string Value { get; set; }
        internal CandidateExperiencesJob(string value)
        {
            Value = value;
        }
        public static CandidateExperiencesJob Create(string value)
        {
            Validate(value);
            return new CandidateExperiencesJob(value);
        }
        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
        }
    }
}
