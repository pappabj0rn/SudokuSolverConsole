using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverConsole.Strategies
{
    public class CandidateFilterStrategy : SolvingStrategy
    {
        public override List<Square> TrySolve(IPlayingField field)
        {
            var modifiedSquares = new List<Square>();

            modifiedSquares.AddRange(FilterCandidates(field.GetRow));
            modifiedSquares.AddRange(FilterCandidates(field.GetColumn));
            modifiedSquares.AddRange(FilterCandidates(field.GetBigSquare));

            foreach (var square in modifiedSquares.Where(x=>x.Candidates.Count==1))
            {
                square.Value = square.Candidates[0];
            }

            return modifiedSquares.Distinct().ToList();
        }

        private static List<Square> FilterCandidates(Func<int,List<Square>> squareSelector)
        {
            var modifiedSquares = new List<Square>();
            for (var i = 0; i < PlayingField.Height; i++)
            {
                var currentSquares = squareSelector(i);
                var usedNumbers = currentSquares
                    .Where(x => x.Value > 0)
                    .Select(square => square.Value)
                    .ToList();

                foreach (var number in usedNumbers)
                {
                    foreach (var square in currentSquares.Where(x => x.Value==0))
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