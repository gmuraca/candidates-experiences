namespace RedArbor.RedArborTest.Domain.ValueObjects.Candidates
{
    public record CandidateBirthdate
    {
        public DateTime Value { get; set; }
        internal CandidateBirthdate(DateTime value)
        {
            Value = value;
        }
        public static CandidateBirthdate Create(DateTime value)
        {
            Validate(value);
            return new CandidateBirthdate(value);
        }
        private static void Validate(DateTime value)
        {
            if (value >= DateTime.Today)
                throw new ArgumentNullException(nameof(value));
        }
    }
}
