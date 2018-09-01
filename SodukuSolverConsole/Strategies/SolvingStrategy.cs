using System.Collections.Generic;

namespace SudokuSolverConsole.Strategies
{
    public abstract class SolvingStrategy
    {
        public abstract List<Square> TrySolve(IPlayingField field);
    }
}