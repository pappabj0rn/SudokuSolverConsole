using System.Collections.Generic;
using System.Linq;
using Moq;
using SudokuSolverConsole;
using SudokuSolverConsole.Strategies;
using Xunit;

namespace SudokuSolverConsoleTestUnit.Strategies
{
    public abstract class CandidateFilterStrategyTests : StrategyTestsBase
    {
        private readonly SolvingStrategy _strategy = new CandidateFilterStrategy();

        public class TrySolve : CandidateFilterStrategyTests
        {
            [Fact]
            public void Should_get_rows_from_field()
            {
                _strategy.TrySolve(FieldMock.Object);

                foreach (var i in CountNine)
                {
                    FieldMock.Verify(x => x.GetRow(i), Times.Once);
                }
            }

            [Fact]
            public void Should_get_columns_from_field()
            {
                _strategy.TrySolve(FieldMock.Object);

                foreach (var i in CountNine)
                {
                    FieldMock.Verify(x => x.GetColumn(i), Times.Once);
                }
            }

            [Fact]
            public void Should_get_bigsquare_from_field()
            {
                _strategy.TrySolve(FieldMock.Object);

                foreach (var i in CountNine)
                {
                    FieldMock.Verify(x => x.GetBigSquare(i), Times.Once);
                }
            }

            [Fact]
            public void Should_remove_set_values_from_cadidate_lists_in_row()
            {
                Field.Squares[0,0].Value = 1;
                Field.Squares[5,0].Value = 3;

                _strategy.TrySolve(FieldMock.Object);
                
                foreach (var square in Field.GetRow(0).Where(x=>x.Value==0))
                {
                    Assert.DoesNotContain(1, square.Candidates);
                    Assert.DoesNotContain(3, square.Candidates);
                }
            }

            [Fact]
            public void Should_remove_set_values_from_cadidate_lists_in_column()
            {
                Field.Squares[0,0].Value = 1;
                Field.Squares[0,1].Value = 3;

                _strategy.TrySolve(FieldMock.Object);

                foreach (var square in Field.GetColumn(0).Where(x=>x.Value==0))
                {
                    Assert.DoesNotContain(1, square.Candidates);
                    Assert.DoesNotContain(3, square.Candidates);
                }
            }

            [Fact]
            public void Should_remove_set_values_from_cadidate_lists_in_big_square()
            {
                Field.Squares[0,0].Value = 1;
                Field.Squares[0,1].Value = 3;
                Field.Squares[2,2].Value = 5;


                _strategy.TrySolve(FieldMock.Object);

                foreach (var square in Field.GetBigSquare(0).Where(x=>x.Value==0))
                {
                    Assert.DoesNotContain(5, square.Candidates);
                }
            }

            [Fact]
            public void Should_return_squares_that_have_had_candidates_removed()
            {
                Field.Squares[0,0].Value = 1;
                Field.Squares[0,1].Value = 3;
                Field.Squares[2,2].Value = 5;

                var expectedSquares = new List<Square>();
                expectedSquares.AddRange(Field.GetRow(0));
                expectedSquares.AddRange(Field.GetRow(1));
                expectedSquares.AddRange(Field.GetRow(2));
                expectedSquares.AddRange(Field.GetColumn(0));
                expectedSquares.AddRange(Field.GetColumn(2));
                expectedSquares.AddRange(Field.GetBigSquare(0));
                expectedSquares.RemoveAll(x => x.Id == Field.Squares[0,0].Id);
                expectedSquares.RemoveAll(x => x.Id == Field.Squares[0,1].Id);
                expectedSquares.RemoveAll(x => x.Id == Field.Squares[2,2].Id);
                expectedSquares = expectedSquares
                    .Distinct()
                    .OrderBy(s => (int)s.Meta[Square.Keys.X])
                    .ThenBy(s => (int)s.Meta[Square.Keys.Y])
                    .ToList();
                
                var modSquares = _strategy.TrySolve(FieldMock.Object)
                    .OrderBy(s => (int)s.Meta[Square.Keys.X])
                    .ThenBy(s => (int)s.Meta[Square.Keys.Y])
                    .ToList();

                Assert.Equal(expectedSquares,modSquares);
            }

            [Fact]
            public void Should_set_value_for_squares_having_only_one_candidate()
            {
                for (var i = 0; i < 8; i++) // skip last square in row
                {
                    Field.Squares[i,0].Value = i+1;
                }

                _strategy.TrySolve(FieldMock.Object);

                Assert.Equal(9,Field.Squares[8,0].Value);
            }

            [Fact]
            public void Should_itterate_until_done()
            {
                var solvableField = new PlayingField("007000069010906040000302000238790100400030020000260030609000000040080507370019000");

                _strategy.TrySolve(solvableField);

                //6,5 will be the last one solved when running the program manually
                Assert.Equal(9, solvableField.Squares[6, 5].Value);
            }

            [Fact]
            public void Should_stop_itterating_when_unable_to_solve()
            {
                var solvableField = new PlayingField("007000069010906040000302000238790100400030020000260030609000000040080507300000000");

                _strategy.TrySolve(solvableField);

                Assert.Equal(0, solvableField.Squares[6, 5].Value);
            }
        }
    }
}
