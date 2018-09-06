using Castle.DynamicProxy.Tokens;
using SudokuSolverConsole;
using Xunit;

namespace SudokuSolverConsoleTestUnit
{
    public abstract class PlayingFieldTests
    {
        protected const string FieldDefinition = "007000069010906040000302000238790100400030020000260030609000000040080507370019000";
        private IPlayingField _field;

        protected PlayingFieldTests()
        {
            _field = new PlayingField(FieldDefinition);
        }
        public class Constructor : PlayingFieldTests
        {
            private const string Complete = "527841369813976245964352871238794156496135728751268934689527413142683597375419682";
            public Constructor()
            {
                _field = new PlayingField(FieldDefinition);
            }

            [Fact]
            public void Should_set_square_values_from_input_string()
            {
                var i = 0;
                for (var y = 0; y < PlayingField.GroupCount; y++)
                {
                    for (var x = 0; x < PlayingField.GroupCount; x++)
                    {
                        Assert.Equal(
                            int.Parse(FieldDefinition[i].ToString()),
                            _field.Squares[x,y].Value);
                        i++;
                    }    
                }
            }

            [Fact]
            public void Should_set_square_meta_to_x_and_y_coordinates()
            {
                for (var y = 0; y < PlayingField.GroupCount; y++)
                {
                    for (var x = 0; x < PlayingField.GroupCount; x++)
                    {
                        Assert.Equal(x,_field.Squares[x,y].Meta["x"]);
                        Assert.Equal(y,_field.Squares[x,y].Meta["y"]);
                    }
                    
                }
            }
        }

        public class GetRow_int : PlayingFieldTests
        {
            private const string FieldDefinition = "123456789111222333000000000000000000000000000000000000000000000000000000000000000";
            public GetRow_int()
            {
                _field = new PlayingField(FieldDefinition);
            }

            [Fact]
            public void Should_return_nine_squares()
            {
                var row = _field.GetRow(0);

                Assert.Equal(9,row.Count);
            }

            [Theory]
            [InlineData(0,"123456789")]
            [InlineData(1,"111222333")]
            public void Should_return_squares_from_selected_row(int rowIndex, string expextedValues)
            {
                var row = _field.GetRow(rowIndex);

                for (var i = 0; i < row.Count; i++)
                {
                    Assert.Equal(
                        int.Parse(expextedValues[i].ToString()),
                        row[i].Value);
                }
            }
        }

        public class GetColumn_int : PlayingFieldTests
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
            public GetColumn_int()
            {
                _field = new PlayingField(FieldDefinition);
            }

            [Fact]
            public void Should_return_nine_squares()
            {
                var row = _field.GetRow(0);

                Assert.Equal(9,row.Count);
            }

            [Theory]
            [InlineData(0,"123456789")]
            [InlineData(1,"111222333")]
            public void Should_return_squares_from_selected_row(int rowIndex, string expextedValues)
            {
                var row = _field.GetColumn(rowIndex);

                for (var i = 0; i < row.Count; i++)
                {
                    Assert.Equal(
                        int.Parse(expextedValues[i].ToString()),
                        row[i].Value);
                }
            }
        }

        public class GetBigSquare_int : PlayingFieldTests
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
            public GetBigSquare_int()
            {
                _field = new PlayingField(FieldDefinition);
            }

            [Fact]
            public void Should_return_nine_squares()
            {
                var row = _field.GetBigSquare(0);

                Assert.Equal(9,row.Count);
            }

            [Theory]
            [InlineData(0,"123456789")]
            [InlineData(1,"111222333")]
            [InlineData(8,"999999999")]
            public void Should_return_squares_from_selected_row(int rowIndex, string expextedValues)
            {
                var row = _field.GetBigSquare(rowIndex);

                for (var i = 0; i < row.Count; i++)
                {
                    Assert.Equal(
                        int.Parse(expextedValues[i].ToString()),
                        row[i].Value);
                }
            }
        }

        public class ToString : PlayingFieldTests
        {
            [Fact]
            public void Should_return_given_constructor_string_after_construction()
            {
                _field = new PlayingField(FieldDefinition);
                var str = _field.ToString();

                Assert.Equal(FieldDefinition, str);
            }
        }

        public class GetRow_square : PlayingFieldTests
        {
            [Theory]
            [InlineData(1,0,0)]
            [InlineData(0,1,1)]
            [InlineData(0,3,3)]
            [InlineData(6,5,5)]
            public void Should_return_correct_row_for_square(int x, int y, int expected)
            {
                var group = _field.GetRow(expected);
                var squares = _field.GetRow(_field.Squares[x, y]);
                
                Assert.Equal(group, squares);
            }
        }

        public class GetColumn_square : PlayingFieldTests
        {
            [Theory]
            [InlineData(0,1,0)]
            [InlineData(1,2,1)]
            [InlineData(3,0,3)]
            [InlineData(5,7,5)]
            public void Should_return_correct_column_for_square(int x, int y, int expected)
            {
                var group = _field.GetColumn(expected);
                var squares = _field.GetColumn(_field.Squares[x, y]);
                
                Assert.Equal(group, squares);
            }
        }

        public class GetBigSquare_square : PlayingFieldTests
        {
            [Theory]
            [InlineData(0,1,0)]
            [InlineData(1,2,0)]
            [InlineData(3,0,1)]
            [InlineData(5,1,1)]
            [InlineData(4,5,4)]
            [InlineData(5,3,4)]
            public void Should_return_correct_big_square_for_square(int x, int y, int expected)
            {
                var group = _field.GetBigSquare(expected);
                var squares = _field.GetBigSquare(_field.Squares[x, y]);
                
                Assert.Equal(group, squares);
            }
        }
    }
}