using System.Text.RegularExpressions;

namespace RedArbor.RedArborTest.Domain.ValueObjects.Candidates
{
    public record CandidateEmail
    {
        public string Value { get; set; }
        internal CandidateEmail(string value)
        {
            Value = value;
        }
        public static CandidateEmail Create(string value)
        {
            Validate(value);
            return new CandidateEmail(value);
        }
        private static void Validate(string value)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (string.IsNullOrEmpty(value) || !(regex.Match(value)).Success)
                throw new ArgumentNullException(nameof(value));
        }
    }
}
