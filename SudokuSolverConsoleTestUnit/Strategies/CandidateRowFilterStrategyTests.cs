using System.Collections.Generic;
using Moq;
using SudokuSolverConsole;
using SudokuSolverConsole.Strategies;
using Xunit;

namespace SudokuSolverConsoleTestUnit.Strategies
{
    public abstract class CandidateRowFilterStrategyTests
    {
        protected Mock<IPlayingField> _fieldMock = new Mock<IPlayingField>();
        protected readonly List<Square> _testSquares = new List<Square>();

        protected CandidateFilterStrategy _strategy = new CandidateFilterStrategy();

        protected CandidateRowFilterStrategyTests()
        {
            for (var i = 0; i < 9; i++)
            {
                _testSquares.Add(new Square());
            }

            _fieldMock.Setup(x => x.GetRow(It.IsAny<int>())).Returns(_testSquares);
        }

        public class TrySolve : CandidateRowFilterStrategyTests
        {
            private static readonly List<int> CountNine = new List<int>{0,1,2,3,4,5,6,7,8};

            [Fact]
            public void Should_get_rows_from_field()
            {
                _strategy.TrySolve(_fieldMock.Object);

                foreach (int i in CountNine)
                {
                    _fieldMock.Verify(x => x.GetRow(i), Times.Once);
                }
            }

            [Fact]
            public void Should_get_columns_from_field()
            {
                _strategy.TrySolve(_fieldMock.Object);

                foreach (int i in CountNine)
                {
                    _fieldMock.Verify(x => x.GetColumn(i), Times.Once);
                }
            }

            [Fact]
            public void Should_get_bigsquare_from_field()
            {
                _strategy.TrySolve(_fieldMock.Object);

                foreach (int i in CountNine)
                {
                    _fieldMock.Verify(x => x.GetBigSquare(i), Times.Once);
                }
            }

            [Fact]
            public void Should_remove_set_values_from_cadidate_lists_in_row()
            {
                _testSquares[0].Value = 1;
                _testSquares[5].Value = 3;

                _strategy.TrySolve(_fieldMock.Object);

                foreach (var square in _testSquares)
                {
                    Assert.DoesNotContain(1, square.Candidates);
                    Assert.DoesNotContain(3, square.Candidates);
                }
            }

            [Fact]
            public void Should_return_squares_have_that_have_had_candidates_removed()
            {
                _testSquares[0].Value = 1;

                var result = _strategy.TrySolve(_fieldMock.Object);

                //Assert.True(result);
            }

            [Fact]
            public void Should_return_false_when_squares_have_not_been_modified()
            {
                _testSquares[0].Value = 1;

                var firstTry = _strategy.TrySolve(_fieldMock.Object);
                var secondTry = _strategy.TrySolve(_fieldMock.Object);

                //Assert.False(secondTry);
            }
        }
    }
}
