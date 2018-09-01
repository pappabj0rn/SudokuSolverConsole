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

        protected CandidateRowFilterStrategy _strategy = new CandidateRowFilterStrategy();

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
            [Fact]
            public void Should_get_rows_from_field()
            {
                _strategy.TrySolve(_fieldMock.Object);

                _fieldMock.Verify(x => x.GetRow(It.IsAny<int>()), Times.Exactly(9));
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
            public void Should_return_true_when_squares_have_been_modified()
            {
                _testSquares[0].Value = 1;

                var result = _strategy.TrySolve(_fieldMock.Object);

                Assert.True(result);
            }

            [Fact]
            public void Should_return_false_when_squares_have_not_been_modified()
            {
                _testSquares[0].Value = 1;

                var firstTry = _strategy.TrySolve(_fieldMock.Object);
                var secondTry = _strategy.TrySolve(_fieldMock.Object);

                Assert.False(secondTry);
            }
        }
    }
}
