namespace RadioFETestApp.Model
{
    public class DiffResult
    {
        public string Message { get; set; }
        public IEnumerable<Difference> DifferencesLeft { get; set; }
        public IEnumerable<Difference> DifferencesRight { get; set; }
    }
}
