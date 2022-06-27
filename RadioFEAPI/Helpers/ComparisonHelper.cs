namespace RadioFEAPI.Helpers
{
    public static class ComparisonHelper
    {
        public static bool IsSameLength(this ComparisonObject obj)
        {
            // Checks if both sides are of same length
            return obj.Left?.Length == obj.Right?.Length;
        }

        public static bool IsEqual(this ComparisonObject obj)
        {
            // Checks if both contents are the same
            return obj.Left.Equals(obj.Right);
        }

        public static IEnumerable<Difference> GetLeftDifference(this ComparisonObject obj)
        {
            // Return result of difference check
            return GetDifferences(obj.Left, obj.Right);
        }


        public static IEnumerable<Difference> GetRightDifference(this ComparisonObject obj)
        {
            // Returns result of inverted difference check
            return GetDifferences(obj.Right, obj.Left);
        }

        private static IEnumerable<Difference> GetDifferences(string left, string right)
        {
            // We are here only interested in the biggest sections that are alike. We try to exclude the subsets
            // If the length is one or less we don't count it, as probability of two same letters is very high and doesn't give any real insight

            // Create list for storing differences
            var diffs = new List<Difference>();

            // Set initial index
            var leftIndex = 0;

            // While we still have unchecked chars
            while (leftIndex < left.Length)
            {
                // Set length to one to get at least on char
                var length = 1;

                // Set initial possible list of indexes on the comparison to check
                var possibleRights = Enumerable.Range(0, right.Length).ToList();

                // While we still have possible offsets to check
                while (possibleRights.Any())
                {

                    var filteredIndexes = new List<int>();

                    // Filter ofsets to where the substring match our current substring from the left side. Also check that we don't go out of bounds
                    filteredIndexes.AddRange(possibleRights.Where(rightIndex => 
                        leftIndex + length <= left.Length 
                        && rightIndex + length <= right.Length 
                        && right.Substring(rightIndex, length) == left.Substring(leftIndex, length))
                    );

                    // Update the possibles with the filtered ones to get a updated version of the current possible matches and increase the length
                    if (filteredIndexes.Any())
                    {
                        possibleRights = filteredIndexes;
                        length++;
                    }
                    // No match for this length was set -> set length to previous value which had at least one match and breaks 
                    else
                    {
                        length--;
                        break;
                    }
                }

                // If the length is one or less then we just break as it's not needed to count
                if (length <= 1)
                {
                    leftIndex++;
                    continue;
                };

                // Add the offsets found
                diffs.AddRange(possibleRights.Select(x => new Difference { Index = leftIndex, ComparedIndex = x, Length = length }));

                // Increase the index with the length of the found match(es) so that we don't look up a subset of what we have already found a match for
                leftIndex += length;
            }

            // Return the overview of differences
            return diffs.Distinct();
        }
    }
}
