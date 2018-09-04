using System;
using SudokuSolverConsole;
using Xunit;

namespace SudokuSolverConsoleTestUnit
{
    public abstract class SquareTests
    {
        public class Constructor : SquareTests
        {
            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            public void Should_set_value_to_given_value(int value)
            {
                var square = new Square(value);
                Assert.Equal(value,square.Value);
            }

            [Theory]
            [InlineData(0,false)]
            [InlineData(1,true)]
            public void Should_set_Given_when_initialised_with_value_above_zero(int value, bool expectedGiven)
            {
                var square = new Square(value);
                Assert.Equal(expectedGiven,square.Given);
            }

            [Theory]
            [InlineData(0, new[] {1, 2, 3, 4, 5, 6, 7, 8, 9})]
            [InlineData(1, new int[] {})]
            public void Should_set_candidates_depending_on_value(int value, int[] candidates)
            {
                var square = new Square(value);

                foreach (int candidate in candidates)
                {
                    Assert.Contains(candidate,square.Candidates);
                }
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(10)]
            public void Should_treat_values_out_of_sudoku_range_as_zero(int value)
            {
                var square = new Square(value);

                Assert.Equal(0,square.Value);
                Assert.False(square.Given);
            }

            [Fact]
            public void Should_set_id()
            {
                var square = new Square();

                Assert.NotEqual(Guid.Empty,square.Id);
            }
        }

        public class Value : SquareTests
        {
            [Theory]
            [InlineData(0,false)]
            [InlineData(1,true)]
            public void Should_lock_for_modification_when_given(int value, bool locked)
            {
                var square = new Square(value);

                if (locked)
                    Assert.Throws<AccessViolationException>(() => square.Value = value);
                else
                {
                    var newValue = 5;
                    square.Value = newValue;
                    Assert.Equal(newValue,square.Value);
                }
            }
        }
    }
}