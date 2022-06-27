namespace RadioFEAPI.Models.V1
{
    public class Difference
    {
        public int Index { get; init; }
        public int ComparedIndex { get; init; }
        public int Length { get; init; }

        public override bool Equals(object? obj)
        {
            var objDiff = obj as Difference;
            return obj != null && objDiff.Index == Index && objDiff.ComparedIndex == ComparedIndex && objDiff.Length == Length;
        }
    }
}
