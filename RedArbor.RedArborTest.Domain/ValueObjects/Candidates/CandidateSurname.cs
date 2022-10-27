namespace RedArbor.RedArborTest.Domain.ValueObjects.Candidates
{
    public record CandidateSurname
    {
        public string Value { get; set; }
        internal CandidateSurname(string value)
        {
            Value = value;
        }
        public static CandidateSurname Create(string value)
        {
            Validate(value);
            return new CandidateSurname(value);
        }
        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
        }
    }
}
