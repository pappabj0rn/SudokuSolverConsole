using System.Collections.Generic;

namespace SudokuSolverConsole
{
    public interface IPlayingField
    {
        Square[,] Squares { get; set; }
        List<Square> GetRow(int y);
        List<Square> GetColumn(int x);
        List<Square> GetBigSquare(int i);
        List<Square> GetRow(Square s);
        List<Square> GetColumn(Square s);
        List<Square> GetBigSquare(Square s);
    }
}