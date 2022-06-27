using RadioFEAPI.Models.V1;
using RadioFEAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.Helpers
{
    public class ComparisonHelperTests
    {
        [Fact]
        public void GetSameLength()
        {
            var compObject = new ComparisonObject { Id = 1, Left = "abc", Right = "abc" };
            
            Assert.True(compObject.IsSameLength(), "Strings are not marked as same length while they are");
        }

        [Fact]
        public void GetDifferentLength()
        {
            var compObject = new ComparisonObject { Id = 1, Left = "abc", Right = "ab" };

            Assert.False(compObject.IsSameLength(), "Strings are marked as same length while they are not");
        }

        [Fact]
        public void GetAreSame()
        {
            var compObject = new ComparisonObject { Id = 1, Left = "abc", Right = "abc" };

            Assert.True(compObject.IsEqual(), "Strings are marked different while they are the same");
        }


        [Fact]
        public void GetAreDifferent()
        {
            var compObject = new ComparisonObject { Id = 1, Left = "abc", Right = "abx" };

            Assert.False(compObject.IsEqual(), "Strings are not marked different while they are not the same");
        }

        [Fact]
        public void GetLeftDifferences()
        {
            var compObject = new ComparisonObject { Id = 1, Left = "abc2avt1", Right = "2abcxavt" };

            var differences = compObject.GetLeftDifference();

            var expectedDifferences = new List<Difference>
            {
                new Difference() { ComparedIndex = 1, Index = 0, Length = 3 },
                new Difference() { ComparedIndex = 0, Index = 3, Length = 2 },
                new Difference() { ComparedIndex = 6, Index = 5, Length = 2 },
            };

            Assert.Equal(expectedDifferences, differences);
        }


        [Fact]
        public void GetRightDifferences()
        {
            var compObject = new ComparisonObject { Id = 1, Left = "abc2avt1", Right = "2abcxavt" };

            var differences = compObject.GetRightDifference();

            var expectedDifferences = new List<Difference>
            {
                new Difference() { ComparedIndex = 3, Index = 0, Length = 2 },
                new Difference() { ComparedIndex = 1, Index = 2, Length = 2 },
                new Difference() { ComparedIndex = 4, Index = 5, Length = 3 },
            };

            Assert.Equal(expectedDifferences, differences);
        }
    }
}
