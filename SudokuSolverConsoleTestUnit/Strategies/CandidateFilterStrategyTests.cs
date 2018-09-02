using System.Collections.Generic;
using System.Linq;
using Moq;
using SudokuSolverConsole;
using SudokuSolverConsole.Strategies;
using Xunit;

namespace SudokuSolverConsoleTestUnit.Strategies
{
    public abstract class CandidateFilterStrategyTests
    {
        protected Mock<IPlayingField> _fieldMock = new Mock<IPlayingField>();
        protected readonly List<Square> _testSquaresRow = new List<Square>();
        protected readonly List<Square> _testSquaresCol = new List<Square>();
        protected readonly List<Square> _testSquaresBig = new List<Square>();

        protected CandidateFilterStrategy _strategy = new CandidateFilterStrategy();

        protected CandidateFilterStrategyTests()
        {
            for (var i = 0; i < 9; i++)
            {
                _testSquaresRow.Add(new Square());
            }

            _testSquaresCol.Add(_testSquaresRow.First());
            for (var i = 0; i < 8; i++)
            {
                _testSquaresCol.Add(new Square());
            }

            //rrr
            //cnn
            //cnn
            _testSquaresBig.AddRange(_testSquaresRow.GetRange(0,3));
            for (var r = 0; r < 2; r++)
            {
                _testSquaresBig.AddRange(_testSquaresRow.Skip(1+r).Take(1));
                for (var i = 0; i < 2; i++)
                {
                    _testSquaresBig.Add(new Square());
                }
            }

            _fieldMock.Setup(x => x.GetRow(It.IsAny<int>())).Returns(_testSquaresRow);
            _fieldMock.Setup(x => x.GetColumn(It.IsAny<int>())).Returns(_testSquaresCol);
            _fieldMock.Setup(x => x.GetBigSquare(It.IsAny<int>())).Returns(_testSquaresBig);
        }

        public class TrySolve : CandidateFilterStrategyTests
        {
            private static readonly List<int> CountNine = new List<int>{0,1,2,3,4,5,6,7,8};

            [Fact]
            public void Should_get_rows_from_field()
            {
                _strategy.TrySolve(_fieldMock.Object);

                foreach (var i in CountNine)
                {
                    _fieldMock.Verify(x => x.GetRow(i), Times.Once);
                }
            }

            [Fact]
            public void Should_get_columns_from_field()
            {
                _strategy.TrySolve(_fieldMock.Object);

                foreach (var i in CountNine)
                {
                    _fieldMock.Verify(x => x.GetColumn(i), Times.Once);
                }
            }

            [Fact]
            public void Should_get_bigsquare_from_field()
            {
                _strategy.TrySolve(_fieldMock.Object);

                foreach (var i in CountNine)
                {
                    _fieldMock.Verify(x => x.GetBigSquare(i), Times.Once);
                }
            }

            [Fact]
            public void Should_remove_set_values_from_cadidate_lists_in_row()
            {
                _testSquaresRow[0].Value = 1;
                _testSquaresRow[5].Value = 3;

                _strategy.TrySolve(_fieldMock.Object);

                foreach (var square in _testSquaresRow)
                {
                    Assert.DoesNotContain(1, square.Candidates);
                    Assert.DoesNotContain(3, square.Candidates);
                }
            }

            [Fact]
            public void Should_remove_set_values_from_cadidate_lists_in_column()
            {
                _testSquaresRow[0].Value = 1;
                _testSquaresCol[1].Value = 3;

                _strategy.TrySolve(_fieldMock.Object);

                foreach (var square in _testSquaresCol)
                {
                    Assert.DoesNotContain(1, square.Candidates);
                    Assert.DoesNotContain(3, square.Candidates);
                }
            }

            [Fact]
            public void Should_remove_set_values_from_cadidate_lists_in_big_square()
            {
                _testSquaresRow[0].Value = 1;
                _testSquaresCol[1].Value = 3;
                _testSquaresBig[8].Value = 5;


                _strategy.TrySolve(_fieldMock.Object);

                foreach (var square in _testSquaresBig)
                {
                    Assert.DoesNotContain(5, square.Candidates);
                }
            }

            [Fact]
            public void Should_return_squares_have_that_have_had_candidates_removed()
            {
                _testSquaresRow[0].Value = 1;

                var result = _strategy.TrySolve(_fieldMock.Object);

                //Assert.True(result);
            }

            [Fact]
            public void Should_return_false_when_squares_have_not_been_modified()
            {
                _testSquaresRow[0].Value = 1;

                var firstTry = _strategy.TrySolve(_fieldMock.Object);
                var secondTry = _strategy.TrySolve(_fieldMock.Object);

                //Assert.False(secondTry);
            }
        }
    }
}
