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
        protected IPlayingField _field = new PlayingField("000000000" +
                                                          "000000000" +
                                                          "000000000" +
                                                          "000000000" +
                                                          "000000000" +
                                                          "000000000" +
                                                          "000000000" +
                                                          "000000000" +
                                                          "000000000");
        
        protected CandidateFilterStrategy _strategy = new CandidateFilterStrategy();

        protected CandidateFilterStrategyTests()
        {
            _fieldMock.Setup(x => x.GetRow(It.IsAny<int>()))
                .Returns((int i) => _field.GetRow(i));

            _fieldMock.Setup(x => x.GetColumn(It.IsAny<int>()))
                .Returns((int i) => _field.GetColumn(i));

            _fieldMock.Setup(x => x.GetBigSquare(It.IsAny<int>()))
                .Returns((int i) => _field.GetBigSquare(i));
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
                _field.Squares[0,0].Value = 1;
                _field.Squares[5,0].Value = 3;

                _strategy.TrySolve(_fieldMock.Object);
                
                foreach (var square in _field.GetRow(0).Where(x=>x.Value==0))
                {
                    Assert.DoesNotContain(1, square.Candidates);
                    Assert.DoesNotContain(3, square.Candidates);
                }
            }

            [Fact]
            public void Should_remove_set_values_from_cadidate_lists_in_column()
            {
                _field.Squares[0,0].Value = 1;
                _field.Squares[0,1].Value = 3;

                _strategy.TrySolve(_fieldMock.Object);

                foreach (var square in _field.GetColumn(0).Where(x=>x.Value==0))
                {
                    Assert.DoesNotContain(1, square.Candidates);
                    Assert.DoesNotContain(3, square.Candidates);
                }
            }

            [Fact]
            public void Should_remove_set_values_from_cadidate_lists_in_big_square()
            {
                _field.Squares[0,0].Value = 1;
                _field.Squares[0,1].Value = 3;
                _field.Squares[2,2].Value = 5;


                _strategy.TrySolve(_fieldMock.Object);

                foreach (var square in _field.GetBigSquare(0).Where(x=>x.Value==0))
                {
                    Assert.DoesNotContain(5, square.Candidates);
                }
            }

            [Fact]
            public void Should_return_squares_that_have_had_candidates_removed()
            {
                _field.Squares[0,0].Value = 1;
                _field.Squares[0,1].Value = 3;
                _field.Squares[2,2].Value = 5;

                var expectedSquares = new List<Square>();
                expectedSquares.AddRange(_field.GetRow(0));
                expectedSquares.AddRange(_field.GetRow(1));
                expectedSquares.AddRange(_field.GetRow(2));
                expectedSquares.AddRange(_field.GetColumn(0));
                expectedSquares.AddRange(_field.GetColumn(2));
                expectedSquares.AddRange(_field.GetBigSquare(0));
                expectedSquares.RemoveAll(x => x.Id == _field.Squares[0,0].Id);
                expectedSquares.RemoveAll(x => x.Id == _field.Squares[0,1].Id);
                expectedSquares.RemoveAll(x => x.Id == _field.Squares[2,2].Id);
                expectedSquares = expectedSquares.Distinct().OrderBy(s => s.Meta).ToList();
                
                var modSquares = _strategy.TrySolve(_fieldMock.Object)
                    .OrderBy(s => s.Meta)
                    .ToList();

                Assert.Equal(expectedSquares,modSquares);
            }

            [Fact]
            public void Should_set_value_for_squares_having_only_one_candidate()
            {
                for (var i = 0; i < 8; i++) // skip last square in row
                {
                    _field.Squares[i,0].Value = i+1;
                }

                _strategy.TrySolve(_fieldMock.Object);

                Assert.Equal(9,_field.Squares[8,0].Value);
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
