namespace RadioFEAPI.Models.V1
{
    public class ComparisonResult
    {
        public string Message { get; set; }
        public IEnumerable<Difference> DifferencesLeft { get; set; }
        public IEnumerable<Difference> DifferencesRight { get; set; }

    }
}
