namespace API.DTOs
{
    public class SearchDTO
    {
        public string? Field { get; set; }
        public string? Value { get; set; }
        public SearchEvaluationTypes? EvaluationType { get; set; }
    }

    public enum SearchEvaluationTypes
    {
        Equals = 0,
        NotEquals = 1,
        Contains = 2,
        GreaterThan = 3,
        LessThan = 4
    }
}
