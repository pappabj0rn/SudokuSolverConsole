using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverConsole.Strategies
{
    public class CandidateFilterStrategy : SolvingStrategy
    {
        public override List<Square> TrySolve(IPlayingField field)
        {
            var run = true;
            var modifiedSquares = new List<Square>();

            while (run)
            {
                run = false;

                FilterCandidates(field.GetRow, ref run, modifiedSquares);
                FilterCandidates(field.GetColumn, ref run, modifiedSquares);
                FilterCandidates(field.GetBigSquare, ref run, modifiedSquares);

                foreach (var square in modifiedSquares.Where(x => x.Candidates.Count == 1)) //todo: && value == 0, but how to test? guess I could extract interface for square and verify 
                {
                    square.Value = square.Candidates[0];
                }
            }

            return modifiedSquares.Distinct().ToList();
        }

        private static void FilterCandidates(Func<int,List<Square>> squareSelector, ref bool run, List<Square> modifiedSquares)
        {
            var currentMods = new List<Square>();

            for (var i = 0; i < PlayingField.Height; i++)
            {
                var currentSquares = squareSelector(i);
                var usedNumbers = currentSquares
                    .Where(x => x.Value > 0)
                    .Select(square => square.Value)
                    .ToList();

                usedNumbers.ForEach(n => 
                    currentMods.AddRange(
                        currentSquares.Where(x => x.Value == 0 && x.Candidates.Remove(n))));
            }

            
            if (!currentMods.Any())
                return;

            run = true;
            modifiedSquares.AddRange(currentMods);
        }
    }
}