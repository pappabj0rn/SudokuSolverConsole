using System.Collections.Generic;

namespace SudokuSolverConsole
{
    public class PlayingField : IPlayingField
    {
        public const int Width = 9;
        public const int Height = 9;
        public const int BigSquareSize = 3;
        public Square[,] Squares { get; set; }

        public PlayingField(string s)
        {
            var i = 0;
            Squares = new Square[Width,Height];
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    Squares[x,y] = new Square(int.Parse(s[i].ToString()));
                    i++;
                }    
            }
        }

        public List<Square> GetRow(int y)
        {
            var row = new List<Square>();

            for (var x = 0; x < Width; x++)
            {
                row.Add(Squares[x,y]);
            }

            return row;
        }

        public List<Square> GetColumn(int x)
        {
            var col = new List<Square>();

            for (var y = 0; y < Height; y++)
            {
                col.Add(Squares[x,y]);
            }

            return col;
        }

        public List<Square> GetBigSquare(int i)
        {
            var bigSquare = new List<Square>();
            var xOffset = i%3*BigSquareSize;
            var yOffset = i / 3 * BigSquareSize;

            for (var y = 0; y < BigSquareSize; y++)
            {
                for (var x = 0; x < BigSquareSize; x++)
                {
                    bigSquare.Add(Squares[x+xOffset,y+yOffset]);
                }
            }

            return bigSquare;
        }
    }
}