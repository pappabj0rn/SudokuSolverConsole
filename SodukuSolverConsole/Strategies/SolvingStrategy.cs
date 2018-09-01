namespace SudokuSolverConsole.Strategies
{
    public abstract class SolvingStrategy
    {
        public abstract bool TrySolve(IPlayingField field);
    }
}