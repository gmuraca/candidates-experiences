namespace RedArbor.RedArborTest.Domain.ValueObjects.CandidateExperiences
{
    public record CandidateExperiencesIdCandidate
    {
        public int Value { get; set; }

        internal CandidateExperiencesIdCandidate(int value)
        {
            Value = value;
        }
        public static CandidateExperiencesIdCandidate Create(int value)
            => new CandidateExperiencesIdCandidate(value);
        public static implicit operator int(CandidateExperiencesIdCandidate candidateId)
            => candidateId.Value;
    }
}
