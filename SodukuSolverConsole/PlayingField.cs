using System.Collections.Generic;
using System.Text;

namespace SudokuSolverConsole
{
    public class PlayingField : IPlayingField
    {
        public const int GroupCount = 9;
        public const int BigSquareSize = 3;
        public Square[,] Squares { get; set; }

        public PlayingField(string s)
        {
            var i = 0;
            Squares = new Square[GroupCount,GroupCount];
            for (var y = 0; y < GroupCount; y++)
            {
                for (var x = 0; x < GroupCount; x++)
                {
                    Squares[x, y] = new Square(int.Parse(s[i].ToString()));
                    Squares[x, y].Meta.Add(Square.Keys.X, x);
                    Squares[x, y].Meta.Add(Square.Keys.Y, y);
                    i++;
                }    
            }
        }

        public List<Square> GetRow(int y)
        {
            var row = new List<Square>();

            for (var x = 0; x < GroupCount; x++)
            {
                row.Add(Squares[x,y]);
            }

            return row;
        }

        public List<Square> GetRow(Square square)
        {
            return GetRow((int) square.Meta[Square.Keys.Y]);
        }

        public List<Square> GetColumn(int x)
        {
            var col = new List<Square>();

            for (var y = 0; y < GroupCount; y++)
            {
                col.Add(Squares[x,y]);
            }

            return col;
        }

        public List<Square> GetColumn(Square square)
        {
            return GetColumn((int) square.Meta[Square.Keys.X]);
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

        public List<Square> GetBigSquare(Square square)
        {
            var y = (int) square.Meta[Square.Keys.Y];
            var a = y / 3 * 3;
            var x = (int) square.Meta[Square.Keys.X];
            var b = x / 3;

            return GetBigSquare(a+b);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var y = 0; y < GroupCount; y++)
            {
                for (var x = 0; x < GroupCount; x++)
                {
                    sb.Append(Squares[x, y].Value);
                }
            }

            return sb.ToString();
        }
    }
}