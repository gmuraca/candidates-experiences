namespace RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences
{
    public record CandidateExperiencesSalary
    {
        public double Value { get; set; }
        internal CandidateExperiencesSalary(double value)
        {
            Value = value;
        }
        public static CandidateExperiencesSalary Create(double value)
        {
            Validate(value);
            return new CandidateExperiencesSalary(value);
        }
        private static void Validate(double value)
        {
            if (value <= 0)
                throw new ArgumentNullException(nameof(value));
        }
    }
}
