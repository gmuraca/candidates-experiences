namespace RedArbor.RedArborTest.Domain.ValueObjects.Candidates
{
    public record CandidateName
    { 
        public string Value { get; set; }
        internal CandidateName(string value)
        {
            Value = value;
        }
        public static CandidateName Create(string value)
        {
            Validate(value);
            return new CandidateName(value);
        }
        private static void Validate(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException(nameof(value));
        }
    }
}
