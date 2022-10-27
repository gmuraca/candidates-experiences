namespace RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences
{
    public record CandidateExperiencesCompany
    {
        public string Value { get; set; }
        internal CandidateExperiencesCompany(string value)
        {
            Value = value;
        }
        public static CandidateExperiencesCompany Create(string value)
        {
            Validate(value);
            return new CandidateExperiencesCompany(value);
        }
        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
        }
    }
}
