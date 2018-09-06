using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSolverConsole.Strategies
{
    public class SinglePositionStrategy : SolvingStrategy
    {
        private IPlayingField _field;

        public override List<Square> TrySolve(IPlayingField field)
        {
            _field = field;

            var modifiedSquares = new List<Square>();
            
            SetSingleOccurance(field.GetRow, modifiedSquares);
            SetSingleOccurance(field.GetColumn, modifiedSquares);
            SetSingleOccurance(field.GetBigSquare, modifiedSquares);

            return modifiedSquares.Distinct().ToList();
        }

        private void SetSingleOccurance(Func<int,List<Square>> squareSelector, List<Square> modifiedSquares)
        {
            var currentMods = new List<Square>();

            for (var i = 0; i < PlayingField.GroupCount; i++)
            {
                var candidateCount = new Dictionary<int,int>();
                var groupSquares = squareSelector(i);
                foreach (var square in groupSquares.Where(x=>x.Value==0))
                {
                    foreach (int candidate in square.Candidates)
                    {
                        if(!candidateCount.ContainsKey(candidate))
                            candidateCount.Add(candidate,0);

                        candidateCount[candidate]++;
                    }
                }

                foreach (var count in candidateCount.Where(x=>x.Value==1))
                {
                    var spSquare = groupSquares.First(x => x.Candidates.Contains(count.Key) && x.Value==0);
                    spSquare.Value = count.Key;
                    RemoveCandidatesInGroups(spSquare);
                    currentMods.Add(spSquare);
                }
                
            }

            
            if (!currentMods.Any())
                return;

            modifiedSquares.AddRange(currentMods);
        }

        private void RemoveCandidatesInGroups(Square square)
        {
            var affectedSquares = _field.GetRow(square);
            affectedSquares.AddRange(_field.GetColumn(square)); 
            affectedSquares.AddRange(_field.GetBigSquare(square));

            foreach (var affectedSquare in affectedSquares.Where(x=>x.Value==0))
            {
                affectedSquare.Candidates.Remove(square.Value);
            }
        }
    }
}