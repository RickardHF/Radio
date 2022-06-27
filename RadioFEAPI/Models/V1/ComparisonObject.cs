namespace RadioFEAPI.Models.V1
{
    public record ComparisonObject
    {
        public int Id { get; init; }
        public string Left { get; set; }
        public string Right { get; set; }
    }
}
