using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverConsole.Strategies
{
    public class CandidateFilterStrategy : SolvingStrategy
    {
        public override List<Square> TrySolve(IPlayingField field)
        {
            var modifiedSquares = new List<Square>();
            for (var y = 0; y < PlayingField.Height; y++)
            {
                var rowSquares = field.GetRow(y);
                var usedNumbers = rowSquares
                    .Where(x => x.Value > 0)
                    .Select(square => square.Value)
                    .ToList();

                foreach (var number in usedNumbers)
                {
                    foreach (var square in rowSquares.Where(x => x.Candidates != null))
                    {
                        if (square.Candidates.Remove(number))
                            modifiedSquares.Add(square);
                    }
                }
            }

            return modifiedSquares;
        }
    }
}