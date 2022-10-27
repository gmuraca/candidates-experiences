namespace RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences
{
    public record CandidateExperiencesDescription
    {
        public string Value { get; set; }
        internal CandidateExperiencesDescription(string value)
        {
            Value = value;
        }
        public static CandidateExperiencesDescription Create(string value)
        {
            Validate(value);
            return new CandidateExperiencesDescription(value);
        }
        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
        }
    }
}
