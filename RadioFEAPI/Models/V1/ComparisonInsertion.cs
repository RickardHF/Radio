namespace RadioFEAPI.Models.V1
{
    public enum SideToSet
    {
        Left = 0,
        Right = 1,
    }
    public class ComparisonInsertion
    {
        public int Id { get; init; }
        public string Input { get; init; }
        public SideToSet Side { get; init; }
    }
}
