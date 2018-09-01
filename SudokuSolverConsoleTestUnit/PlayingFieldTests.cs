using SudokuSolverConsole;
using Xunit;

namespace SudokuSolverConsoleTestUnit
{
    public abstract class PlayingFieldTests
    {
        private PlayingField field; 
        public class Constructor : PlayingFieldTests
        {
            private const string Complete =        "527841369813976245964352871238794156496135728751268934689527413142683597375419682";
            private const string FieldDefinition = "007000069010906040000302000238790100400030020000260030609000000040080507370019000";

            public Constructor()
            {
                field = new PlayingField(FieldDefinition);
            }

            [Fact]
            public void Should_set_square_values_from_input_string()
            {
                var i = 0;
                for (var y = 0; y < PlayingField.Width; y++)
                {
                    for (var x = 0; x < PlayingField.Height; x++)
                    {
                        Assert.Equal(
                            int.Parse(FieldDefinition[i].ToString()),
                            field.Squares[x,y].Value);
                        i++;
                    }    
                }
            }
        }

        public class GetRow : PlayingFieldTests
        {
            private const string FieldDefinition = "123456789111222333000000000000000000000000000000000000000000000000000000000000000";
            public GetRow()
            {
                field = new PlayingField(FieldDefinition);
            }

            [Fact]
            public void Should_return_nine_squares()
            {
                var row = field.GetRow(0);

                Assert.Equal(9,row.Count);
            }

            [Theory]
            [InlineData(0,"123456789")]
            [InlineData(1,"111222333")]
            public void Should_return_squares_from_selected_row(int rowIndex, string expextedValues)
            {
                var row = field.GetRow(rowIndex);

                for (var i = 0; i < row.Count; i++)
                {
                    Assert.Equal(
                        int.Parse(expextedValues[i].ToString()),
                        row[i].Value);
                }
            }
        }

        public class GetColumn : PlayingFieldTests
        {
            private const string FieldDefinition = "110000000" +
                                                   "210000000" +
                                                   "310000000" +
                                                   "420000000" +
                                                   "520000000" +
                                                   "620000000" +
                                                   "730000000" +
                                                   "830000000" +
                                                   "930000000";
            public GetColumn()
            {
                field = new PlayingField(FieldDefinition);
            }

            [Fact]
            public void Should_return_nine_squares()
            {
                var row = field.GetRow(0);

                Assert.Equal(9,row.Count);
            }

            [Theory]
            [InlineData(0,"123456789")]
            [InlineData(1,"111222333")]
            public void Should_return_squares_from_selected_row(int rowIndex, string expextedValues)
            {
                var row = field.GetColumn(rowIndex);

                for (var i = 0; i < row.Count; i++)
                {
                    Assert.Equal(
                        int.Parse(expextedValues[i].ToString()),
                        row[i].Value);
                }
            }
        }

        public class GetBigSquare : PlayingFieldTests
        {
            private const string FieldDefinition = "123111000" +
                                                   "456222000" +
                                                   "789333000" +
                                                   "000000000" +
                                                   "000000000" +
                                                   "000000000" +
                                                   "000000999" +
                                                   "000000999" +
                                                   "000000999";
            public GetBigSquare()
            {
                field = new PlayingField(FieldDefinition);
            }

            [Fact]
            public void Should_return_nine_squares()
            {
                var row = field.GetBigSquare(0);

                Assert.Equal(9,row.Count);
            }

            [Theory]
            [InlineData(0,"123456789")]
            [InlineData(1,"111222333")]
            [InlineData(8,"999999999")]
            public void Should_return_squares_from_selected_row(int rowIndex, string expextedValues)
            {
                var row = field.GetBigSquare(rowIndex);

                for (var i = 0; i < row.Count; i++)
                {
                    Assert.Equal(
                        int.Parse(expextedValues[i].ToString()),
                        row[i].Value);
                }
            }
        }
    }
}