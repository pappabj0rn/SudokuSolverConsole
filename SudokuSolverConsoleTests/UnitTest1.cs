using System;
using Xunit;

namespace SudokuSolverConsoleTests
{
    public class PlayingFieldTests
    {
        public class Constructor : PlayingFieldTests
        {
            private PlayingField field = new PlayingField(); 

            [Fact]
            public void Should_set_square_values_from_input_string()
            {
                var i = 0;
                for (var x = 0; x < Width; x++)
                {
                    for (var y = 0; y < Height; y++)
                    {
                        Squares[x,y] = new Square();
                    }    
                }
            }
        }
    }
}
