namespace RedArbor.RedArborTest.Domain.ValueObjects.Candidates
{
    public record CandidateId
    {
        public int Value { get; set; }
        
        internal CandidateId(int value)
        {
            Value = value;
        }
        public static CandidateId Create(int value)
            => new CandidateId(value);
        
        public static implicit operator int(CandidateId candidateId)
            => candidateId.Value;
    }
}
