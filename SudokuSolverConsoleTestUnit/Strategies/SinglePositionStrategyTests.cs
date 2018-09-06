using System.Linq;
using Moq;
using SudokuSolverConsole;
using SudokuSolverConsole.Strategies;
using Xunit;

namespace SudokuSolverConsoleTestUnit.Strategies
{
    public abstract class SinglePositionStrategyTests : StrategyTestsBase
    {
        private readonly SolvingStrategy _strategy = new SinglePositionStrategy();

        protected SinglePositionStrategyTests()
        {
            Field = new PlayingField("094080362" +
                                     "200003017" +
                                     "000000905" +
                                     "000000006" +
                                     "000040739" +
                                     "009150008" +
                                     "800320694" +
                                     "000000503" +
                                     "530007001");

            //Prepare candidates with CFS
            new CandidateFilterStrategy().TrySolve(Field);
        }
        public class TrySolve : SinglePositionStrategyTests
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
            public void Should_set_values_for_squares_in_group_that_have_the_only_occurance_of_a_candidate()
            {
                _strategy.TrySolve(FieldMock.Object);

                Assert.Equal(1,Field.Squares[6,3].Value);
            }

            [Fact]
            public void Should_not_make_any_modifications_when_candidates_have_not_changed()
            {
                Field = new PlayingField("123456000" +
                                         "000000000" +
                                         "000000000" +
                                         "000000000" +
                                         "000000090" +
                                         "000000000" +
                                         "000000000" +
                                         "000000000" +
                                         "000000900");
                new CandidateFilterStrategy().TrySolve(Field);

                _strategy.TrySolve(FieldMock.Object);

                var mods = _strategy.TrySolve(FieldMock.Object);

                Assert.Empty(mods);
            }

            [Fact]
            public void Should_filter_candidates_for_current_groups_after_setting_a_value()
            {
                Field = new PlayingField("123456000" +
                                         "000000000" +
                                         "000000000" +
                                         "000000000" +
                                         "000000090" +
                                         "000000000" +
                                         "000000000" +
                                         "000000000" +
                                         "000000900");
                new CandidateFilterStrategy().TrySolve(Field);

                _strategy.TrySolve(FieldMock.Object);

                var row = Field.GetRow(0);
                var col = Field.GetColumn(8);
                var big = Field.GetBigSquare(2);

                var unsetSquares = row.Where(x => x.Value == 0).ToList();
                unsetSquares.AddRange(col.Where(x=>x.Value==0));
                unsetSquares.AddRange(big.Where(x=>x.Value==0));

                foreach (var square in unsetSquares)
                {
                    Assert.DoesNotContain(9,square.Candidates);
                }
            }
        }
    }
}